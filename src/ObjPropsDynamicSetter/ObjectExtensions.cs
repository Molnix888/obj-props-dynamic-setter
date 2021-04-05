using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ObjPropsDynamicSetter
{
    /// <summary>Provides extension methods to access object properties via their names.</summary>
    public static class ObjectExtensions
    {
        /// <summary>Gets public property info via its name.</summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <returns>Property information.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        public static PropertyInfo GetPropertyInfo<T>(this T obj, string name) => obj.GetPropertyInfo(name, false);

        /// <summary>Gets property info via its name.</summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="includeNonPublic">Indicates whether to include non-public properties.</param>
        /// <returns>Property information.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        public static PropertyInfo GetPropertyInfo<T>(this T obj, string name, bool includeNonPublic)
        {
            ValidateParameters(obj, name);
            return GetPropertyDetails(obj, GetPropertyPathItems(name), includeNonPublic).PropertyInfo;
        }

        /// <summary>Gets public property value via its name.</summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <returns>Property value.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        public static object GetPropertyValue<T>(this T obj, string name) => obj.GetPropertyValue(name, false);

        /// <summary>Gets property value via its name.</summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="includeNonPublic">Indicates whether to include non-public properties.</param>
        /// <returns>Property value.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        public static object GetPropertyValue<T>(this T obj, string name, bool includeNonPublic)
        {
            ValidateParameters(obj, name);
            return GetPropertyDetails(obj, GetPropertyPathItems(name), includeNonPublic).Value;
        }

        /// <summary>Sets public property value via its name.</summary>
        /// <typeparam name="TObj">Object type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to assign to property.</param>
        /// <returns>Updated object.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        /// <exception cref="InvalidCastException">Property value cannot be casted to expected return type.</exception>
        /// <exception cref="OverflowException">Number is out of range of conversionType.</exception>
        public static TObj SetPropertyValue<TObj, TValue>(this TObj obj, string name, TValue value) => obj.SetPropertyValue(name, value, false);

        /// <summary>Sets property value via its name.</summary>
        /// <typeparam name="TObj">Object type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to assign to property.</param>
        /// <param name="includeNonPublic">Indicates whether to include non-public properties.</param>
        /// <returns>Updated object.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        /// <exception cref="InvalidCastException">Property value cannot be casted to expected return type.</exception>
        /// <exception cref="OverflowException">Number is out of range of conversionType.</exception>
        public static TObj SetPropertyValue<TObj, TValue>(this TObj obj, string name, TValue value, bool includeNonPublic)
        {
            ValidateParameters(obj, name);
            return (TObj)obj.SetPropertyValue(GetPropertyPathItems(name), value, includeNonPublic);
        }

        private static (object Value, PropertyInfo PropertyInfo) GetPropertyDetails(object obj, ICollection<string> propertyPathItems, bool includeNonPublic)
        {
            var (_, cutPropertyPathItems, propertyInfo) = GetFirstPropertyDetails(obj, propertyPathItems, includeNonPublic);
            return cutPropertyPathItems.Any()
                ? GetPropertyDetails(propertyInfo.GetValue(obj), propertyPathItems, includeNonPublic)
                : (propertyInfo.GetValue(obj), propertyInfo);
        }

        private static object SetPropertyValue(this object obj, ICollection<string> propertyPathItems, object value, bool includeNonPublic)
        {
            var (propertyName, cutPropertyPathItems, propertyInfo) = GetFirstPropertyDetails(obj, propertyPathItems, includeNonPublic);
            if (cutPropertyPathItems.Any())
            {
                var innerObject = propertyInfo.GetValue(obj);
                _ = innerObject.SetPropertyValue(cutPropertyPathItems, value, includeNonPublic);
                propertyInfo = GetObjectPropertyInfo(obj, propertyName, includeNonPublic);
                SetPropertyValue(obj, propertyInfo, innerObject);
            }
            else
            {
                SetPropertyValue(obj, propertyInfo, value);
            }

            return obj;

            static void SetPropertyValue(object obj, PropertyInfo propertyInfo, object value) =>
                propertyInfo.SetValue(obj, value is IConvertible ? Convert.ChangeType(value, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType, CultureInfo.InvariantCulture) : value);
        }

        private static (string Name, ICollection<string> PathItems, PropertyInfo Info) GetFirstPropertyDetails(object obj, ICollection<string> pathItems, bool includeNonPublic)
        {
            var name = pathItems.First();
            _ = pathItems.Remove(name);
            var propertyInfo = GetObjectPropertyInfo(obj, name, includeNonPublic);
            return (name, pathItems, propertyInfo);
        }

        private static PropertyInfo GetObjectPropertyInfo(object obj, string name, bool includeNonPublic)
        {
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
            if (includeNonPublic)
            {
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                flags |= BindingFlags.NonPublic;
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
            }

            var type = obj.GetType();
            return type.GetProperty(name, flags) ?? throw new ArgumentException($"Property {name} not found in {type}.");
        }

        private static ICollection<string> GetPropertyPathItems(string path) => path.Split('.', StringSplitOptions.RemoveEmptyEntries).ToList();

        private static void ValidateParameters<T>(T obj, string name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Property name cannot be null, empty or whitespace.");
            }
        }
    }
}

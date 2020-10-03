using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ObjPropsDynamicSetter
{
    /// <summary>
    /// Provides extension methods to access object properties via their names.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the property info via its name.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <returns>Property information.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        public static PropertyInfo GetPropertyInfo(this object obj, string name) => obj.GetPropertyInfo(name, false);

        /// <summary>
        /// Gets the property info via its name.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="includeNonPublic">Indicates whether to include non-public properties.</param>
        /// <returns>Property information.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        public static PropertyInfo GetPropertyInfo(this object obj, string name, bool includeNonPublic)
        {
            ValidateParameters(obj, name);
            return obj.GetPropertyDetails(GetPropertyPathItems(name), includeNonPublic).PropertyInfo;
        }

        /// <summary>
        /// Gets the property value via its name.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <returns>Property value.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        /// <exception cref="InvalidCastException">Property value cannot be casted to expected return type.</exception>
        public static T GetPropertyValue<T>(this object obj, string name) => obj.GetPropertyValue<T>(name, false);

        /// <summary>
        /// Gets the property value via its name.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="includeNonPublic">Indicates whether to include non-public properties.</param>
        /// <returns>Property value.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        /// <exception cref="InvalidCastException">Property value cannot be casted to expected return type.</exception>
        public static T GetPropertyValue<T>(this object obj, string name, bool includeNonPublic)
        {
            ValidateParameters(obj, name);
            return (T)obj.GetPropertyDetails(GetPropertyPathItems(name), includeNonPublic).Value;
        }

        /// <summary>
        /// Sets the property value via its name.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to assign to property.</param>
        /// <returns>Updated object.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        /// <exception cref="InvalidCastException">Property value cannot be casted to expected return type.</exception>
        /// <exception cref="OverflowException">Number is out of the range of conversionType.</exception>
        public static T SetPropertyValue<T>(this object obj, string name, object value) => obj.SetPropertyValue<T>(name, value, false);

        /// <summary>
        /// Sets the property value via its name.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to assign to property.</param>
        /// <param name="includeNonPublic">Indicates whether to include non-public properties.</param>
        /// <returns>Updated object.</returns>
        /// <exception cref="ArgumentNullException">Object is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object or property name is null or empty.</exception>
        /// <exception cref="InvalidCastException">Property value cannot be casted to expected return type.</exception>
        /// <exception cref="OverflowException">Number is out of the range of conversionType.</exception>
        public static T SetPropertyValue<T>(this object obj, string name, object value, bool includeNonPublic)
        {
            ValidateParameters(obj, name);
            return (T)obj.SetPropertyValue(GetPropertyPathItems(name), value, includeNonPublic);
        }

        private static (object Value, PropertyInfo PropertyInfo) GetPropertyDetails(this object obj, ICollection<string> propertyPathItems, bool includeNonPublic)
        {
            var (_, cutPropertyPathItems, propertyInfo) = GetFirstPropertyDetails(obj, propertyPathItems, includeNonPublic);

            if (cutPropertyPathItems.Any())
            {
                var innerObject = propertyInfo.GetValue(obj);
                return innerObject.GetPropertyDetails(propertyPathItems, includeNonPublic);
            }

            return (propertyInfo.GetValue(obj), propertyInfo);
        }

        private static object SetPropertyValue(this object obj, ICollection<string> propertyPathItems, object value, bool includeNonPublic)
        {
            var (propertyName, cutPropertyPathItems, propertyInfo) = GetFirstPropertyDetails(obj, propertyPathItems, includeNonPublic);

            if (cutPropertyPathItems.Any())
            {
                var innerObject = propertyInfo.GetValue(obj);
                _ = innerObject.SetPropertyValue(cutPropertyPathItems, value, includeNonPublic);

                propertyInfo = obj.GetObjectPropertyInfo(propertyName, includeNonPublic);
                obj.SetPropertyValue(propertyInfo, innerObject);
            }
            else
            {
                obj.SetPropertyValue(propertyInfo, value);
            }

            return obj;
        }

        private static void SetPropertyValue(this object obj, PropertyInfo propertyInfo, object value)
        {
            var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            value = value is IConvertible ? Convert.ChangeType(value, propertyType, CultureInfo.InvariantCulture) : value;
            propertyInfo.SetValue(obj, value);
        }

        private static (string Name, ICollection<string> PathItems, PropertyInfo Info) GetFirstPropertyDetails(object obj, ICollection<string> pathItems, bool includeNonPublic)
        {
            var name = pathItems.First();
            _ = pathItems.Remove(name);
            var propertyInfo = obj.GetObjectPropertyInfo(name, includeNonPublic);

            return (name, pathItems, propertyInfo);
        }

        private static PropertyInfo GetObjectPropertyInfo(this object obj, string name, bool includeNonPublic)
        {
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

            if (includeNonPublic)
            {
                flags |= BindingFlags.NonPublic;
            }

            var type = obj.GetType();
            return type.GetProperty(name, flags) ?? throw new ArgumentException($"Property {name} not found in {type}.");
        }

        private static ICollection<string> GetPropertyPathItems(string path) => path.Split('.', StringSplitOptions.RemoveEmptyEntries).ToList();

        private static void ValidateParameters(object obj, string name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Property name cannot be null or empty.");
            }
        }
    }
}

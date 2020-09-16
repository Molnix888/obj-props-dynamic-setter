using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ObjPropsDynamicSetter
{
    /// <summary>
    /// Provides methods to access object properties via their names.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the property info via its name.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="delimiter">delimiter.</param>
        /// <returns>Property information.</returns>
        /// <exception cref="ArgumentNullException">Object or property name is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object.</exception>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        public static PropertyInfo GetPropertyInfo(this object obj, string name, char delimiter = '.')
        {
            ValidateParameters(obj, name);
            return GetPropertyInfo<object>(obj, name.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList()).PropertyInfo;
        }

        /// <summary>
        /// Sets the property value via its name.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to assign to property.</param>
        /// <param name="delimiter">delimiter.</param>
        /// <exception cref="ArgumentNullException">Object or property name is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object.</exception>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        public static void SetPropertyValue(this object obj, string name, object value, char delimiter = '.')
        {
            ValidateParameters(obj, name);
            SetPropertyValue(obj, name.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList(), value);
        }

        /// <summary>
        /// Gets the property value via its name.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="delimiter">delimiter.</param>
        /// <returns>Property value.</returns>
        /// <exception cref="ArgumentNullException">Object or property name is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object.</exception>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        /// <exception cref="InvalidCastException">Property value cannot be casted to expected return type.</exception>
        public static T GetPropertyValue<T>(this object obj, string name, char delimiter = '.')
        {
            ValidateParameters(obj, name);
            return GetPropertyInfo<T>(obj, name.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList()).Value;
        }

        private static (T Value, PropertyInfo PropertyInfo) GetPropertyInfo<T>(object obj, ICollection<string> propertyPathItems)
        {
            var (_, cutPropertyPathItems, propertyInfo) = GetFirstPropertyDetails(obj, propertyPathItems);

            if (!cutPropertyPathItems.Any())
            {
                return ((T)propertyInfo.GetValue(obj), propertyInfo);
            }

            var innerObject = propertyInfo.GetValue(obj);
            return GetPropertyInfo<T>(innerObject, propertyPathItems);
        }

        private static void SetPropertyValue(this object obj, ICollection<string> propertyPathItems, object value)
        {
            var (name, cutPropertyPathItems, propertyInfo) = GetFirstPropertyDetails(obj, propertyPathItems);

            if (cutPropertyPathItems.Any())
            {
                var innerObject = propertyInfo.GetValue(obj);
                innerObject.SetPropertyValue(cutPropertyPathItems, value);

                obj.SetPropertyValue(name, innerObject);
            }
            else
            {
                var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                value = value is IConvertible ? Convert.ChangeType(value, propertyType, CultureInfo.InvariantCulture) : value;
                propertyInfo.SetValue(obj, value);
            }
        }

        private static void ValidateParameters(object obj, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        private static (string PropertyName, ICollection<string> PropertyPathItems, PropertyInfo PropertyInfo) GetFirstPropertyDetails(object obj, ICollection<string> propertyPathItems)
        {
            var name = propertyPathItems.First();
            _ = propertyPathItems.Remove(name);
            var propertyInfo = obj.GetType().GetTypePropInfo(name);

            return (name, propertyPathItems, propertyInfo);
        }

        private static PropertyInfo GetTypePropInfo(this Type type, string name) => type.GetProperty(name) ?? throw new ArgumentException($"Property {name} not found in {type}.");
    }
}

using System;
using System.Globalization;
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
        /// <returns>Property information.</returns>
        /// <exception cref="ArgumentNullException">Object or property name is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object.</exception>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        public static PropertyInfo GetPropertyInfo(this object obj, string name) => obj.GetObjectPropInfo(name).PropertyInfo;

        /// <summary>
        /// Sets the property value via its name.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to assign to property.</param>
        /// <exception cref="ArgumentNullException">Object or property name is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object.</exception>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        public static void SetPropertyValue(this object obj, string name, object value)
        {
            var info = obj.GetObjectPropInfo(name);
            var type = Nullable.GetUnderlyingType(info.PropertyInfo.PropertyType) ?? info.PropertyInfo.PropertyType;

            value = value is IConvertible ? Convert.ChangeType(value, type, CultureInfo.InvariantCulture) : value;

            info.PropertyInfo.SetValue(info.Obj, value);

            if (info.PropertyInfo.ReflectedType?.IsValueType ?? false)
            {
                var index = name.LastIndexOf('.');

                if (index > 0)
                {
                    name = name.Substring(0, index);
                    obj.SetPropertyValue(name, info.Obj);
                }
            }
        }

        /// <summary>
        /// Gets the property value via its name.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="name">Property name.</param>
        /// <returns>Property value.</returns>
        /// <exception cref="ArgumentNullException">Object or property name is null.</exception>
        /// <exception cref="ArgumentException">Property is not found in object.</exception>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        /// <exception cref="InvalidCastException">Property value cannot be casted to expected return type.</exception>
        public static T GetPropertyValue<T>(this object obj, string name)
        {
            var info = obj.GetObjectPropInfo(name);
            return (T)info.PropertyInfo.GetValue(info.Obj);
        }

        private static (object Obj, PropertyInfo PropertyInfo) GetObjectPropInfo(this object obj, string name)
        {
            var type = obj?.GetType() ?? throw new ArgumentNullException(nameof(obj));

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo propertyInfo;

            if (name.Contains('.', StringComparison.InvariantCulture))
            {
                var nestedObjectInfo = obj.GetNestedObjectPropInfo(type, name);
                propertyInfo = nestedObjectInfo.PropertyInfo;
                obj = nestedObjectInfo.Obj;
            }
            else
            {
                propertyInfo = type.GetTypePropInfo(name);
            }

            return (obj, propertyInfo);
        }

        private static (object Obj, PropertyInfo PropertyInfo) GetNestedObjectPropInfo(this object obj, Type type, string name)
        {
            var nestedNames = name.Split('.', StringSplitOptions.RemoveEmptyEntries);

            PropertyInfo propertyInfo = null;

            for (var i = 0; i < nestedNames.Length; i++)
            {
                propertyInfo = type.GetTypePropInfo(nestedNames[i]);

                if (i != nestedNames.Length - 1)
                {
                    type = propertyInfo.PropertyType;
                    obj = propertyInfo.GetValue(obj);
                }
            }

            return (obj, propertyInfo);
        }

        private static PropertyInfo GetTypePropInfo(this Type type, string name) => type.GetProperty(name) ?? throw new ArgumentException($"Property {name} not found in {type}.");
    }
}

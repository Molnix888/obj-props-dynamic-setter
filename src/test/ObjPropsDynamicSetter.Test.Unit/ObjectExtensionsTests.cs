using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using ObjPropsDynamicSetter.Test.Unit.Models;

namespace ObjPropsDynamicSetter.Test.Unit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ObjectExtensionsTests
    {
        [Test]
        public void GetPropertyInfoDefaultFlagsSuccess()
        {
            const string propertyName = "NestedTestStruct";
            var obj = GetTestStruct();
            Assert.That(obj.GetPropertyInfo(propertyName), Is.EqualTo(obj.GetType().GetProperty(propertyName)));
        }

        [Test]
        [TestCaseSource(nameof(GetValidDataForGetPropertyInfo))]
        public void GetPropertyInfoSuccess(object obj, string propertyName, Type type, bool includeNonPublic) =>
            Assert.That(obj.GetPropertyInfo(propertyName, includeNonPublic).PropertyType, Is.EqualTo(type));

        [Test]
        [TestCaseSource(nameof(GetRequiredDataInvalidCommon))]
        public void GetPropertyInfoThrowsException(object obj, string propertyName, ExactTypeConstraint throwsException) =>
            Assert.That(() => obj.GetPropertyInfo(propertyName), throwsException);

        [Test]
        public void GetPropertyValueDefaultFlagsSuccess()
        {
            var obj = GetTestStruct();
            Assert.That(obj.GetPropertyValue<NestedTestClass>("NestedTestClass"), Is.EqualTo(new NestedTestClass()));
        }

        [Test]
        [TestCaseSource(nameof(GetValidDataForGetValue))]
        public void GetPropertyValueSuccess(object obj, string propertyName, object value, bool includeNonPublic) =>
            Assert.That(obj.GetPropertyValue<object>(propertyName, includeNonPublic), Is.EqualTo(value));

        [Test]
        [TestCaseSource(nameof(GetRequiredDataInvalidCommon))]
        public void GetPropertyValueThrowsException(object obj, string propertyName, ExactTypeConstraint throwsException) =>
            Assert.That(() => obj.GetPropertyValue<object>(propertyName), throwsException);

        [Test]
        public void GetPropertyValueThrowsInvalidCastException()
        {
            var obj = new TestClass();
            Assert.That(() => obj.GetPropertyValue<TestStruct>("CharValue"), Throws.TypeOf<InvalidCastException>());
        }

        [Test]
        [TestCaseSource(nameof(GetValidTestClassDataForSetValue))]
        public void SetPropertyValueTestClassSuccess(string propertyName, object value, bool includeNonPublic)
        {
            var obj = new TestClass();
            _ = obj.SetPropertyValue<TestClass>(propertyName, value, includeNonPublic);
            Assert.That(obj.GetPropertyValue<object>(propertyName, includeNonPublic), Is.EqualTo(value));
        }

        [Test]
        [TestCaseSource(nameof(GetValidTestStructDataForSetValue))]
        public void SetPropertyValueTestStructDefaultFlagsSuccess(string propertyName, object value)
        {
            var obj = GetTestStruct();
            obj = obj.SetPropertyValue<TestStruct>(propertyName, value);
            Assert.That(obj.GetPropertyValue<object>(propertyName), Is.EqualTo(value));
        }

        [Test]
        [TestCaseSource(nameof(GetRequiredDataInvalidCommon))]
        [TestCaseSource(nameof(GetRequiredDataInvalidForSetValue))]
        public void SetPropertyValueThrowsException(object obj, string propertyName, ExactTypeConstraint throwsException)
        {
            const int value = 1000;
            Assert.That(() => obj.SetPropertyValue<object>(propertyName, value), throwsException);
        }

        private static IEnumerable<object[]> GetValidDataForGetPropertyInfo()
        {
            yield return new object[] { new TestClass(), "ObjectValue", typeof(object), false };
            yield return new object[] { new TestClass(), "BoolValue", typeof(bool), false };
            yield return new object[] { new TestClass(), "ByteValue", typeof(byte), false };
            yield return new object[] { new TestClass(), "SByteValue", typeof(sbyte), true };
            yield return new object[] { new TestClass(), "CharValue", typeof(char), false };
            yield return new object[] { new TestClass(), "StringValue", typeof(string), false };
            yield return new object[] { new TestClass(), "UIntValue", typeof(uint), false };
            yield return new object[] { new TestClass(), "LongValue", typeof(long), false };
            yield return new object[] { new TestClass(), "ULongValue", typeof(ulong), false };
            yield return new object[] { new TestClass(), "UShortValue", typeof(ushort), false };
            yield return new object[] { new TestClass(), "DecimalValue", typeof(decimal), false };
            yield return new object[] { new TestClass(), "DoubleValue", typeof(double), false };
            yield return new object[] { new TestClass(), "FloatValue", typeof(float), false };
            yield return new object[] { new TestClass(), "DynamicValue", typeof(object), false };
            yield return new object[] { new TestClass(), "NullableIntValue", typeof(int?), false };
            yield return new object[] { new TestClass(), "NullableDateTimeValue", typeof(DateTime?), true };
            yield return new object[] { new TestClass(), "EnumValue", typeof(TestEnumeration), false };
            yield return new object[] { new TestClass(), "TestStruct", typeof(TestStruct), false };
            yield return new object[] { new TestClass(), "TestStruct.ByteValue", typeof(byte), false };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestClass", typeof(NestedTestClass), false };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestClass.NestedStringValue", typeof(string), false };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestClass.PrivateIntValue", typeof(int), true };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestStruct", typeof(NestedTestStruct), true };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestStruct.NestedCharValue", typeof(char), false };
            yield return new object[] { new TestClass(), "ProtectedNestedTestClass.NestedStringValue", typeof(string), true };
            yield return new object[] { new TestClass(), "IntCollectionValue", typeof(IEnumerable<int>), false };
            yield return new object[] { new TestClass(), "StaticIntValue", typeof(int), false };
            yield return new object[] { new TestClass(), "InternalShortValue", typeof(short), true };
            yield return new object[] { GetTestStruct(), "NestedTestClass", typeof(NestedTestClass), false };
        }

        private static IEnumerable<object[]> GetValidDataForGetValue()
        {
            yield return new object[] { new TestClass(), "ObjectValue", true, false };
            yield return new object[] { new TestClass(), "BoolValue", false, false };
            yield return new object[] { new TestClass(), "ByteValue", 3, false };
            yield return new object[] { new TestClass(), "SByteValue", -5, true };
            yield return new object[] { new TestClass(), "CharValue", 'r', false };
            yield return new object[] { new TestClass(), "StringValue", "something", false };
            yield return new object[] { new TestClass(), "UIntValue", 6475457, false };
            yield return new object[] { new TestClass(), "LongValue", -748978967867, false };
            yield return new object[] { new TestClass(), "ULongValue", 573242342423, false };
            yield return new object[] { new TestClass(), "UShortValue", 16534, false };
            yield return new object[] { new TestClass(), "DecimalValue", 456.656544654644m, false };
            yield return new object[] { new TestClass(), "DoubleValue", 3453453.345436575d, false };
            yield return new object[] { new TestClass(), "FloatValue", 4355464.64f, false };
            yield return new object[] { new TestClass(), "DynamicValue", "dynamic", false };
            yield return new object[] { new TestClass(), "NullableIntValue", 347, false };
            yield return new object[] { new TestClass(), "NullableDateTimeValue", DateTime.Parse("1886-11-03", CultureInfo.InvariantCulture), true };
            yield return new object[] { new TestClass(), "EnumValue", TestEnumeration.First, false };
            yield return new object[] { new TestClass(), "TestStruct", new TestStruct { IntValue = 27, ByteValue = 2, NestedTestClass = new NestedTestClass(), NestedTestStruct = new NestedTestStruct { NestedCharValue = 'k' } }, false };
            yield return new object[] { new TestClass(), "TestStruct.ByteValue", 2, false };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestClass", new NestedTestClass(), false };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestClass.NestedStringValue", "foo", false };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestClass.PrivateIntValue", 7, true };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestStruct", new NestedTestStruct { NestedCharValue = 'k' }, true };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestStruct.NestedCharValue", 'k', false };
            yield return new object[] { new TestClass(), "ProtectedNestedTestClass.NestedStringValue", "foo", true };
            yield return new object[] { new TestClass(), "IntCollectionValue", new List<int> { 1, 5, 6 }, false };
            yield return new object[] { new TestClass(), "StaticIntValue", 5, false };
            yield return new object[] { new TestClass(), "InternalShortValue", -1654, true };
            yield return new object[] { GetTestStruct(), "NestedTestStruct", new NestedTestStruct { NestedCharValue = 'c' }, false };
        }

        private static IEnumerable<object[]> GetValidTestClassDataForSetValue()
        {
            yield return new object[] { "ObjectValue", null, false };
            yield return new object[] { "ObjectValue", 56, false };
            yield return new object[] { "BoolValue", true, false };
            yield return new object[] { "ByteValue", 24, false };
            yield return new object[] { "SByteValue", 7, true };
            yield return new object[] { "CharValue", 'U', false };
            yield return new object[] { "StringValue", "test", false };
            yield return new object[] { "UIntValue", 14, false };
            yield return new object[] { "LongValue", 5786, false };
            yield return new object[] { "ULongValue", 72424, false };
            yield return new object[] { "UShortValue", 73, false };
            yield return new object[] { "DecimalValue", 56.45632423m, false };
            yield return new object[] { "DoubleValue", 82723.4563242d, false };
            yield return new object[] { "FloatValue", 875.34f, false };
            yield return new object[] { "DynamicValue", 56, false };
            yield return new object[] { "NullableIntValue", null, false };
            yield return new object[] { "NullableIntValue", 32, false };
            yield return new object[] { "NullableDateTimeValue", null, true };
            yield return new object[] { "NullableDateTimeValue", DateTime.Parse("1990-06-13", CultureInfo.InvariantCulture), false };
            yield return new object[] { "EnumValue", TestEnumeration.Second, false };
            yield return new object[] { "TestStruct", new TestStruct { IntValue = 12, ByteValue = 6, NestedTestClass = new NestedTestClass { NestedStringValue = "str" }, NestedTestStruct = new NestedTestStruct { NestedCharValue = 'l' } }, false };
            yield return new object[] { "TestStruct.ByteValue", 5, false };
            yield return new object[] { "TestStruct.NestedTestClass", new NestedTestClass { NestedStringValue = "internal string" }, false };
            yield return new object[] { "TestStruct.NestedTestClass.NestedStringValue", "some string", false };
            yield return new object[] { "TestStruct.NestedTestClass.PrivateIntValue", -78954, true };
            yield return new object[] { "TestStruct.NestedTestStruct", new NestedTestStruct { NestedCharValue = '!' }, true };
            yield return new object[] { "TestStruct.NestedTestStruct.NestedCharValue", ' ', false };
            yield return new object[] { "ProtectedNestedTestClass.PrivateIntValue", 8347, true };
            yield return new object[] { "IntCollectionValue", new[] { 9, 3 }, false };
            yield return new object[] { "StaticIntValue", 13, false };
            yield return new object[] { "InternalShortValue", 90, true };
        }

        private static IEnumerable<object[]> GetValidTestStructDataForSetValue()
        {
            yield return new object[] { "IntValue", 63 };
            yield return new object[] { "NestedTestClass", new NestedTestClass { NestedStringValue = "something" } };
            yield return new object[] { "NestedTestStruct", new NestedTestStruct { NestedCharValue = '/' } };
        }

        private static IEnumerable<object[]> GetRequiredDataInvalidCommon()
        {
            yield return new object[] { null, "test", Throws.ArgumentNullException };
            yield return new object[] { new TestClass(), null, Throws.ArgumentException };
            yield return new object[] { new TestClass(), string.Empty, Throws.ArgumentException };
            yield return new object[] { new TestClass(), " ", Throws.ArgumentException };
            yield return new object[] { new TestClass(), "Absent", Throws.ArgumentException };
            yield return new object[] { new TestClass(), "ObjectField", Throws.ArgumentException };
            yield return new object[] { new TestClass(), "TestEvent", Throws.ArgumentException };
            yield return new object[] { new TestClass(), "TestMethod", Throws.ArgumentException };
            yield return new object[] { new TestClass(), "InternalShortValue", Throws.ArgumentException };
            yield return new object[] { new TestClass(), "TestStruct.NestedTestClass.PrivateIntValue", Throws.ArgumentException };
            yield return new object[] { new TestClass(), "ProtectedNestedTestClass", Throws.ArgumentException };
            yield return new object[] { new TestClass(), "ProtectedNestedTestClass.PrivateIntValue", Throws.ArgumentException };
            yield return new object[] { GetTestStruct(), "Missing", Throws.ArgumentException };
        }

        private static IEnumerable<object[]> GetRequiredDataInvalidForSetValue()
        {
            yield return new object[] { GetTestStruct(), "NestedTestClass", Throws.TypeOf<InvalidCastException>() };
            yield return new object[] { new TestClass(), "ByteValue", Throws.TypeOf<OverflowException>() };
        }

        private static TestStruct GetTestStruct() => new TestStruct
        {
            IntValue = 875,
            ByteValue = 48,
            NestedTestClass = new NestedTestClass(),
            NestedTestStruct = new NestedTestStruct
            {
                NestedCharValue = 'c',
            },
        };
    }
}

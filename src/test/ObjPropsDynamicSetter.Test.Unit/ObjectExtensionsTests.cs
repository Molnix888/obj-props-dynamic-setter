using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using ObjPropsDynamicSetter.Test.Unit.Extensions;
using ObjPropsDynamicSetter.Test.Unit.ModelBuilders;
using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit
{
    [Parallelizable(ParallelScope.All)]
    public class ObjectExtensionsTests
    {
        private const string NestedTestStructValue = "NestedTestStruct";
        private const string NestedTestClassValue = "NestedTestClass";
        private const string ObjectValue = "ObjectValue";
        private const string CharValue = "CharValue";
        private const string IntValue = "IntValue";
        private const string ReadOnlyBoolValue = "ReadOnlyBoolValue";
        private const string BoolValue = "BoolValue";
        private const string ByteValue = "ByteValue";
        private const string SByteValue = "SByteValue";
        private const string StringValue = "StringValue";
        private const string UIntValue = "UIntValue";
        private const string LongValue = "LongValue";
        private const string ULongValue = "ULongValue";
        private const string UShortValue = "UShortValue";
        private const string DecimalValue = "DecimalValue";
        private const string DoubleValue = "DoubleValue";
        private const string FloatValue = "FloatValue";
        private const string DynamicValue = "DynamicValue";
        private const string NullableIntValue = "NullableIntValue";
        private const string NullableDateTimeValue = "NullableDateTimeValue";
        private const string EnumValue = "EnumValue";
        private const string TestStructValue = "TestStruct";
        private const string TestStructByteValue = "TestStruct.ByteValue";
        private const string TestStructNestedTestClassValue = "TestStruct.NestedTestClass";
        private const string TestStructNestedTestClassNestedStringValue = "TestStruct.NestedTestClass.NestedStringValue";
        private const string TestStructNestedTestClassPrivateIntValue = "TestStruct.NestedTestClass.PrivateIntValue";
        private const string TestStructNestedTestStructValue = "TestStruct.NestedTestStruct";
        private const string TestStructNestedTestStructNestedCharValue = "TestStruct.NestedTestStruct.NestedCharValue";
        private const string ProtectedNestedTestClassNestedStringValue = "ProtectedNestedTestClass.NestedStringValue";
        private const string IntCollectionValue = "IntCollectionValue";
        private const string StaticIntValue = "StaticIntValue";
        private const string InternalShortValue = "InternalShortValue";
        private const string TestRecordValue = "TestRecord";
        private const string ProtectedNestedTestClassPrivateIntValue = "ProtectedNestedTestClass.PrivateIntValue";

        private static readonly int ValueForPrivateInt = RandomUtil.Randomizer.Next();

        private static readonly NestedTestClass NestedTestClass = NestedTestClassBuilder.Build(RandomUtil.Randomizer.Next());
        private static readonly TestClass TestClass = TestClassBuilder.Build(ValueForPrivateInt, NestedTestClass);
        private static readonly TestStruct TestStruct = TestStructBuilder.Build(RandomUtil.Randomizer.Next());
        private static readonly TestRecord TestRecord = TestRecordBuilder.Build();

        [Test]
        public void GetPropertyInfoDefaultFlagsSuccess() => Assert.That(TestStruct.GetPropertyInfo(NestedTestStructValue), Is.EqualTo(TestStruct.GetType().GetProperty(NestedTestStructValue)));

        [Test]
        [TestCaseSource(nameof(GetValidDataForGetPropertyInfo))]
        public void GetPropertyInfoSuccess(object obj, string propertyName, Type type, bool includeNonPublic) => Assert.That(obj.GetPropertyInfo(propertyName, includeNonPublic).PropertyType, Is.EqualTo(type));

        [Test]
        [TestCaseSource(nameof(GetRequiredDataInvalidCommon))]
        public void GetPropertyInfoThrowsException(object obj, string propertyName, ExactTypeConstraint throwsException, string exceptionMessage) =>
            Assert.That(() => obj.GetPropertyInfo(propertyName), throwsException?.With.Message.EqualTo(exceptionMessage));

        [Test]
        public void GetPropertyValueDefaultFlagsSuccess() => Assert.That(TestStruct.GetPropertyValue(NestedTestClassValue) as NestedTestClass, Is.EqualTo(TestStruct.NestedTestClass));

        [Test]
        [TestCaseSource(nameof(GetValidDataForGetValue))]
        public void GetPropertyValueSuccess(object obj, string propertyName, object value, bool includeNonPublic) => Assert.That(obj.GetPropertyValue(propertyName, includeNonPublic), Is.EqualTo(value));

        [Test]
        [TestCaseSource(nameof(GetRequiredDataInvalidCommon))]
        public void GetPropertyValueThrowsException(object obj, string propertyName, ExactTypeConstraint throwsException, string exceptionMessage) =>
            Assert.That(() => obj.GetPropertyValue(propertyName), throwsException?.With.Message.EqualTo(exceptionMessage));

        [Test]
        [TestCaseSource(nameof(GetValidTestClassDataForSetValue))]
        public void SetPropertyValueTestClassSuccess(string propertyName, object value, bool includeNonPublic)
        {
            var obj = TestClassBuilder.Build(RandomUtil.Randomizer.Next(), NestedTestClassBuilder.Build(RandomUtil.Randomizer.Next()));
            _ = obj.SetPropertyValue(propertyName, value, includeNonPublic);
            Assert.That(obj.GetPropertyValue(propertyName, includeNonPublic), Is.EqualTo(value));
        }

        [Test]
        [TestCaseSource(nameof(GetValidTestStructDataForSetValue))]
        public void SetPropertyValueTestStructDefaultFlagsSuccess(string propertyName, object value)
        {
            var obj = TestStructBuilder.Build(RandomUtil.Randomizer.Next());
            obj = obj.SetPropertyValue(propertyName, value);
            Assert.That(obj.GetPropertyValue(propertyName), Is.EqualTo(value));
        }

        [Test]
        public void SetPropertyValueTestRecordSuccess()
        {
            var obj = TestRecordBuilder.Build();
            var value = RandomUtil.Randomizer.Next();
            _ = obj.SetPropertyValue(IntValue, value);
            Assert.That(obj.GetPropertyValue(IntValue), Is.EqualTo(value));
        }

        [Test]
        [TestCaseSource(nameof(GetRequiredDataInvalidCommon))]
        [TestCaseSource(nameof(GetRequiredDataInvalidForSetValue))]
        public void SetPropertyValueThrowsException(object obj, string propertyName, ExactTypeConstraint throwsException, string exceptionMessage) =>
            Assert.That(() => obj.SetPropertyValue(propertyName, RandomUtil.Randomizer.Next(256, int.MaxValue)), throwsException?.With.Message.EqualTo(exceptionMessage));

        private static IEnumerable<object[]> GetValidDataForGetPropertyInfo()
        {
            yield return new object[] { TestClass, ReadOnlyBoolValue, typeof(bool), false };
            yield return new object[] { TestClass, ObjectValue, typeof(object), false };
            yield return new object[] { TestClass, BoolValue, typeof(bool), false };
            yield return new object[] { TestClass, ByteValue, typeof(byte), false };
            yield return new object[] { TestClass, SByteValue, typeof(sbyte), true };
            yield return new object[] { TestClass, CharValue, typeof(char), false };
            yield return new object[] { TestClass, StringValue, typeof(string), false };
            yield return new object[] { TestClass, UIntValue, typeof(uint), false };
            yield return new object[] { TestClass, LongValue, typeof(long), false };
            yield return new object[] { TestClass, ULongValue, typeof(ulong), false };
            yield return new object[] { TestClass, UShortValue, typeof(ushort), false };
            yield return new object[] { TestClass, DecimalValue, typeof(decimal), false };
            yield return new object[] { TestClass, DoubleValue, typeof(double), false };
            yield return new object[] { TestClass, FloatValue, typeof(float), false };
            yield return new object[] { TestClass, DynamicValue, typeof(object), false };
            yield return new object[] { TestClass, NullableIntValue, typeof(int?), false };
            yield return new object[] { TestClass, NullableDateTimeValue, typeof(DateTime?), true };
            yield return new object[] { TestClass, EnumValue, typeof(TestEnumeration), false };
            yield return new object[] { TestClass, TestStructValue, typeof(TestStruct), false };
            yield return new object[] { TestClass, TestStructByteValue, typeof(byte), false };
            yield return new object[] { TestClass, TestStructNestedTestClassValue, typeof(NestedTestClass), false };
            yield return new object[] { TestClass, TestStructNestedTestClassNestedStringValue, typeof(string), false };
            yield return new object[] { TestClass, TestStructNestedTestClassPrivateIntValue, typeof(int), true };
            yield return new object[] { TestClass, TestStructNestedTestStructValue, typeof(NestedTestStruct), true };
            yield return new object[] { TestClass, TestStructNestedTestStructNestedCharValue, typeof(char), false };
            yield return new object[] { TestClass, ProtectedNestedTestClassNestedStringValue, typeof(string), true };
            yield return new object[] { TestClass, IntCollectionValue, typeof(IEnumerable<int>), false };
            yield return new object[] { TestClass, StaticIntValue, typeof(int), false };
            yield return new object[] { TestClass, InternalShortValue, typeof(short), true };
            yield return new object[] { TestClass, TestRecordValue, typeof(TestRecord), false };
            yield return new object[] { TestStruct, NestedTestClassValue, typeof(NestedTestClass), false };
            yield return new object[] { TestRecord, StringValue, typeof(string), false };
        }

        private static IEnumerable<object[]> GetValidDataForGetValue()
        {
            yield return new object[] { TestClass, ReadOnlyBoolValue, TestClass.ReadOnlyBoolValue, false };
            yield return new object[] { TestClass, ObjectValue, TestClass.ObjectValue, false };
            yield return new object[] { TestClass, BoolValue, TestClass.BoolValue, false };
            yield return new object[] { TestClass, ByteValue, TestClass.ByteValue, false };
            yield return new object[] { TestClass, SByteValue, TestClass.SByteValue, true };
            yield return new object[] { TestClass, CharValue, TestClass.CharValue, false };
            yield return new object[] { TestClass, StringValue, TestClass.StringValue, false };
            yield return new object[] { TestClass, UIntValue, TestClass.UIntValue, false };
            yield return new object[] { TestClass, LongValue, TestClass.LongValue, false };
            yield return new object[] { TestClass, ULongValue, TestClass.ULongValue, false };
            yield return new object[] { TestClass, UShortValue, TestClass.UShortValue, false };
            yield return new object[] { TestClass, DecimalValue, TestClass.DecimalValue, false };
            yield return new object[] { TestClass, DoubleValue, TestClass.DoubleValue, false };
            yield return new object[] { TestClass, FloatValue, TestClass.FloatValue, false };
            yield return new object[] { TestClass, DynamicValue, TestClass.DynamicValue, false };
            yield return new object[] { TestClass, NullableIntValue, TestClass.NullableIntValue, false };
            yield return new object[] { TestClass, NullableDateTimeValue, TestClass.NullableDateTimeValue, true };
            yield return new object[] { TestClass, EnumValue, TestClass.EnumValue, false };
            yield return new object[] { TestClass, TestStructValue, TestClass.TestStruct, false };
            yield return new object[] { TestClass, TestStructByteValue, TestClass.TestStruct.ByteValue, false };
            yield return new object[] { TestClass, TestStructNestedTestClassValue, TestClass.TestStruct.NestedTestClass, false };
            yield return new object[] { TestClass, TestStructNestedTestClassNestedStringValue, TestClass.TestStruct.NestedTestClass.NestedStringValue, false };
            yield return new object[] { TestClass, TestStructNestedTestClassPrivateIntValue, ValueForPrivateInt, true };
            yield return new object[] { TestClass, TestStructNestedTestStructValue, TestClass.TestStruct.NestedTestStruct, true };
            yield return new object[] { TestClass, TestStructNestedTestStructNestedCharValue, TestClass.TestStruct.NestedTestStruct.NestedCharValue, false };
            yield return new object[] { TestClass, ProtectedNestedTestClassNestedStringValue, NestedTestClass.NestedStringValue, true };
            yield return new object[] { TestClass, IntCollectionValue, TestClass.IntCollectionValue, false };
            yield return new object[] { TestClass, StaticIntValue, TestClass.StaticIntValue, false };
            yield return new object[] { TestClass, InternalShortValue, TestClass.InternalShortValue, true };
            yield return new object[] { TestClass, TestRecordValue, TestClass.TestRecord, false };
            yield return new object[] { TestStruct, NestedTestStructValue, TestStruct.NestedTestStruct, false };
            yield return new object[] { TestRecord, IntValue, TestRecord.IntValue, false };
        }

        private static IEnumerable<object[]> GetValidTestClassDataForSetValue()
        {
            yield return new object[] { ObjectValue, null, false };
            yield return new object[] { ObjectValue, RandomUtil.Randomizer.Next(), false };
            yield return new object[] { BoolValue, RandomUtil.Randomizer.NextBool(), false };
            yield return new object[] { ByteValue, RandomUtil.Randomizer.NextByte(), false };
            yield return new object[] { SByteValue, RandomUtil.Randomizer.NextSByte(), true };
            yield return new object[] { CharValue, RandomUtil.Randomizer.NextChar(), false };
            yield return new object[] { StringValue, RandomUtil.Randomizer.GetString(), false };
            yield return new object[] { UIntValue, RandomUtil.Randomizer.NextUInt(), false };
            yield return new object[] { LongValue, RandomUtil.Randomizer.NextLong(), false };
            yield return new object[] { ULongValue, RandomUtil.Randomizer.NextULong(), false };
            yield return new object[] { UShortValue, RandomUtil.Randomizer.NextUShort(), false };
            yield return new object[] { DecimalValue, RandomUtil.Randomizer.NextDecimal(), false };
            yield return new object[] { DoubleValue, RandomUtil.Randomizer.NextDouble(), false };
            yield return new object[] { FloatValue, RandomUtil.Randomizer.NextFloat(), false };
            yield return new object[] { DynamicValue, RandomUtil.Randomizer.NextBool(), false };
            yield return new object[] { NullableIntValue, null, false };
            yield return new object[] { NullableIntValue, RandomUtil.Randomizer.Next(), false };
            yield return new object[] { NullableDateTimeValue, null, true };
            yield return new object[] { NullableDateTimeValue, DateTime.Now, false };
            yield return new object[] { EnumValue, RandomUtil.Randomizer.NextEnum<TestEnumeration>(), false };
            yield return new object[] { TestStructValue, TestStructBuilder.Build(RandomUtil.Randomizer.Next()), false };
            yield return new object[] { TestStructByteValue, RandomUtil.Randomizer.NextByte(), false };
            yield return new object[] { TestStructNestedTestClassValue, NestedTestClassBuilder.Build(RandomUtil.Randomizer.Next()), false };
            yield return new object[] { TestStructNestedTestClassNestedStringValue, RandomUtil.Randomizer.GetString(), false };
            yield return new object[] { TestStructNestedTestClassPrivateIntValue, RandomUtil.Randomizer.Next(), true };
            yield return new object[] { TestStructNestedTestStructValue, NestedTestStructBuilder.Build(), true };
            yield return new object[] { TestStructNestedTestStructNestedCharValue, RandomUtil.Randomizer.NextChar(), false };
            yield return new object[] { ProtectedNestedTestClassPrivateIntValue, RandomUtil.Randomizer.Next(), true };
            yield return new object[] { IntCollectionValue, new[] { RandomUtil.Randomizer.Next(), RandomUtil.Randomizer.Next() }, false };
            yield return new object[] { StaticIntValue, RandomUtil.Randomizer.Next(), false };
            yield return new object[] { InternalShortValue, RandomUtil.Randomizer.NextShort(), true };
            yield return new object[] { TestRecordValue, TestRecordBuilder.Build(), false };
        }

        private static IEnumerable<object[]> GetValidTestStructDataForSetValue()
        {
            yield return new object[] { IntValue, RandomUtil.Randomizer.Next() };
            yield return new object[] { NestedTestClassValue, NestedTestClassBuilder.Build(RandomUtil.Randomizer.Next()) };
            yield return new object[] { NestedTestStructValue, NestedTestStructBuilder.Build() };
        }

        private static IEnumerable<object[]> GetRequiredDataInvalidCommon()
        {
            const string nameExceptionMessage = "Property name cannot be null, empty or whitespace.";
            var notExistingProperty = RandomUtil.Randomizer.GetString();

            yield return new object[] { null, notExistingProperty, Throws.ArgumentNullException, "Value cannot be null. (Parameter 'obj')" };
            yield return new object[] { TestClass, null, Throws.ArgumentException, nameExceptionMessage };
            yield return new object[] { TestClass, string.Empty, Throws.ArgumentException, nameExceptionMessage };
            yield return new object[] { TestClass, " ", Throws.ArgumentException, nameExceptionMessage };
            yield return new object[] { TestClass, notExistingProperty, Throws.ArgumentException, $"Property {notExistingProperty} not found in ObjPropsDynamicSetter.Test.Unit.Models.TestClass." };
            yield return new object[] { TestClass, "TestField", Throws.ArgumentException, "Property TestField not found in ObjPropsDynamicSetter.Test.Unit.Models.TestClass." };
            yield return new object[] { TestClass, "TestEvent", Throws.ArgumentException, "Property TestEvent not found in ObjPropsDynamicSetter.Test.Unit.Models.TestClass." };
            yield return new object[] { TestClass, "TestMethod", Throws.ArgumentException, "Property TestMethod not found in ObjPropsDynamicSetter.Test.Unit.Models.TestClass." };
            yield return new object[] { TestClass, InternalShortValue, Throws.ArgumentException, "Property InternalShortValue not found in ObjPropsDynamicSetter.Test.Unit.Models.TestClass." };
            yield return new object[] { TestClass, TestStructNestedTestClassPrivateIntValue, Throws.ArgumentException, "Property PrivateIntValue not found in ObjPropsDynamicSetter.Test.Unit.Models.NestedTestClass." };
            yield return new object[] { TestClass, "ProtectedNestedTestClass", Throws.ArgumentException, "Property ProtectedNestedTestClass not found in ObjPropsDynamicSetter.Test.Unit.Models.TestClass." };
            yield return new object[] { TestClass, ProtectedNestedTestClassPrivateIntValue, Throws.ArgumentException, "Property ProtectedNestedTestClass not found in ObjPropsDynamicSetter.Test.Unit.Models.TestClass." };
            yield return new object[] { TestStruct, notExistingProperty, Throws.ArgumentException, $"Property {notExistingProperty} not found in ObjPropsDynamicSetter.Test.Unit.Models.TestStruct." };
            yield return new object[] { TestRecord, notExistingProperty, Throws.ArgumentException, $"Property {notExistingProperty} not found in ObjPropsDynamicSetter.Test.Unit.Models.TestRecord." };
        }

        private static IEnumerable<object[]> GetRequiredDataInvalidForSetValue()
        {
            yield return new object[] { TestStruct, NestedTestClassValue, Throws.TypeOf<InvalidCastException>(), "Invalid cast from 'System.Int32' to 'ObjPropsDynamicSetter.Test.Unit.Models.NestedTestClass'." };
            yield return new object[] { TestClass, ByteValue, Throws.TypeOf<OverflowException>(), "Value was either too large or too small for an unsigned byte." };
            yield return new object[] { TestClass, ReadOnlyBoolValue, Throws.ArgumentException, "Property set method not found." };
        }
    }
}

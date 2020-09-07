using System;
using System.Collections.Generic;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using ObjPropsDynamicSetter.Test.Unit.Models;

namespace ObjPropsDynamicSetter.Test.Unit
{
    /// <summary>
    /// Contains tests for object extension methods.
    /// </summary>
    [TestFixture]
    public class ObjectExtensionsTests
    {
        /// <summary>
        /// Tests SetPropertyValue extension method for class model.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">Value to assign to property.</param>
        [Test]
        [TestCaseSource(nameof(GetValidTestClassData))]
        public void SetPropertyValueTestClassSuccess(string propertyName, object value)
        {
            var obj = GetTestClass();
            obj.SetPropertyValue(propertyName, value);
            _ = obj.GetPropertyValue<object>(propertyName).Should().Be(value);
        }

        /// <summary>
        /// Tests SetPropertyValue extension method for struct model.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">Value to assign to property.</param>
        [Test]
        [TestCaseSource(nameof(GetValidTestStructData))]
        public void SetPropertyValueTestStructSuccess(string propertyName, object value)
        {
            var obj = GetTestStruct();
            var expected = obj.GetPropertyValue<object>(propertyName);
            obj.SetPropertyValue(propertyName, value);
            _ = obj.GetPropertyValue<object>(propertyName).Should().Be(expected);
        }

        private static IEnumerable<object[]> GetValidTestClassData()
        {
            yield return new object[] { "ObjectValue", null };
            yield return new object[] { "ObjectValue", 56 };
            yield return new object[] { "BoolValue", true };
            yield return new object[] { "ByteValue", 24 };
            yield return new object[] { "SByteValue", 7 };
            yield return new object[] { "CharValue", 'U' };
            yield return new object[] { "StringValue", "test" };
            yield return new object[] { "IntValue", 583 };
            yield return new object[] { "UIntValue", 14 };
            yield return new object[] { "LongValue", 5786 };
            yield return new object[] { "ULongValue", 72424 };
            yield return new object[] { "ShortValue", 90 };
            yield return new object[] { "UShortValue", 73 };
            yield return new object[] { "DecimalValue", 56.45632423m };
            yield return new object[] { "DoubleValue", 82723.4563242d };
            yield return new object[] { "FloatValue", 875.34f };
            yield return new object[] { "DynamicValue", 56 };
            yield return new object[] { "NullableIntValue", null };
            yield return new object[] { "NullableIntValue", 32 };
            yield return new object[] { "NullableDateTimeValue", null };
            yield return new object[] { "NullableDateTimeValue", DateTime.Parse("1990-06-13", CultureInfo.InvariantCulture) };
            yield return new object[] { "EnumValue", TestEnumeration.Second };
            yield return new object[] { "StructValue", new TestStruct { IntValue = 12, InternalByteValue = 6, InternalTestModel = new InternalTestClass { InternalStringValue = "str", IntValue = 1 } } };
            yield return new object[] { "StructValue.InternalByteValue", 5 };
            yield return new object[] { "StructValue.InternalTestModel", new InternalTestClass { InternalStringValue = "internal string", IntValue = 45 } };
            yield return new object[] { "StructValue.InternalTestModel.InternalStringValue", "some string" };
            yield return new object[] { "StructValue.InternalTestModel.IntValue", -78954 };
            yield return new object[] { "IntCollectionValue", new int[] { 9, 3 } };
        }

        private static IEnumerable<object[]> GetValidTestStructData()
        {
            yield return new object[] { "IntValue", 63 };
            yield return new object[] { "InternalTestModel", new InternalTestClass { InternalStringValue = "something", IntValue = -2 } };
        }

        private static TestClass GetTestClass() => new TestClass
        {
            ObjectValue = true,
            BoolValue = false,
            ByteValue = 3,
            SByteValue = -5,
            CharValue = 'r',
            StringValue = "something",
            IntValue = -765438,
            UIntValue = 6475457,
            LongValue = -748978967867,
            ULongValue = 573242342423,
            ShortValue = -1654,
            UShortValue = 16534,
            DecimalValue = 456.656544654644m,
            DoubleValue = 3453453.345436575d,
            FloatValue = 4355464.64f,
            DynamicValue = "dynamic",
            NullableIntValue = 347,
            NullableDateTimeValue = DateTime.Now,
            EnumValue = TestEnumeration.First,
            StructValue = new TestStruct
            {
                IntValue = 27,
                InternalByteValue = 2,
                InternalTestModel = new InternalTestClass
                {
                    InternalStringValue = "foo",
                    IntValue = 7,
                },
            },
            InternalTestModel = new InternalTestClass(),
            IntCollectionValue = new List<int> { 1, 5, 6 },
        };

        private static TestStruct GetTestStruct() => new TestStruct
        {
            IntValue = 875,
            InternalByteValue = 48,
            InternalTestModel = new InternalTestClass
            {
                InternalStringValue = "struct test",
                IntValue = -37789,
            },
        };
    }
}

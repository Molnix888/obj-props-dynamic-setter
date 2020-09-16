using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using ObjPropsDynamicSetter.Test.Unit.Models;

namespace ObjPropsDynamicSetter.Test.Unit
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        [TestCaseSource(nameof(GetValidTestClassData))]
        public void SetPropertyValueTestClassSuccess(string propertyName, object value)
        {
            var obj = GetTestClass();
            obj.SetPropertyValue(propertyName, value);
            Assert.That(obj.GetPropertyValue<object>(propertyName), Is.EqualTo(value));
        }

        [Test]
        [TestCaseSource(nameof(GetValidTestStructData))]
        public void SetPropertyValueTestStructSuccess(string propertyName, object value)
        {
            var obj = GetTestStruct();
            var expected = obj.GetPropertyValue<object>(propertyName);
            obj.SetPropertyValue(propertyName, value);
            Assert.That(obj.GetPropertyValue<object>(propertyName), Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(GetRequiredDataNull))]
        public void SetPropertyValueNullReferenceException(object obj, string propertyName)
        {
            var value = 1;
            Assert.That(() => obj.SetPropertyValue(propertyName, value), Throws.ArgumentNullException);
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
            yield return new object[] { "TestStruct", new TestStruct { IntValue = 12, InternalByteValue = 6, InternalTestClass = new InternalTestClass { InternalStringValue = "str", IntValue = 1 }, InternalTestStruct = new InternalTestStruct { InternalCharValue = 'l' } } };
            yield return new object[] { "TestStruct.InternalByteValue", 5 };
            yield return new object[] { "TestStruct.InternalTestClass", new InternalTestClass { InternalStringValue = "internal string", IntValue = 45 } };
            yield return new object[] { "TestStruct.InternalTestClass.InternalStringValue", "some string" };
            yield return new object[] { "TestStruct.InternalTestClass.IntValue", -78954 };
            yield return new object[] { "TestStruct.InternalTestStruct", new InternalTestStruct { InternalCharValue = '!' } };
            yield return new object[] { "TestStruct.InternalTestStruct.InternalCharValue", ' ' };
            yield return new object[] { "InternalTestClass", new InternalTestClass { InternalStringValue = "str1", IntValue = 9438 } };
            yield return new object[] { "InternalTestClass.InternalStringValue", "str2" };
            yield return new object[] { "IntCollectionValue", new int[] { 9, 3 } };
        }

        private static IEnumerable<object[]> GetValidTestStructData()
        {
            yield return new object[] { "IntValue", 63 };
            yield return new object[] { "InternalTestClass", new InternalTestClass { InternalStringValue = "something", IntValue = -2 } };
            yield return new object[] { "InternalTestStruct", new InternalTestStruct { InternalCharValue = '/' } };
        }

        private static IEnumerable<object[]> GetRequiredDataNull()
        {
            yield return new object[] { null, "test" };
            yield return new object[] { GetTestClass(), null };
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
            TestStruct = new TestStruct
            {
                IntValue = 27,
                InternalByteValue = 2,
                InternalTestClass = new InternalTestClass
                {
                    InternalStringValue = "foo",
                    IntValue = 7,
                },
                InternalTestStruct = new InternalTestStruct
                {
                    InternalCharValue = 'k',
                },
            },
            InternalTestClass = new InternalTestClass(),
            IntCollectionValue = new List<int> { 1, 5, 6 },
        };

        private static TestStruct GetTestStruct() => new TestStruct
        {
            IntValue = 875,
            InternalByteValue = 48,
            InternalTestClass = new InternalTestClass
            {
                InternalStringValue = "struct test",
                IntValue = -37789,
            },
            InternalTestStruct = new InternalTestStruct
            {
                InternalCharValue = 'c',
            },
        };
    }
}

using System;
using System.Collections.Generic;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public class TestClass
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1401 // Fields should be private
        public object TestField;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CA1051 // Do not declare visible instance fields

        public TestClass(bool readOnlyBoolValue, NestedTestClass protectedNestedTestClass)
        {
            ReadOnlyBoolValue = readOnlyBoolValue;
            ProtectedNestedTestClass = protectedNestedTestClass;
        }

#pragma warning disable S3264 // Events should be invoked
#pragma warning disable CS0067 // Events should be used
        public event EventHandler TestEvent;
#pragma warning restore CS0067 // Events should be used
#pragma warning restore S3264 // Events should be invoked

        public static int StaticIntValue { get; set; } = RandomUtil.Randomizer.Next();

        public bool ReadOnlyBoolValue { get; }

        public object ObjectValue { get; set; }

        public bool BoolValue { get; set; }

        public byte ByteValue { get; set; }

        public sbyte SByteValue { get; set; }

        public char CharValue { get; set; }

        public string StringValue { get; set; }

        public uint UIntValue { get; set; }

        public long LongValue { get; set; }

        public ulong ULongValue { get; init; }

        public ushort UShortValue { get; set; }

        public decimal DecimalValue { get; set; }

        public double DoubleValue { get; set; }

        public float FloatValue { get; set; }

        public dynamic DynamicValue { get; set; }

        public int? NullableIntValue { get; set; }

        public DateTime? NullableDateTimeValue { get; set; }

        public TestEnumeration EnumValue { get; set; }

        public TestStruct TestStruct { get; set; }

        public IEnumerable<int> IntCollectionValue { get; set; }

        public TestRecord TestRecord { get; set; }

        internal short InternalShortValue { get; set; }

        protected static NestedTestClass ProtectedNestedTestClass { get; set; }

        public void TestMethod() => TestField = default;
    }
}

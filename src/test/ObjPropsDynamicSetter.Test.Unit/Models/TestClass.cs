using System;
using System.Collections.Generic;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public class TestClass
    {
        public object ObjectValue { get; set; }

        public bool BoolValue { get; set; }

        public byte ByteValue { get; set; }

        public sbyte SByteValue { get; set; }

        public char CharValue { get; set; }

        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public uint UIntValue { get; set; }

        public long LongValue { get; set; }

        public ulong ULongValue { get; set; }

        public short ShortValue { get; set; }

        public ushort UShortValue { get; set; }

        public decimal DecimalValue { get; set; }

        public double DoubleValue { get; set; }

        public float FloatValue { get; set; }

        public dynamic DynamicValue { get; set; }

        public int? NullableIntValue { get; set; }

        public DateTime? NullableDateTimeValue { get; set; }

        public TestEnumeration EnumValue { get; set; }

        public TestStruct TestStruct { get; set; }

        public InternalTestClass InternalTestClass { get; set; }

        public IEnumerable<int> IntCollectionValue { get; set; }
    }
}

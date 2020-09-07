using System;
using System.Collections.Generic;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    /// <summary>
    /// Contains model for testing purposes.
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// Gets or sets object value.
        /// </summary>
        public object ObjectValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether test value is true or false.
        /// </summary>
        public bool BoolValue { get; set; }

        /// <summary>
        /// Gets or sets byte value.
        /// </summary>
        public byte ByteValue { get; set; }

        /// <summary>
        /// Gets or sets sbyte value.
        /// </summary>
        public sbyte SByteValue { get; set; }

        /// <summary>
        /// Gets or sets char value.
        /// </summary>
        public char CharValue { get; set; }

        /// <summary>
        /// Gets or sets string value.
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// Gets or sets int value.
        /// </summary>
        public int IntValue { get; set; }

        /// <summary>
        /// Gets or sets uint value.
        /// </summary>
        public uint UIntValue { get; set; }

        /// <summary>
        /// Gets or sets long value.
        /// </summary>
        public long LongValue { get; set; }

        /// <summary>
        /// Gets or sets ulong value.
        /// </summary>
        public ulong ULongValue { get; set; }

        /// <summary>
        /// Gets or sets short value.
        /// </summary>
        public short ShortValue { get; set; }

        /// <summary>
        /// Gets or sets ushort value.
        /// </summary>
        public ushort UShortValue { get; set; }

        /// <summary>
        /// Gets or sets decimal value.
        /// </summary>
        public decimal DecimalValue { get; set; }

        /// <summary>
        /// Gets or sets double value.
        /// </summary>
        public double DoubleValue { get; set; }

        /// <summary>
        /// Gets or sets float value.
        /// </summary>
        public float FloatValue { get; set; }

        /// <summary>
        /// Gets or sets dynamic value.
        /// </summary>
        public dynamic DynamicValue { get; set; }

        /// <summary>
        /// Gets or sets nullable int value.
        /// </summary>
        public int? NullableIntValue { get; set; }

        /// <summary>
        /// Gets or sets nullable DateTime value.
        /// </summary>
        public DateTime? NullableDateTimeValue { get; set; }

        /// <summary>
        /// Gets or sets enum value.
        /// </summary>
        public TestEnumeration EnumValue { get; set; }

        /// <summary>
        /// Gets or sets struct value.
        /// </summary>
        public TestStruct StructValue { get; set; }

        /// <summary>
        /// Gets or sets internal test model.
        /// </summary>
        public InternalTestClass InternalTestModel { get; set; }

        /// <summary>
        /// Gets or sets int collection value.
        /// </summary>
        public IEnumerable<int> IntCollectionValue { get; set; }
    }
}

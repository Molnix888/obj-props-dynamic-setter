using System;
using System.Collections.Generic;
using System.Globalization;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public class TestClass
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1401 // Fields should be private
        public object TestField = 1;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CA1051 // Do not declare visible instance fields

#pragma warning disable S3264 // Events should be invoked
#pragma warning disable CS0067 // Events should be used
        public event EventHandler TestEvent;
#pragma warning restore CS0067 // Events should be used
#pragma warning restore S3264 // Events should be invoked

        public static int StaticIntValue { get; set; } = 5;

        public object ObjectValue { get; set; } = true;

        public bool BoolValue { get; set; }

        public byte ByteValue { get; set; } = 3;

        public sbyte SByteValue { get; set; } = -5;

        public char CharValue { get; set; } = 'r';

        public string StringValue { get; set; } = "something";

        public uint UIntValue { get; set; } = 6475457;

        public long LongValue { get; set; } = -748978967867;

        public ulong ULongValue { get; set; } = 573242342423;

        public ushort UShortValue { get; set; } = 16534;

        public decimal DecimalValue { get; set; } = 456.656544654644m;

        public double DoubleValue { get; set; } = 3453453.345436575d;

        public float FloatValue { get; set; } = 4355464.64f;

        public dynamic DynamicValue { get; set; } = "dynamic";

        public int? NullableIntValue { get; set; } = 347;

        public DateTime? NullableDateTimeValue { get; set; } = DateTime.Parse("1886-11-03", CultureInfo.InvariantCulture);

        public TestEnumeration EnumValue { get; set; } = TestEnumeration.First;

        public TestStruct TestStruct { get; set; } = new TestStruct
        {
            IntValue = 27,
            ByteValue = 2,
            NestedTestClass = new NestedTestClass(),
            NestedTestStruct = new NestedTestStruct
            {
                NestedCharValue = 'k',
            },
        };

        public IEnumerable<int> IntCollectionValue { get; set; } = new List<int> { 1, 5, 6 };

        internal short InternalShortValue { get; set; } = -1654;

        protected static NestedTestClass ProtectedNestedTestClass { get; set; } = new NestedTestClass();

#pragma warning disable CA1822 // Mark members as static
        public void TestMethod()
#pragma warning restore CA1822 // Mark members as static
        {
            // Intentionally empty
        }
    }
}

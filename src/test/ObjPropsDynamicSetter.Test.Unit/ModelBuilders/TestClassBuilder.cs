using System;
using System.Collections.Generic;
using ObjPropsDynamicSetter.Test.Unit.Extensions;
using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.ModelBuilders
{
    public static class TestClassBuilder
    {
        public static TestClass Build() => new()
        {
            TestField = RandomUtil.Randomizer.Next(),
            ObjectValue = RandomUtil.Randomizer.NextBool(),
            ByteValue = RandomUtil.Randomizer.NextByte(),
            SByteValue = RandomUtil.Randomizer.NextSByte(),
            CharValue = RandomUtil.Randomizer.NextChar(),
            StringValue = RandomUtil.Randomizer.GetString(),
            UIntValue = RandomUtil.Randomizer.NextUInt(),
            LongValue = RandomUtil.Randomizer.NextLong(),
            ULongValue = RandomUtil.Randomizer.NextULong(),
            UShortValue = RandomUtil.Randomizer.NextUShort(),
            DecimalValue = RandomUtil.Randomizer.NextDecimal(),
            DoubleValue = RandomUtil.Randomizer.NextDouble(),
            FloatValue = RandomUtil.Randomizer.NextFloat(),
            DynamicValue = RandomUtil.Randomizer.GetString(),
            NullableIntValue = RandomUtil.Randomizer.Next(),
            NullableDateTimeValue = DateTime.Today,
            EnumValue = RandomUtil.Randomizer.NextEnum<TestEnumeration>(),
            TestStruct = TestStructBuilder.Build(),
            IntCollectionValue = new List<int> { RandomUtil.Randomizer.Next(), RandomUtil.Randomizer.Next(), RandomUtil.Randomizer.Next() },
            TestRecord = TestRecordBuilder.Build(),
            InternalShortValue = RandomUtil.Randomizer.NextShort(),
        };
    }
}

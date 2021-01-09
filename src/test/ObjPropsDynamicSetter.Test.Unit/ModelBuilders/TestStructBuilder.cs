using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.ModelBuilders
{
    public static class TestStructBuilder
    {
        public static TestStruct Build() => new()
        {
            IntValue = RandomUtil.Randomizer.Next(),
            ByteValue = RandomUtil.Randomizer.NextByte(),
            NestedTestClass = NestedTestClassBuilder.Build(),
            NestedTestStruct = NestedTestStructBuilder.Build(),
        };
    }
}

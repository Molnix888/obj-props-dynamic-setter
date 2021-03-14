using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.ModelBuilders
{
    internal static class TestStructBuilder
    {
        internal static TestStruct Build(int nestedTestClassPrivateIntValue) => new()
        {
            IntValue = RandomUtil.Randomizer.Next(),
            ByteValue = RandomUtil.Randomizer.NextByte(),
            NestedTestClass = NestedTestClassBuilder.Build(nestedTestClassPrivateIntValue),
            NestedTestStruct = NestedTestStructBuilder.Build(),
        };
    }
}

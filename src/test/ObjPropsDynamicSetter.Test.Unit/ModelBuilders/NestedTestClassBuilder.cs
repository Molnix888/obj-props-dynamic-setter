using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.ModelBuilders
{
    public static class NestedTestClassBuilder
    {
        public static NestedTestClass Build() => new NestedTestClass
        {
            NestedStringValue = RandomUtil.Randomizer.GetString(),
        };
    }
}

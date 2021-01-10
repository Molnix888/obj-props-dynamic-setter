using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.ModelBuilders
{
    internal static class NestedTestClassBuilder
    {
        internal static NestedTestClass Build() => new()
        {
            NestedStringValue = RandomUtil.Randomizer.GetString(),
        };
    }
}

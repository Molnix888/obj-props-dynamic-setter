using ObjPropsDynamicSetter.Test.Unit.Extensions;
using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.ModelBuilders
{
    public static class NestedTestStructBuilder
    {
        public static NestedTestStruct Build() => new()
        {
            NestedCharValue = RandomUtil.Randomizer.NextChar(),
        };
    }
}

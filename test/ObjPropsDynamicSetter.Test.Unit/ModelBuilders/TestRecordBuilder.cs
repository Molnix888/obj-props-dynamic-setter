using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.ModelBuilders
{
    internal static class TestRecordBuilder
    {
        internal static TestRecord Build() => new(RandomUtil.Randomizer.GetString(), RandomUtil.Randomizer.Next());
    }
}

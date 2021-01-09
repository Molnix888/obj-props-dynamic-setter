using ObjPropsDynamicSetter.Test.Unit.Models;
using ObjPropsDynamicSetter.Test.Unit.Utils;

namespace ObjPropsDynamicSetter.Test.Unit.ModelBuilders
{
    public static class TestRecordBuilder
    {
        public static TestRecord Build() => new(RandomUtil.Randomizer.GetString(), RandomUtil.Randomizer.Next());
    }
}

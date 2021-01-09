using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public sealed class NestedTestClass
    {
        public string NestedStringValue { get; set; }

        private int PrivateIntValue { get; set; } = 778567;

        public override bool Equals(object obj) => obj is NestedTestClass nestedTestClass
            && NestedStringValue == nestedTestClass.NestedStringValue
            && PrivateIntValue == nestedTestClass.PrivateIntValue;

        public override int GetHashCode() => HashCode.Combine(NestedStringValue, PrivateIntValue);
    }
}

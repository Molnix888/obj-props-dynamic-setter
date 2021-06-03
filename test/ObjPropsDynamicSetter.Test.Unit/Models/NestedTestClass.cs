using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public sealed class NestedTestClass
    {
        public NestedTestClass(int privateIntValue) => PrivateIntValue = privateIntValue;

        public string NestedStringValue { get; set; }

        private int PrivateIntValue { get; set; }

        public override bool Equals(object obj) => ReferenceEquals(this, obj)
            || (obj is NestedTestClass nestedTestClass
            && NestedStringValue == nestedTestClass.NestedStringValue
            && PrivateIntValue == nestedTestClass.PrivateIntValue);

        public override int GetHashCode() => HashCode.Combine(NestedStringValue, PrivateIntValue);
    }
}

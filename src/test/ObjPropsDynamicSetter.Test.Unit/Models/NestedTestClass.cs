using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public sealed class NestedTestClass : IEquatable<NestedTestClass>
    {
        public string NestedStringValue { get; set; } = "foo";

        private int PrivateIntValue { get; set; } = 7;

        public bool Equals(NestedTestClass other) => NestedStringValue == other?.NestedStringValue && PrivateIntValue == other?.PrivateIntValue;

        public override bool Equals(object obj) => obj is NestedTestClass nestedTestClass && Equals(nestedTestClass);

        public override int GetHashCode() => HashCode.Combine(NestedStringValue, PrivateIntValue);
    }
}

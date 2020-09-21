using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public sealed class NestedTestClass : IEquatable<NestedTestClass>
    {
        public string InternalStringValue { get; set; } = "foo";

        private int IntValue { get; set; } = 7;

        public bool Equals(NestedTestClass other) => InternalStringValue == other?.InternalStringValue && IntValue == other?.IntValue;

        public override bool Equals(object obj) => obj is NestedTestClass @model && Equals(@model);

        public override int GetHashCode() => HashCode.Combine(InternalStringValue, IntValue);
    }
}

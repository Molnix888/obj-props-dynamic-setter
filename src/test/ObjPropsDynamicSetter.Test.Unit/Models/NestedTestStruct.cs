using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public struct NestedTestStruct : IEquatable<NestedTestStruct>
    {
        public char NestedCharValue { get; set; }

        public static bool operator ==(NestedTestStruct left, NestedTestStruct right) => left.Equals(right);

        public static bool operator !=(NestedTestStruct left, NestedTestStruct right) => !(left == right);

        public override int GetHashCode() => HashCode.Combine(NestedCharValue);

        public bool Equals(NestedTestStruct other) => NestedCharValue == other.NestedCharValue;

        public override bool Equals(object obj) => obj is NestedTestStruct nestedTestStruct && Equals(nestedTestStruct);
    }
}

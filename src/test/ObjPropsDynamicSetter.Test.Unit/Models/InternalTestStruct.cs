using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public struct InternalTestStruct : IEquatable<InternalTestStruct>
    {
        public char InternalCharValue { get; set; }

        public static bool operator ==(InternalTestStruct left, InternalTestStruct right) => left.Equals(right);

        public static bool operator !=(InternalTestStruct left, InternalTestStruct right) => !(left == right);

        public override int GetHashCode() => HashCode.Combine(InternalCharValue);

        public bool Equals(InternalTestStruct other) => InternalCharValue == other.InternalCharValue;

        public override bool Equals(object obj) => obj is InternalTestStruct @struct && Equals(@struct);
    }
}

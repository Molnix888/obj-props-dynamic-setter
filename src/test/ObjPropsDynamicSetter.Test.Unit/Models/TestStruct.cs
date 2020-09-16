using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public struct TestStruct : IEquatable<TestStruct>
    {
        public int IntValue { get; set; }

        public byte InternalByteValue { get; set; }

        public InternalTestClass InternalTestClass { get; set; }

        public InternalTestStruct InternalTestStruct { get; set; }

        public static bool operator ==(TestStruct left, TestStruct right) => left.Equals(right);

        public static bool operator !=(TestStruct left, TestStruct right) => !(left == right);

        public override int GetHashCode() => HashCode.Combine(IntValue, InternalByteValue, InternalTestClass, InternalTestStruct);

        public bool Equals(TestStruct other) => IntValue == other.IntValue
            && InternalByteValue == other.InternalByteValue
            && InternalTestClass.Equals(other.InternalTestClass)
            && InternalTestStruct == other.InternalTestStruct;

        public override bool Equals(object obj) => obj is TestStruct @struct && Equals(@struct);
    }
}

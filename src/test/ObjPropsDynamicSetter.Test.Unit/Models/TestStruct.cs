using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public struct TestStruct : IEquatable<TestStruct>
    {
        public int IntValue { get; set; }

        public byte ByteValue { get; set; }

        public NestedTestClass NestedTestClass { get; set; }

        public NestedTestStruct NestedTestStruct { get; set; }

        public static bool operator ==(TestStruct left, TestStruct right) => left.Equals(right);

        public static bool operator !=(TestStruct left, TestStruct right) => !(left == right);

        public override int GetHashCode() => HashCode.Combine(IntValue, ByteValue, NestedTestClass, NestedTestStruct);

        public bool Equals(TestStruct other) => IntValue == other.IntValue
            && ByteValue == other.ByteValue
            && NestedTestClass.Equals(other.NestedTestClass)
            && NestedTestStruct == other.NestedTestStruct;

        public override bool Equals(object obj) => obj is TestStruct testStruct && Equals(testStruct);
    }
}

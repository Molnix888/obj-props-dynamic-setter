using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    /// <summary>
    /// Contains struct for testing purposes.
    /// </summary>
    public struct TestStruct : IEquatable<TestStruct>
    {
        /// <summary>
        /// Gets or sets int value.
        /// </summary>
        public int IntValue { get; set; }

        /// <summary>
        /// Gets or sets byte value.
        /// </summary>
        public byte InternalByteValue { get; set; }

        /// <summary>
        /// Gets or sets internal test model.
        /// </summary>
        public InternalTestClass InternalTestModel { get; set; }

        /// <summary>
        /// Checks equality of TestStruct instances.
        /// </summary>
        /// <param name="left">TestStruct instance to compare.</param>
        /// <param name="right">TestStruct instance to compare to.</param>
        /// <returns>Whether TestStruct instances are equal.</returns>
        public static bool operator ==(TestStruct left, TestStruct right) => left.Equals(right);

        /// <summary>
        /// Checks inequality of TestStruct instances.
        /// </summary>
        /// <param name="left">TestStruct instance to compare.</param>
        /// <param name="right">TestStruct instance to compare to.</param>
        /// <returns>Whether TestStruct instances are not equal.</returns>
        public static bool operator !=(TestStruct left, TestStruct right) => !(left == right);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(IntValue, InternalByteValue, InternalTestModel);

        /// <inheritdoc/>
        public bool Equals(TestStruct other) => IntValue == other.IntValue && InternalByteValue == other.InternalByteValue && InternalTestModel.Equals(other.InternalTestModel);

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is TestStruct @struct && Equals(@struct);
    }
}

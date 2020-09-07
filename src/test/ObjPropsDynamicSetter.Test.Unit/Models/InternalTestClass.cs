using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    /// <summary>
    /// Contains model for testing purposes.
    /// </summary>
    public sealed class InternalTestClass : IEquatable<InternalTestClass>
    {
        /// <summary>
        /// Gets or sets string value.
        /// </summary>
        public string InternalStringValue { get; set; }

        /// <summary>
        /// Gets or sets int value.
        /// </summary>
        public int IntValue { get; set; }

        /// <inheritdoc/>
        public bool Equals(InternalTestClass other) => InternalStringValue == other?.InternalStringValue && IntValue == other?.IntValue;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is InternalTestClass @model && Equals(@model);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(InternalStringValue, IntValue);
    }
}

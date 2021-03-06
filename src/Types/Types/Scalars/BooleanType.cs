using System;
using HotChocolate.Language;

namespace HotChocolate.Types
{
    /// <summary>
    /// The Boolean scalar type represents true or false.
    /// Response formats should use a built‐in boolean type if supported;
    /// otherwise, they should use their representation of the integers 1 and 0.
    /// </summary>
    public sealed class BooleanType
        : ScalarType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanType"/> class.
        /// </summary>
        public BooleanType()
            : base("Boolean")
        {
        }

        public override Type NativeType { get; } = typeof(bool);

        public override bool IsInstanceOfType(IValueNode literal)
        {
            if (literal == null)
            {
                throw new ArgumentNullException(nameof(literal));
            }

            return literal is BooleanValueNode
                || literal is NullValueNode;
        }

        public override object ParseLiteral(IValueNode literal)
        {
            if (literal == null)
            {
                throw new ArgumentNullException(nameof(literal));
            }

            if (literal is BooleanValueNode boolLiteral)
            {
                return boolLiteral.Value;
            }

            if (literal is NullValueNode)
            {
                return null;
            }

            throw new ArgumentException(
                "The boolean type can only parse bool literals.",
                nameof(literal));
        }

        public override IValueNode ParseValue(object value)
        {
            if (value == null)
            {
                return NullValueNode.Default;
            }

            if (value is bool b)
            {
                return new BooleanValueNode(b);
            }

            throw new ArgumentException(
                "The specified value has to be a boolean" +
                "to be parsed by the boolean type.");
        }

        public override object Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (typeof(bool).IsInstanceOfType(value))
            {
                return value;
            }

            throw new ArgumentException(
                "The specified value cannot be handled by the BooleanType.");
        }
    }
}

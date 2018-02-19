using System;

namespace NitroxModel.DataStructures
{
    [Serializable]
    public class Optional<T> : IOptional<T>
        where T : class
    {
        private readonly T value;

        private Optional()
        {
            value = null;
        }

        private Optional(T value)
        {
            this.value = value;
        }

        public static Optional<T> Empty()
        {
            return new Optional<T>();
        }

        public static Optional<T> Of(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Value cannot be null");
            }

            return new Optional<T>(value);
        }

        public static Optional<T> OfNullable(T value)
        {
            return new Optional<T>(value);
        }

        public override T Get()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Optional did not have a value");
            }

            return value;
        }

        public override bool IsPresent()
        {
            return value != null;
        }
    }

    [Serializable]
    public sealed class OptionalEmptyException<T> : Exception
    {
        public OptionalEmptyException() : base($"Optional <{nameof(T)}> is empty.")
        {
        }

        public OptionalEmptyException(string message) : base($"Optional <{nameof(T)}> is empty:\n\t" + message)
        {
        }
    }
}

using System;
using NitroxModel.Helper;

namespace NitroxModel.DataStructures
{
    [Serializable]
    public class ValueTypeOptional<T> : IOptional<T>
        where T : struct
    {
        readonly T value;
        readonly bool hasValue;

        private ValueTypeOptional()
        {
        }

        private ValueTypeOptional(T value)
        {
            this.value = value;
            hasValue = true;
        }

        public static ValueTypeOptional<T> Empty()
        {
            return new ValueTypeOptional<T>();
        }

        public static ValueTypeOptional<T> Of(T value)
        {
            return new ValueTypeOptional<T>(value);
        }

        public override bool IsPresent() => hasValue;

        public override T Get()
        {
            Validate.IsTrue(hasValue);
            return value;
        }

        public bool IsDefault()
        {
            return hasValue && value.Equals(default(T));
        }
    }
}

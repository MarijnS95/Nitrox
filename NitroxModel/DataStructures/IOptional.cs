namespace NitroxModel.DataStructures
{
    public abstract class IOptional<T>
    {
        public abstract bool IsPresent();
        public bool IsEmpty() => !IsPresent();

        public abstract T Get();

        public T OrElse(T elseValue)
        {
            if (IsPresent())
            {
                return Get();
            }

            return elseValue;
        }

        public override string ToString()
        {
            if (IsEmpty())
            {
                return "Optional<" + typeof(T) + ">.Empty()";
            }

            return "Optional[" + Get().ToString() + "]";
        }
    }
}

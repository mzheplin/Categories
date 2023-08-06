namespace Graps
{
    public abstract class Homomorphism<T>
    {
        protected T source_;
        protected T target_;

        public T Source => source_;
        public T Target => target_;

        public abstract bool IsSurjective();

        public abstract bool IsInjective();

        public bool IsIsomorphism()
        {
            return IsInjective() && IsSurjective();
        }
    }
}

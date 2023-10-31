namespace Categories

{
    public abstract class Map<T>
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

        protected abstract Map<T> Composite(Map<T> map);

        public static Map<T> operator *(Map<T> m1, Map<T> m2)
        {
            return m1.Composite(m2);
        }
    }
}

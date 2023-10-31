using System.Collections.Generic;

namespace Categories
{
    public class EquivalenceClass<T> where T : class
    {
        public T representative;
        public List<T> elements;

        public EquivalenceClass()
        {
            elements = new List<T>();
        }

        public EquivalenceClass(T rep) : this()
        {
            representative = rep;
        }
    }
}

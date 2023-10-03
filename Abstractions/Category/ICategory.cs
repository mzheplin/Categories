using System;
namespace Categories
{
    public interface ICategory<T> 
    {
        public Coproduct<T> GetCoproduct(T t1, T t2);
        public Product<T> GetProduct(T t1, T t2);
        public Pushout<T> GetPushout(Map<T> h1, Map<T> h2);
        public Pullback<T> GetPullback(Map<T> h1, Map<T> h2);
    }
}

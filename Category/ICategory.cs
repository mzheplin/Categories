using System;
namespace Graps.Category
{
    public interface ICategory<T> 
    {
        public Coproduct<T> GetCoproduct(T t1, T t2);
        public Product<T> GetProduct(T t1, T t2);
        public Pushout<T> GetPushout(Homomorphism<T> h1, Homomorphism<T> h2);
        public Pullback<T> GetPullback(Homomorphism<T> h1, Homomorphism<T> h2);
    }
}

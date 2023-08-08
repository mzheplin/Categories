using System;
using System.Text;

namespace Categories
{
    public class Product<T>
    {
        private readonly T product_;
        private readonly Homomorphism<T> hom1_;
        private readonly Homomorphism<T> hom2_;

        public T product => product_;
        public Homomorphism<T> hom1 => hom1_;
        public Homomorphism<T> hom2 => hom2_;

        public Product(T obj, Homomorphism<T> hom1, Homomorphism<T> hom2)
        {
            product_ = obj;
            hom1_ = hom1;
            hom2_ = hom2;
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("product:");
            builder.AppendLine(product_.ToString());
            builder.AppendLine("homomorphism to first object:");
            builder.AppendLine(hom1.ToString());
            builder.AppendLine("homomorphism to second object:");
            builder.AppendLine(hom2.ToString());
            return builder.ToString();
        }
    }
}

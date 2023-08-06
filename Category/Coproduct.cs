using System;
using System.Text;

namespace Graps.Category
{
    public class Coproduct<T>
    {
        private readonly T coproduct_;
        private readonly Homomorphism<T> hom1_;
        private readonly Homomorphism<T> hom2_;

        public T coproduct => coproduct_;
        public Homomorphism<T> hom1 => hom1_;
        public Homomorphism<T> hom2 => hom2_;

        public Coproduct(T obj, Homomorphism<T> hom1, Homomorphism<T> hom2)
        {
            coproduct_ = obj;
            hom1_ = hom1;
            hom2_ = hom2;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("coproduct:");
            builder.AppendLine(coproduct.ToString());
            builder.AppendLine("homomorphism from first object:");
            builder.AppendLine(hom1.ToString());
            builder.AppendLine("homomorphism from second object:");
            builder.AppendLine(hom2.ToString());
            return builder.ToString();
        }
    }
}

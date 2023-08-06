using System;
using System.Text;

namespace Graps
{
    public class Pushout<T>
    {
        private readonly T pushout_;
        private readonly Homomorphism<T> hom1_;
        private readonly Homomorphism<T> hom2_;

        public T pushout => pushout_;
        public Homomorphism<T> hom1=> hom1_;
        public Homomorphism<T> hom2=> hom2_;

        public Pushout(T obj, Homomorphism<T> hom1, Homomorphism<T> hom2)
        {
            pushout_ = obj;
            hom1_ = hom1;
            hom2_ = hom2;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("pushout:");
            builder.AppendLine(pushout_.ToString());
            builder.AppendLine("homomorphism from first object:");
            builder.AppendLine(hom1.ToString());
            builder.AppendLine("homomorphism from second object:");
            builder.AppendLine(hom2.ToString());
            return builder.ToString();
        }
    }
}

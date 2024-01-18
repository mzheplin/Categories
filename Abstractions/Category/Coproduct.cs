using System;
using System.Text;

namespace Categories
{
    public class Coproduct<T>
    {
        private readonly T coproduct_;
        private readonly Map<T> hom1_;
        private readonly Map<T> hom2_;

        public T coproduct => coproduct_;
        public Map<T> hom1 => hom1_;
        public Map<T> hom2 => hom2_;

        public Coproduct(T obj, Map<T> hom1, Map<T> hom2)
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
            builder.AppendLine("Map from the first object:");
            builder.AppendLine(hom1.ToString());
            builder.AppendLine("Map from the second object:");
            builder.AppendLine(hom2.ToString());
            return builder.ToString();
        }
    }
}

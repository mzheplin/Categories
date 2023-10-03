using System;
using System.Text;

namespace Categories
{
    public class Pullback<T>
    {
        private readonly T pullback_;
        private readonly Map<T> hom1_;
        private readonly Map<T> hom2_;

        public T pullback => pullback_;
        public Map<T> hom1 => hom1_;
        public Map<T> hom2 => hom2_;
        public Pullback(T obj, Map<T> hom1, Map<T> hom2)
        {
            pullback_ = obj;
            hom1_ = hom1;
            hom2_ = hom2;
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("pullback:");
            builder.AppendLine(pullback_.ToString());
            builder.AppendLine("Map to first object:");
            builder.AppendLine(hom1.ToString());
            builder.AppendLine("Map to second object:");
            builder.AppendLine(hom2.ToString());
            return builder.ToString();
        }
    }
}

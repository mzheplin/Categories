using System;
using System.Text;

namespace Categories
{
    public class Pushout<T>
    {
        private readonly T pushout_;
        private readonly Map<T> hom1_;
        private readonly Map<T> hom2_;

        public T pushout => pushout_;
        public Map<T> hom1=> hom1_;
        public Map<T> hom2=> hom2_;

        public Pushout(T obj, Map<T> hom1, Map<T> hom2)
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
            builder.AppendLine("Map from first object:");
            builder.AppendLine(hom1.ToString());
            builder.AppendLine("Map from second object:");
            builder.AppendLine(hom2.ToString());
            return builder.ToString();
        }
    }
}

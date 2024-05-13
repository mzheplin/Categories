using System;
using System.Text;

namespace Categories
{
    public class DEdge : Edge
    {
        public DEdge(Vertex tail, Vertex head, string label = "") : base(tail, head, label) { }

        public static bool operator ==(DEdge edge1, DEdge edge2)
        {
            if (edge1 is null) return edge2 is null;
            return edge1.Equals(edge2);
        }

        public static bool operator !=(DEdge edge1, DEdge edge2)
        { 
            return !(edge1 == edge2);
        }

        public override bool Equals(object obj)
        {
            return Label == ((Edge)obj).Label &&
            Tail == ((Edge)obj).Tail &&
            Head == ((Edge)obj).Head;
        }
        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"{Label}: {Tail.Label} -> {Head.Label}");
            return builder.ToString();
        }
    }
}

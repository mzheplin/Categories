using System.Text;

namespace Categories
{


    public abstract class Edge
    {
        private readonly Vertex tail_;
        private readonly Vertex head_;
        private string label_;

        public Vertex Tail => tail_;
        public Vertex Head => head_;
        public string Label
        {
            get => label_;
            set => label_ = value;
        }

        public bool IsALoop => Tail == Head;

        public Edge(Vertex tail, Vertex head, string label = null)
        {
            tail_ = tail;
            head_ = head;
            label_ = label;
        }

    }

    public class DEdge : Edge
    {
        public DEdge(Vertex tail, Vertex head, string label = null) : base(tail, head, label) { }

        public static bool operator ==(DEdge edge1, DEdge edge2)
        {
            return edge1.Equals(edge2);
        }

        public static bool operator !=(DEdge edge1, DEdge edge2)
        {
            return !edge1.Equals(edge2);
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

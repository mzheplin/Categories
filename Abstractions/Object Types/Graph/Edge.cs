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
}

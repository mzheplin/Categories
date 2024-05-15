using System.Text;

namespace Categories
{


    public abstract class Edge
    {
        protected readonly Vertex tail_;
        protected readonly Vertex head_;
        protected string label_;
        protected string left_;
        protected string right_;


        public Vertex Tail => tail_;
        public Vertex Head => head_;
        public string Label
        {
            get => label_;
            set => label_ = value;
        }
        public string Left
        {
            get => left_;
            set => left_ = value;
        }

        public string Right
        {
            get => right_;
            set => right_ = value;
        }

        public bool IsALoop => Tail == Head;

        public Edge(Vertex tail, Vertex head, string label = "")
        {
            tail_ = tail;
            head_ = head;
            left_ = "";
            right_ = "";
            label_ = label;
        }

        public Edge(Vertex tail, Vertex head, string left, string right)
        {
            tail_ = tail;
            head_ = head;
            left_ = left;
            right_ = right;
            label_ = $"{left} - {right}";
        }

    }
}

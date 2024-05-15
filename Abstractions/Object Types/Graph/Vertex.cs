using System;
namespace Categories

{
    public class Vertex
    {
        protected string label_;
        protected string left_;
        protected string right_;

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


        public Vertex(string label)
        {
            left_ = "";
            right_ = "";
            label_ = label;
        }

        public Vertex(string left, string right) 
        {
            left_ = left;
            right_ = right;
            label_ = $"{left} - {right}";
        }


        public static bool operator ==(Vertex vertex1, Vertex vertex2)
        {
            if (vertex1 is null) return vertex2 is null;
            return vertex1.Equals(vertex2);
        }

        public static bool operator !=(Vertex vertex1, Vertex vertex2)
        {
            return !(vertex1 == vertex2);
        }

        public override bool Equals(object obj)
        {
            return Label == ((Vertex)obj).Label;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}

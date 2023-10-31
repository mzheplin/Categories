using System;
namespace Categories

{
    public class Vertex
    {
        private string label_;
        public string Label
        {
            get => label_;
            set => label_ = value;
        }

        public Vertex(string label)
        {
            label_ = label;
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

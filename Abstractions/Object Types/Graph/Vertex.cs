using System;
namespace Categories

{
    public class Vertex
    {
        private readonly string label_;
        public string Label => label_;

        public Vertex(string label)
        {
            label_ = label;
        }

        public static bool operator ==(Vertex vertex1, Vertex vertex2)
        {
            return vertex1.Label == vertex2.Label;
        }

        public static bool operator !=(Vertex vertex1, Vertex vertex2)
        {
            return vertex1.Label != vertex2.Label;
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Categories
{

    public abstract class Graph<T> where T : Edge
    {
        protected List<T> edges_;
        public List<T> Edges => edges_;

        protected List<Vertex> vertices_;
        public List<Vertex> Vertices => vertices_;

        public Graph()
        {
            edges_ = new List<T>();
            vertices_ = new List<Vertex>();
        }
        public Graph(List<T> edges)
        {
            edges_ = edges.Distinct().ToList();
            vertices_ = edges.Select(t => t.Tail).Union(
                edges.Select(t => t.Head)).ToList();

        }

        public Graph(List<T> edges, List<Vertex> vertices)
        {
            edges_ = edges.Distinct().ToList();
            vertices_ = vertices.Distinct().ToList();
        }

        public void Add(Vertex vertex)
        {
            if (!vertices_.Contains(vertex))
                vertices_.Add(vertex);
         }

        public void Add(T edge)
        {
            if (!vertices_.Contains(edge.Tail))
                vertices_.Add(edge.Tail);
            if (!vertices_.Contains(edge.Head))
                vertices_.Add(edge.Head);
            if (!edges_.Contains(edge))
                edges_.Add(edge);
        }


        public bool Contains(Graph<T> imGraph)
        {
            foreach (Vertex v in imGraph.Vertices)
            {
                if (!Vertices.Contains(v)) return false;
            }
            foreach (Edge edge in imGraph.Edges)
            {
                if (!Edges.Contains(edge)) return false;
            }
            return true;
        }

        public static bool operator ==(Graph<T> graph1, Graph<T> graph2)
        {
            if (graph1 is null) return graph2 is null;
            return graph1.Equals(graph2);
        }

        public static bool operator !=(Graph<T> graph1, Graph<T> graph2)
        {
            return !(graph1==graph2);
        }

        public override bool Equals(object obj)
        {
            return Contains((Graph<T>)obj) &&
                ((Graph<T>)obj).Contains(this);
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
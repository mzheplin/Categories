using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Categories
{
    public class DLMGraph : Graph<DEdge> //directed labeled multigraph
    {
        public DLMGraph() : base() { }
        public DLMGraph(List<DEdge> edges) : base(edges) { }
        public DLMGraph(List<DEdge> edges, List<Vertex> vertices) : base(edges, vertices) { }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("digraph G {");
            foreach(var v in Vertices)
            {
                builder.AppendLine($"{Vertices.IndexOf(v)} [label =\"{v.Label}\"];");
            }
            foreach (var e in Edges)
            {
                builder.AppendLine($"{Vertices.IndexOf(e.Tail)} -> {Vertices.IndexOf(e.Head)} [label=\"{e.Label}\"] ;");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}




























/*       private bool IsCorrectInputAndOutput(int[,] input, int[,] output)
        {
            int vInput = input.GetLength(0);
            int eInput = input.GetLength(1);
            int vOutput = output.GetLength(0);
            int eOutput = output.GetLength(1);

            if (vInput != vOutput || eInput != eOutput) return false;

            for (int i = 0; i < eInput; i++)
            {
                bool hasbVertex = false;
                bool haseVertex = false;

                for (int j = 0; j < vInput; j++)
                {
                    if (input[j, i] != 0 && !hasbVertex)
                        hasbVertex = true;
                    else if (input[j, i] != 0 && hasbVertex)
                        return false;

                    if (output[j, i] != 0 && !haseVertex)
                        haseVertex = true;
                    else if (output[j, i] != 0 && haseVertex)
                        return false;
                }
                if (!hasbVertex || !haseVertex) return false;
            }
            return true;
        }

        private void AssignEdgesAndVertices(int[,] inp,
            int[,] outp,
            List<string> edgeLabeles,
            List<string> vertexLabeles)
        {
            int vertNum1 = inp.GetLength(0);
            int edgeNum1 = inp.GetLength(1);

            for (int j = 0; j < vertNum1; j++)
                vertices_.Add(new Vertex(vertexLabeles[j]));

            for (int i = 0; i < edgeNum1; i++)
            {
                int v1 = -1, v2 = -1;
                for (int j = 0; j < vertNum1; j++)
                {
                    if (inp[j, i] == 1)
                        v1 = j;
                    if (outp[j, i] == 1)
                        v2 = j;
                }
                edges_.Add(new Edge(vertices_[v1], vertices_[v2], edgeLabeles[i]));
            }
        }

        private void ToInputAndOutput(List<Edge> edges)
        {
          
            int[,] input = new int[Vertex_Number, Edge_Number],
                output = new int[Vertex_Number, Edge_Number];

            for (int i = 0; i < resTuples.Count; i++)
            {
                input[resTuples[i].Item1, i] = 1;
                output[resTuples[i].Item2, i] = 1;
            }

            return new Tuple<int[,], int[,]>(input, output);
        }*/
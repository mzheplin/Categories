using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Categories
{

    class Program
    {
        static void Main(string[] args)
        {
            Vertex a = new Vertex("a");
            Vertex b = new Vertex("b");
            Vertex c = new Vertex("c");
            List<DEdge> edges1 = new List<DEdge>() {
                new DEdge(a, b, "a"),
                new DEdge(b, a, "b"),};
            DLMGraph g_1 = new DLMGraph(edges1);
            List<DEdge> edgesc = new List<DEdge>() {
                new DEdge(a, b, "a"),
                new DEdge(c, b, "b")};
            DLMGraph g_c = new DLMGraph(edgesc);
           
            Dictionary<Vertex, Vertex> v_c1 = new Dictionary<Vertex, Vertex>()
                { {a,a },{c,a},{b,b}};
            Dictionary<string, string> l_c1 = new Dictionary<string, string>()
                {{"a","a" },{"b","a"},{"c","a"} };
     
            DLMGHomomorphism h1 = new DLMGHomomorphism(g_c, g_1, v_c1, l_c1);
        }
    }
}

using System.Linq;
using System.Collections.Generic;

namespace Categories
{

    class Program
    {
        static void Main(string[] args)
        {
            DLGCategory category = new DLGCategory();

            /*            Vertex v1 = new Vertex("state A affordance");
                        Vertex v2 = new Vertex("state B affordance");
                        Vertex v3 = new Vertex("state C affordance");
                        DEdge e1 = new DEdge(v1, v2, "swipe left");
                        DEdge e2 = new DEdge(v2, v1, "swipe right");
                        DEdge e3 = new DEdge(v2, v3, "swipe left");
                        DEdge e4 = new DEdge(v3, v2, "swipe right");
                        DEdge e5 = new DEdge(v2, v2, "swipe left");
                        DEdge e6 = new DEdge(v2, v2, "swipe right");
                        List<DEdge> edges1 = new List<DEdge>() { e1, e2, e3, e4, e5, e6 };

                        DLGraph affordance_graph = new DLGraph(edges1);
                        System.Console.WriteLine(affordance_graph.ToString());
                        System.Console.WriteLine();

                        Vertex v11 = new Vertex("state 1 action");
                        Vertex v21 = new Vertex("state 2 action");
                        Vertex v31 = new Vertex("state 3 action");
                        Vertex v41 = new Vertex("state 4 action");
                        DEdge e11 = new DEdge(v11, v21, "next page");
                        DEdge e21 = new DEdge(v21, v11, "previous page");
                        DEdge e31 = new DEdge(v21, v31, "next page");
                        DEdge e41 = new DEdge(v31, v21, "previous page");
                        DEdge e51 = new DEdge(v31, v41, "next page");
                        DEdge e61 = new DEdge(v41, v31, "previous page");
                        List<DEdge> edges2 = new List<DEdge>() { e11, e21, e31, e41, e51, e61 };

                        DLGraph action_graph = new DLGraph(edges2);
                        System.Console.WriteLine(action_graph.ToString());
                        System.Console.WriteLine();

                        Vertex v12 = new Vertex("state end abstract");
                        Vertex v22 = new Vertex("state middle abstract");
                        DEdge e12 = new DEdge(v12, v22, "+");
                        DEdge e22 = new DEdge(v12, v22, "-");
                        DEdge e32 = new DEdge(v22, v12, "+");
                        DEdge e42 = new DEdge(v22, v12, "-");
                        DEdge e52 = new DEdge(v22, v22, "+");
                        DEdge e62 = new DEdge(v22, v22, "-");
                        List<DEdge> edges3 = new List<DEdge>() { e12, e22, e32, e42, e52, e62 };
                        DLGraph abstract_graph = new DLGraph(edges3);
                        System.Console.WriteLine(abstract_graph.ToString());
                        System.Console.WriteLine();



                        Dictionary<Vertex, Vertex> vmap13 = new Dictionary<Vertex, Vertex>()
                        {
                            {v1,v12 },{v2,v22},{v3,v12}
                        };

                        Dictionary<Vertex, Vertex> vmap12 = new Dictionary<Vertex, Vertex>()
                        {
                            {v11,v12},{v21,v22},{v31,v22},{v41,v12}
                        };
                        Dictionary<string, string> edgemap13 = new Dictionary<string, string>()
                        {
                            {"swipe left","-" },{"swipe right","+"}
                        };

                        Dictionary<string, string> edgemap12 = new Dictionary<string, string>()
                        {
                            {"next page","-"},{"previous page","+"}
                        };


                        DLGHomomorphism hom1 = new DLGHomomorphism(affordance_graph, abstract_graph, vmap13, edgemap13);
                        DLGHomomorphism hom2 = new DLGHomomorphism(action_graph, abstract_graph, vmap12, edgemap12);


                        var pullback = category.GetPullback(hom1, hom2);
                        System.Console.WriteLine(pullback.pullback.ToString());
                        System.Console.WriteLine();




                        var product = category.GetProduct(affordance_graph, affordance_graph);
                        System.Console.WriteLine(product.product.ToString());
                        System.Console.WriteLine();

                        var coproduct = category.GetCoproduct(affordance_graph, affordance_graph);
                        System.Console.WriteLine(coproduct.coproduct.ToString());
                        System.Console.WriteLine();

                        


            Vertex v1 = new Vertex("1");
                        Vertex v2 = new Vertex("2");
                        Vertex v3 = new Vertex("3");
                        Vertex v4 = new Vertex("4");
                        DEdge e1 = new DEdge(v2, v1, "a");
                        DEdge e2 = new DEdge(v2, v3, "b");
                        DEdge e3 = new DEdge(v2, v4, "c");
                        DLGraph g1 = new DLGraph();
                        g1.Add(e1);g1.Add(e2);g1.Add(e3);

                        DEdge e5 = new DEdge(v1, v2, "b");
                        DEdge e6 = new DEdge(v1, v3, "a");
                        DEdge e7 = new DEdge(v2, v4, "c");
                        DEdge e8 = new DEdge(v3, v4, "d");
                        DLGraph g2 = new DLGraph();
                        g2.Add(e5); g2.Add(e6); g2.Add(e7); g2.Add(e8);

            Dictionary<Vertex, Vertex> vmap12 = new Dictionary<Vertex, Vertex>()
                        {{v1,v3 },{v4,v3},{v2,v1},{v3,v2}};
                        Dictionary<string, string> edgemap12 = new Dictionary<string, string>()
                        {
                            {"a","a"},{"c","a"},{"b","b"}
                        };

                        DLGHomomorphism hom2 = new DLGHomomorphism(g1, g2, vmap12, edgemap12);

                        DEdge e9 = new DEdge(v1, v2, "b");
                        DEdge e10 = new DEdge(v1, v3, "a");
                        DEdge e11 = new DEdge(v2, v2, "c");
                        DEdge e12 = new DEdge(v3, v3, "d");
                        DLGraph g3 = new DLGraph();
                        g3.Add(e9); g3.Add(e10); g3.Add(e11); g3.Add(e12);
            Dictionary<Vertex, Vertex> vmap13 = new Dictionary<Vertex, Vertex>()
                        {{v1,v2 },{v4,v3},{v2,v1},{v3,v3}};
                        Dictionary<string, string> edgemap13 = new Dictionary<string, string>()
                        {
                            {"a","b"},{"c","a"},{"b","a"}
                        };
                        DLGHomomorphism hom3 = new DLGHomomorphism(g1, g3, vmap13, edgemap13);

                        var pushout = category.GetPushout(hom2, hom3);
            System.Console.WriteLine(pushout.ToString());*/

            Vertex v1 = new Vertex("v1");
            Vertex v2 = new Vertex("v2");
            Vertex v3 = new Vertex("v3");
            Vertex v4 = new Vertex("v4");
            Vertex v5 = new Vertex("v5");

            //graph 1
            List<DEdge> edges1 = new List<DEdge>() {
            new DEdge(v1, v3, "a"),
            new DEdge(v1, v4, "b"),
            new DEdge(v2, v4, "c")
            };
            DLGraph g1 = new DLGraph(edges1);
            System.Console.WriteLine("graph 1:");
            System.Console.WriteLine(g1.ToString());


            //graph2
            List<DEdge> edges2 = new List<DEdge>() {
            new DEdge(v1, v2, "a"),
            new DEdge(v1, v3, "b"),
            new DEdge(v4, v5, "c")
            };
            DLGraph g2 = new DLGraph(edges2);
            System.Console.WriteLine("graph 2:");
            System.Console.WriteLine(g2.ToString());

            //graph 3
            List<DEdge> edges3 = new List<DEdge>() {
            new DEdge(v1, v1, "a"),
            new DEdge(v2, v2, "b"),
            new DEdge(v3, v3, "c"),
            new DEdge(v4, v4, "d"),
            new DEdge(v1, v3, "e"),
            new DEdge(v2, v4, "f")
            };
            DLGraph g3 = new DLGraph(edges3);
            System.Console.WriteLine("graph 3:");
            System.Console.WriteLine(g3.ToString());

            //graph 4
            List<DEdge> edges4 = new List<DEdge>() {
            new DEdge(v1, v1, "a"),
            new DEdge(v2, v2, "b"),
            new DEdge(v1, v2, "c")
            };
            DLGraph g4 = new DLGraph(edges4);
            System.Console.WriteLine("graph 4:");
            System.Console.WriteLine(g4.ToString());

            //homomorhism from g1 to g2

            Dictionary<Vertex, Vertex> g1g2_vertices = new Dictionary<Vertex, Vertex>()
            {
                {v1,v1 },{v2,v1},{v3,v2},{v4,v3}
            };

            Dictionary<string, string> g1g2_labeles = new Dictionary<string, string>()
            {
                {"a","a" },{"b","b"},{"c","b"}
            };
            DLGHomomorphism g1_g2 = new DLGHomomorphism(g1, g2, g1g2_vertices, g1g2_labeles);
            System.Console.WriteLine("homomorphism from g1 to g2:");
            System.Console.WriteLine(g1_g2.ToString());


            //homomorhism from g1 to g3

            Dictionary<Vertex, Vertex> g1g3_vertices = new Dictionary<Vertex, Vertex>()
            {
                {v1,v1 },{v2,v1},{v3,v1},{v4,v1}
            };

            Dictionary<string, string> g1g3_labeles = new Dictionary<string, string>()
            {
                {"a","a" },{"b","a"},{"c","a"}
            };
            DLGHomomorphism g1_g3 = new DLGHomomorphism(g1, g3, g1g3_vertices, g1g3_labeles);

            System.Console.WriteLine("homomorphism from g1 to g3:");
            System.Console.WriteLine(g1_g3.ToString());

            //homomorhism from g2 to g4
            Dictionary<Vertex, Vertex> g2g4_vertices = new Dictionary<Vertex, Vertex>()
            {
                {v1,v1 },{v2,v2},{v3,v2},{v4,v1},{v5,v2}
            };

            Dictionary<string, string> g2g4_labeles = new Dictionary<string, string>()
            {
                {"a","c" },{"b","c"},{"c","c"}
            };
            DLGHomomorphism g2_g4 = new DLGHomomorphism(g2, g4, g2g4_vertices, g2g4_labeles);

            System.Console.WriteLine("homomorphism from g2 to g4:");
            System.Console.WriteLine(g2_g4.ToString());

            //homomorhism from g3 to g4
            Dictionary<Vertex, Vertex> g3g4_vertices = new Dictionary<Vertex, Vertex>()
            {
                {v1,v1 },{v2,v1},{v3,v2},{v4,v2}
            };

            Dictionary<string, string> g3g4_labeles = new Dictionary<string, string>()
            {
                {"a","a" },{"b","a"},{"c","b"},{"d","b"},{"e","c"},{"f","c"}
            };
            DLGHomomorphism g3_g4 = new DLGHomomorphism(g3, g4, g3g4_vertices, g3g4_labeles);

            System.Console.WriteLine("homomorphism from g3 to g4:");
            System.Console.WriteLine(g3_g4.ToString());

            DLGCategory dLGCategory = new DLGCategory();

            //product of g2 and g3
            var product = dLGCategory.GetProduct(g2, g3);

            System.Console.WriteLine("product of g2 and g3:");
            System.Console.WriteLine(product.ToString());


            //coproduct of g2 and g3
            var coproduct = dLGCategory.GetCoproduct(g2, g3);

            System.Console.WriteLine("coproduct of g2 and g3:");
            System.Console.WriteLine(coproduct.ToString());

            //pushout of g2 and g3

            var pushout = dLGCategory.GetPushout(g1_g2,g1_g3);

            System.Console.WriteLine("pushout of g2 and g3:");
            System.Console.WriteLine(pushout.ToString());

            //pullback of g2 and g3

            var pullback = dLGCategory.GetPullback(g2_g4, g3_g4);

            System.Console.WriteLine("pullback of g2 and g3:");
            System.Console.WriteLine(pullback.ToString());



        }
    }
}

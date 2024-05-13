using System;
using System.Collections.Generic;
using System.Linq;

namespace Categories.Examples
{
    public class SevenPuzzle
    {
        public DLMGraph abstractGraph;
        public DLMGraph affordanceGraph;
        public DLMGraph actionGraph;
        public DLMGHomomorphism aff_abs;
        public DLMGHomomorphism act_abs;
        public Pullback<DLMGraph> interfaceAsPullback;

        public SevenPuzzle()
        {
            //abstract graph  (isomorphic to affordance)
            abstractGraph = GenerateAbstract();

            //affordance graph (how does the person do task)
            affordanceGraph = GenerateAffordance();

            //affordance to abstract
            var aff_abs_vertices = new Dictionary<string, string>()
            {
                { "L D F" , "5"}, { "L U F", "1"}, {"L D B" , "7" }, {"L U B" , "3" },
                { "R D F" , "6"}, { "R U F", "2"}, {"R D B" , "8" }, {"R U B" , "4" },
            };

            var aff_abs_edges = new Dictionary<string, string>()
            {
                { "L" , ""}, { "R", ""}, {"U" , "" }, {"D" , "" },  {"B" , "" },  {"F" , "" }
            };

            aff_abs = GenerateMorphism(affordanceGraph, abstractGraph, aff_abs_vertices, aff_abs_edges);

            //actionGraph (generated from abstract)
            int[] arr = { 1, 2, 3,4,5,6,7, 0 };
            int pole = 7;
            actionGraph = new DLMGraph();
            var act_abs_vertices = new Dictionary<string, string>();

            ActionGraphGenerator.GenerateActionGraph(arr, pole, SevenPuzzleDict, ref actionGraph, ref act_abs_vertices);

            //action to abstract
            var act_abs_edges = new Dictionary<string, string>()
            {
                { "8->7" , ""}, { "5->7", ""}, {"3->7" , "" }, {"1->2" , "" }, {"4->2" , "" }, {"6->2" , "" },
                { "3->4" , ""}, { "2->4", ""}, {"8->4" , "" }, {"7->8" , "" }, {"6->8" , "" }, {"4->8" , "" },
                { "2->1" , ""}, { "3->1", ""}, {"5->1" , "" }, {"4->3" , "" }, {"1->3" , "" }, {"7->3" , "" },
                { "5->6" , ""}, { "8->6", ""}, {"2->6" , "" }, {"6->5" , "" }, {"7->5" , "" }, {"1->5" , "" },

            };

            act_abs = GenerateMorphism(actionGraph, abstractGraph, act_abs_vertices, act_abs_edges);


            DLMGCategory category = new DLMGCategory();

            interfaceAsPullback = category.GetPullback(act_abs, aff_abs);
        }


        private DLMGHomomorphism GenerateMorphism(DLMGraph g1, DLMGraph g2, Dictionary<string, string> v, Dictionary<string, string> e)
        {
            Dictionary<Vertex, Vertex> vertex = v.ToDictionary(
                kvp => new Vertex(kvp.Key),
                kvp => new Vertex(kvp.Value)
            );

            return new DLMGHomomorphism(g1, g2, vertex, e);
        }


        private DLMGraph GenerateAbstract()
        {
            Vertex ldf = new Vertex("5");
            Vertex luf = new Vertex("1");
            Vertex rdf = new Vertex("6");
            Vertex ruf = new Vertex("2");
            Vertex ldb = new Vertex("7");
            Vertex lub = new Vertex("3");
            Vertex rdb = new Vertex("8");
            Vertex rub = new Vertex("4");

            List<DEdge> abstractEdges = new List<DEdge>() {
                new DEdge(luf, ruf, ""),
                new DEdge(ruf, luf, ""),
                new DEdge(lub, rub, ""),
                new DEdge(rub, lub, ""),
                new DEdge(rdf, ldf, ""),
                new DEdge(ldf, rdf, ""),
                new DEdge(ldb, rdb, ""),
                new DEdge(rdb, ldb, ""),

                new DEdge(luf, ldf, ""),
                new DEdge(lub, ldb, ""),
                new DEdge(ruf, rdf, ""),
                new DEdge(ldf, luf, ""),
                new DEdge(rdf, ruf, ""),
                new DEdge(ldb, lub, ""),
                new DEdge(rdb, rub, ""),
                new DEdge(rub, rdb, ""),


                new DEdge(lub, luf, ""),
                new DEdge(luf, lub, ""),
                new DEdge(rub, ruf, ""),
                new DEdge(ruf, rub, ""),
                new DEdge(ldb, ldf, ""),
                new DEdge(ldf, ldb, ""),
                new DEdge(rdb, rdf, ""),
                new DEdge(rdf, rdb, ""),


            };
            return new DLMGraph(abstractEdges);
        }

        private DLMGraph GenerateAffordance()
        {
            Vertex ldf = new Vertex("L D F");
            Vertex luf = new Vertex("L U F");
            Vertex rdf = new Vertex("R D F");
            Vertex ruf = new Vertex("R U F");
            Vertex ldb = new Vertex("L D B");
            Vertex lub = new Vertex("L U B");
            Vertex rdb = new Vertex("R D B");
            Vertex rub = new Vertex("R U B");

            List<DEdge> affordanceEdges = new List<DEdge>() {
                new DEdge(luf, ruf, "L"),
                new DEdge(ruf, luf, "R"),
                new DEdge(lub, rub, "L"),
                new DEdge(rub, lub, "R"),
                new DEdge(rdf, ldf, "R"),
                new DEdge(ldf, rdf, "L"),
                new DEdge(ldb, rdb, "L"),
                new DEdge(rdb, ldb, "R"),

                new DEdge(luf, ldf, "U"),
                new DEdge(lub, ldb, "U"),
                new DEdge(ruf, rdf, "U"),
                new DEdge(ldf, luf, "D"),
                new DEdge(rdf, ruf, "D"),
                new DEdge(ldb, lub, "D"),
                new DEdge(rdb, rub, "D"),
                new DEdge(rub, rdb, "U"),


                new DEdge(lub, luf, "B"),
                new DEdge(luf, lub, "F"),
                new DEdge(rub, ruf, "B"),
                new DEdge(ruf, rub, "F"),
                new DEdge(ldb, ldf, "B"),
                new DEdge(ldf, ldb, "F"),
                new DEdge(rdb, rdf, "B"),
                new DEdge(rdf, rdb, "F"),


            };
            return new DLMGraph(affordanceEdges);
        }

        private Dictionary<int, List<Tuple<int, int>>> SevenPuzzleDict = new Dictionary<int, List<Tuple<int, int>>>()
        {
            {
                0, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(2,0),
                    new Tuple<int, int>(1,0),
                    new Tuple<int, int>(4,0),
                }
            },
            {
                1, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(3,1),
                    new Tuple<int, int>(0,1),
                    new Tuple<int, int>(5,1),
                }
            },
            {
                2, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(3,2),
                    new Tuple<int, int>(0,2),
                    new Tuple<int, int>(6,2),
                }
            },
            {
                3, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(2,3),
                    new Tuple<int, int>(1,3),
                    new Tuple<int, int>(7,3),
                }
            },
            {
                4, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(5,4),
                    new Tuple<int, int>(6,4),
                    new Tuple<int, int>(0,4),
                }
            },
            {
                5, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(4,5),
                    new Tuple<int, int>(7,5),
                    new Tuple<int, int>(1,5),
                }
            },
            {
                6, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(7,6),
                    new Tuple<int, int>(4,6),
                    new Tuple<int, int>(2,6),
                }
            },
            {
                7, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(6,7),
                    new Tuple<int, int>(5,7),
                    new Tuple<int, int>(3,7),
                }
            },
        };
    }
}


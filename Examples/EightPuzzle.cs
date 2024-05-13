using System;
using System.Collections.Generic;
using System.Linq;

namespace Categories.Examples
{
    public class EightPuzzle
    {
        public DLMGraph abstractGraph;
        public DLMGraph affordanceGraph;
        public DLMGraph actionGraph;
        public DLMGHomomorphism aff_abs;
        public DLMGHomomorphism act_abs;
        public Pullback<DLMGraph> interfaceAsPullback;

        public EightPuzzle()
        {
            //abstract graph  (isomorphic to affordance)
            abstractGraph = GenerateAbstract();

            //affordance graph (how does the person do task)
            affordanceGraph = GenerateAffordance();

            //affordance to abstract
            var aff_abs_vertices = new Dictionary<string, string>()
            {
                { "L U" , "1"}, { "R L U", "1"}, {"R U" , "7" },
                { "L U D" , "1"}, { "R L U D", "1"}, {"R U D" , "7" },
                { "L D" , "1"}, { "R L D", "1"}, {"R D" , "7" },

            };

            var aff_abs_edges = new Dictionary<string, string>()
            {
                { "L" , ""}, { "R", ""}, {"U" , "" }, {"D" , "" }
            };

            aff_abs = GenerateMorphism(affordanceGraph, abstractGraph, aff_abs_vertices, aff_abs_edges);
           
            //actionGraph (generated from abstract)
            int[] arr = { 1, 2, 3, 4, 5, 6, 7,8, 0 };
            int pole = 8;
            actionGraph = new DLMGraph();
            var act_abs_vertices = new Dictionary<string, string>();

            ActionGraphGenerator.GenerateActionGraph(arr, pole, SevenPuzzleDict, ref actionGraph, ref act_abs_vertices);

            //action to abstract
            var act_abs_edges = new Dictionary<string, string>()
            {
                { "7->8" , ""}, { "5->8", ""}, {"8->7" , "" }, {"6->7" , "" }, {"4->7" , "" }, {"7->6" , "" },
                { "3->6" , ""}, { "4->5", ""}, {"8->5" , "" }, {"2->5" , "" }, {"5->4" , "" }, {"3->4" , "" },
                { "7->4" , ""}, { "1->4", ""}, {"4->3" , "" }, {"6->3" , "" }, {"0->3" , "" }, {"1->2" , "" },
                { "5->2" , ""}, { "0->1", ""}, {"2->1" , "" }, {"4->1" , "" }, {"1->0" , "" }, {"3->0" , "" },

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
            Vertex lu = new Vertex("1");
            Vertex rlu = new Vertex("2");
            Vertex ru = new Vertex("3");
            Vertex lud = new Vertex("4");
            Vertex rlud = new Vertex("5");
            Vertex rud = new Vertex("6");
            Vertex ld = new Vertex("7");
            Vertex rld = new Vertex("8");
            Vertex rd = new Vertex("9");

            List<DEdge> abstractEdges = new List<DEdge>() {
                new DEdge(lu, rlu, ""),
                new DEdge(rlu, ru, ""),
                new DEdge(lud, rlud, ""),
                new DEdge(rlud, rud, ""),
                new DEdge(ld, rld, ""),
                new DEdge(rld, rd, ""),

                new DEdge(rlu, lu, ""),
                new DEdge(ru, rlu, ""),
                new DEdge(rlud, lud, ""),
                new DEdge(rud, rlud, ""),
                new DEdge(rld, ld, ""),
                new DEdge(rd, rld, ""),


                new DEdge(lu, lud, ""),
                new DEdge(lud, ld, ""),
                new DEdge(rlu, rlud, ""),
                new DEdge(rlud, rld, ""),
                new DEdge(ru, rud, ""),
                new DEdge(rud, rd, ""),

                new DEdge(lud, lu, ""),
                new DEdge(ld, lud, ""),
                new DEdge(rlud, rlu, ""),
                new DEdge(rld, rlud, ""),
                new DEdge(rud, ru, ""),
                new DEdge(rd, rud, ""),
            };
            return new DLMGraph(abstractEdges);
        }

        private DLMGraph GenerateAffordance()
        {
            Vertex lu = new Vertex("L U");
            Vertex rlu = new Vertex("R L U");
            Vertex ru = new Vertex("R U");
            Vertex lud = new Vertex("L U D");
            Vertex rlud = new Vertex("R L U D");
            Vertex rud = new Vertex("R U D");
            Vertex ld = new Vertex("L D");
            Vertex rld = new Vertex("R L D");
            Vertex rd = new Vertex("R D");

            List<DEdge> affordanceEdges = new List<DEdge>() {
                new DEdge(lu, rlu, "L"),
                new DEdge(rlu, ru, "L"),
                new DEdge(lud, rlud, "L"),
                new DEdge(rlud, rud, "L"),
                new DEdge(ld, rld, "L"),
                new DEdge(rld, rd, "L"),

                new DEdge(rlu, lu, "R"),
                new DEdge(ru, rlu, "R"),
                new DEdge(rlud, lud, "R"),
                new DEdge(rud, rlud, "R"),
                new DEdge(rld, ld, "R"),
                new DEdge(rd, rld, "R"),


                new DEdge(lu, lud, "U"),
                new DEdge(lud, ld, "U"),
                new DEdge(rlu, rlud, "U"),
                new DEdge(rlud, rld, "U"),
                new DEdge(ru, rud, "U"),
                new DEdge(rud, rd, "U"),

                new DEdge(lud, lu, "D"),
                new DEdge(ld, lud, "D"),
                new DEdge(rlud, rlu, "D"),
                new DEdge(rld, rlud, "D"),
                new DEdge(rud, ru, "D"),
                new DEdge(rd, rud, "D"),
            };
            return new DLMGraph(affordanceEdges);
        }

        private Dictionary<int, List<Tuple<int, int>>> SevenPuzzleDict = new Dictionary<int, List<Tuple<int, int>>>()
        {
            {
                0, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(3,0),
                    new Tuple<int, int>(1,0),
                }
            },
            {
                1, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(2,1),
                    new Tuple<int, int>(0,1),
                    new Tuple<int, int>(4,1),
                }
            },
            {
                2, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(1,2),
                    new Tuple<int, int>(5,2),
                }
            },
            {
                3, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(4,3),
                    new Tuple<int, int>(6,3),
                    new Tuple<int, int>(0,3),
                }
            },
            {
                4, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(5,4),
                    new Tuple<int, int>(3,4),
                    new Tuple<int, int>(7,4),
                    new Tuple<int, int>(1,4),
                }
            },
            {
                5, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(4,5),
                    new Tuple<int, int>(8,5),
                    new Tuple<int, int>(2,5),
                }
            },
            {
                6, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(7,6),
                    new Tuple<int, int>(3,6),
                }
            },
            {
                7, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(8,7),
                    new Tuple<int, int>(6,7),
                    new Tuple<int, int>(4,7),
                }
            },
             {
                8, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(7,8),
                    new Tuple<int, int>(5,8),
                }
            },
        };
    }
}


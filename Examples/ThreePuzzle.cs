using System;
using System.Collections.Generic;
using System.Linq;

namespace Categories.Examples
{
	public class ThreePuzzle
	{
		public DLMGraph abstractGraph;
        public DLMGraph affordanceGraph;
        public DLMGraph actionGraph;
        public DLMGHomomorphism aff_abs;
        public DLMGHomomorphism act_abs;
        public Pullback<DLMGraph> interfaceAsPullback;

        public ThreePuzzle()
		{
            //abstract graph  (isomorphic to affordance)
            abstractGraph = GenerateAbstract();

            //affordance graph (how does the person do task)
            affordanceGraph = GenerateAffordance();

            //affordance to abstract
            var aff_abs_vertices = new Dictionary<string, string>()
            {
                { "L U" , "1"}, { "R U", "2"}, {"L D" , "3" }, {"R D" , "4" }
            };

            var aff_abs_edges = new Dictionary<string, string>()
            {
                { "L" , ""}, { "R", ""}, {"U" , "" }, {"D" , "" }
            };

            aff_abs = GenerateMorphism(affordanceGraph, abstractGraph, aff_abs_vertices, aff_abs_edges);

            //actionGraph (generated from abstract)
            int[] arr = { 1, 2, 3, 0 };
            int pole = 3;
            actionGraph = new DLMGraph();
            var act_abs_vertices = new Dictionary<string, string>();

            ActionGraphGenerator.GenerateActionGraph(arr, pole, ThreePuzzleDict, ref actionGraph, ref act_abs_vertices);

            //action to abstract
            var act_abs_edges = new Dictionary<string, string>()
            {
                { "4->3" , ""}, { "3->4", ""}, {"4->2" , "" }, {"2->4" , "" },
                { "2->1" , ""}, { "1->2", ""}, {"3->1" , "" }, {"1->3" , "" }
            };
            
            act_abs = GenerateMorphism(actionGraph, abstractGraph, act_abs_vertices, act_abs_edges);


            DLMGCategory category = new DLMGCategory();

            interfaceAsPullback = category.GetPullback(act_abs, aff_abs);
        }


        private DLMGHomomorphism GenerateMorphism(DLMGraph g1, DLMGraph g2,Dictionary<string, string> v, Dictionary<string, string> e)
        {
            Dictionary<Vertex, Vertex> vertex = v.ToDictionary(
                kvp => new Vertex(kvp.Key),
                kvp => new Vertex(kvp.Value)
            );

            return new DLMGHomomorphism(g1, g2, vertex, e);
        }

    
        private DLMGraph GenerateAbstract()
        {
            Vertex ld = new Vertex("3");
            Vertex lu = new Vertex("1");
            Vertex rd = new Vertex("4");
            Vertex ru = new Vertex("2");

            List<DEdge> abstractEdges = new List<DEdge>() {
                new DEdge(lu, ru, ""),
                new DEdge(lu, ld, ""),
                new DEdge(ru, lu, ""),
                new DEdge(ld, lu, ""),
                new DEdge(rd, ru, ""),
                new DEdge(rd, ld, ""),
                new DEdge(ru, rd, ""),
                new DEdge(ld, rd, ""),
            };
            return new DLMGraph(abstractEdges);
        }

        private DLMGraph GenerateAffordance()
        {
            Vertex ld = new Vertex("L D");
            Vertex lu = new Vertex("L U");
            Vertex rd = new Vertex("R D");
            Vertex ru = new Vertex("R U");

            List<DEdge> affordanceEdges = new List<DEdge>() {
                new DEdge(lu, ru, "L"),
                new DEdge(lu, ld, "U"),
                new DEdge(ru, lu, "R"),
                new DEdge(ld, lu, "D"),
                new DEdge(rd, ru, "D"),
                new DEdge(rd, ld, "R"),
                new DEdge(ru, rd, "U"),
                new DEdge(ld, rd, "L"),
            };
            return new DLMGraph(affordanceEdges);
        }

        private Dictionary<int, List<Tuple<int, int>>> ThreePuzzleDict = new Dictionary<int, List<Tuple<int, int>>>()
        {
            {
                0, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(2,0),
                    new Tuple<int, int>(1,0),
                }
            },
            {
                1, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(3,1),
                    new Tuple<int, int>(0,1),
                }
            },
            {
                2, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(3,2),
                    new Tuple<int, int>(0,2),
                }
            },
            {
                3, new List<Tuple<int,int>>()
                {
                    new Tuple<int, int>(2,3),
                    new Tuple<int, int>(1,3),
                }
            }

        };

    }
}


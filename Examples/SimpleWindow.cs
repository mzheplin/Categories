using System;
using System.Collections.Generic;

namespace Categories.Examples
{
	public static class SimpleWindow
	{
        public static Pullback<DLMGraph> SimpleWindowPullback()
        { //action graph
            Vertex empty = new Vertex("_");
            Vertex a = new Vertex("a");
            Vertex b = new Vertex("b");
            Vertex exit = new Vertex("exit");

            List<DEdge> Actionedges = new List<DEdge>()
            {
                new DEdge(empty, a, "add a"),
                new DEdge(empty, b, "add b"),
                new DEdge(a, empty, "delete symbol"),
                new DEdge(b, empty, "delete symbol"),
                new DEdge(empty, empty, "delete symbol"),
                new DEdge(a, a, "add a"),
                new DEdge(b, b, "add b"),
                new DEdge(b, b, "add a"),
                new DEdge(a, a, "add b"),

                new DEdge(a, exit, "exit"),
                new DEdge(b, exit, "exit"),
                new DEdge(empty, exit, "exit"),
             };

            DLMGraph actionGraph = new DLMGraph(Actionedges);
            //affordance graph
            Vertex context = new Vertex("a, b, delete");
            Vertex emptyContext = new Vertex(" ");
            List<DEdge> Affedges = new List<DEdge>()
            {
                new DEdge(context, context, "click a"),
                new DEdge(context, context, "click b"),
                new DEdge(context, context, "click delete"),
                new DEdge(context, emptyContext, "click delete"),
            };
            DLMGraph affordanceGraph = new DLMGraph(Affedges);

            //abstract
            Vertex vertex = new Vertex("__");
            Vertex vertex1 = new Vertex("_");
            List<DEdge> abstractedges = new List<DEdge>()
            {
                new DEdge(vertex, vertex, "1"),
                new DEdge(vertex, vertex, "2"),
                new DEdge(vertex, vertex, "3"),
                new DEdge(vertex, vertex1, "3"),
            };
            DLMGraph abstractGraph = new DLMGraph(abstractedges);

            Dictionary<Vertex, Vertex> acab_v = new Dictionary<Vertex, Vertex>()
                { {empty,vertex },{a,vertex},{b,vertex}, {exit, vertex1 } };

            Dictionary<string, string> acab_e = new Dictionary<string, string>()
                {{"add a","1"},
                {"add b","2"},
                {"delete symbol","3"},
                {"exit", "3"}
            };
            DLMGHomomorphism ac_ab = new DLMGHomomorphism(actionGraph, abstractGraph, acab_v, acab_e);

            //affordance to abstract
            Dictionary<Vertex, Vertex> afab_v = new Dictionary<Vertex, Vertex>()
                { {context,vertex }, {emptyContext, vertex1 } };

            Dictionary<string, string> afab_e = new Dictionary<string, string>()
                {{"click a","1"},
                {"click b","2"},
                {"click delete","3"},
            };
            DLMGHomomorphism af_ab = new DLMGHomomorphism(affordanceGraph, abstractGraph, afab_v, afab_e);

            DLMGCategory category = new DLMGCategory();

            var pullback = category.GetPullback(ac_ab, af_ab);
            return pullback;
		}
	}
}


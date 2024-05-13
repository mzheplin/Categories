using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Categories
{
	public static class CoffeeExamples
	{
		public static Pullback<DLMGraph> ItalianExpressoMaker()
		{
            //abstract graph
            Vertex start = new Vertex("initial stage");
            Vertex int1 = new Vertex("intermediate stage 1");
            Vertex int2 = new Vertex("intermediate stage 2");
            Vertex pen = new Vertex("penultimate stage");
            Vertex final = new Vertex("final stage");

            List<DEdge> edges = new List<DEdge>() {
                new DEdge(start, int1, "add water"),
                new DEdge(start, int2, "add coffee grounds"),
                new DEdge(int1, pen, "add coffee grounds"),
                new DEdge(int2, pen, "add water"),
                new DEdge(int1, int1, "intermediate steps"),
                new DEdge(int2, int2, "intermediate steps"),
                new DEdge(pen, pen, "intermediate steps"),
                new DEdge(pen, final, "cook")
            };
            DLMGraph abstractGraph = new DLMGraph(edges);


            //action graph (what task does person do)
            Vertex st1 = new Vertex("state 1");
            Vertex st2 = new Vertex("state 2");
            Vertex st3 = new Vertex("state 3");
            Vertex st4 = new Vertex("state 4");
            Vertex st5 = new Vertex("state 5");
            Vertex st6 = new Vertex("state 6");

            List<DEdge> actionEdges = new List<DEdge>() {
                new DEdge(st1, st2, "add water"),
                new DEdge(st2, st3, "prepare object"),
                new DEdge(st3, st4, "add coffee grounds"),
                new DEdge(st4, st5, "prepare object"),
                new DEdge(st5, st6, "boil water")
            };
            DLMGraph actionGraph = new DLMGraph(actionEdges);


            //affordance graph (how does person do task)
            Vertex af1 = new Vertex("affordances 1");
            Vertex af2 = new Vertex("affordances 2");
            Vertex af3 = new Vertex("affordances 3");
            Vertex af4 = new Vertex("affordances 4");
            Vertex af5 = new Vertex("affordances 5");
            Vertex af6 = new Vertex("affordances 6");

            List<DEdge> affordanceEdges = new List<DEdge>() {
                new DEdge(af1, af2, "Add water to the bottom half"),
                new DEdge(af2, af3, "Put Funnel on the bottom half"),
                new DEdge(af3, af4, "Lay coffee into Funnel"),
                new DEdge(af4, af5, "Assemble together top and bottom"),
                new DEdge(af5, af6, "Place in stove until brewing process is complete")
            };
            DLMGraph affordanceGraph = new DLMGraph(affordanceEdges);

            //action to abstract
            Dictionary<Vertex, Vertex> acab_v = new Dictionary<Vertex, Vertex>()
                { {st1,start },{st2,int1},{st3,int1}, {st4,pen },{ st5,pen},{st6,final} };

            Dictionary<string, string> acab_e = new Dictionary<string, string>()
                {{"add water","add water"},
                {"prepare object","intermediate steps"},
                {"add coffee grounds","add coffee grounds"},
                {"boil water", "cook"}
            };
            DLMGHomomorphism ac_ab = new DLMGHomomorphism(actionGraph, abstractGraph, acab_v, acab_e);

            //affordance to abstract
            Dictionary<Vertex, Vertex> afab_v = new Dictionary<Vertex, Vertex>()
                { {af1,start },{af2,int1},{af3,int1}, {af4,pen },{ af5,pen},{af6,final} };

            Dictionary<string, string> afab_e = new Dictionary<string, string>()
                {{"Add water to the bottom half","add water"},
                {"Put Funnel on the bottom half","intermediate steps"},
                {"Assemble together top and bottom","intermediate steps"},
                {"Lay coffee into Funnel","add coffee grounds"},
                {"Place in stove until brewing process is complete", "cook"}
            };
            DLMGHomomorphism af_ab = new DLMGHomomorphism(affordanceGraph, abstractGraph, afab_v, afab_e);

            DLMGCategory category = new DLMGCategory();

            var pullback = category.GetPullback(ac_ab, af_ab);
            return pullback;
        }

        public static Pullback<DLMGraph> TurkExrpessoMaker()
        {
            //action graph
            Vertex st1 = new Vertex("state 1");
            Vertex st2 = new Vertex("state 2");
            Vertex st3 = new Vertex("state 3");
            Vertex st4 = new Vertex("state 4");
            Vertex st5 = new Vertex("state 5");


            List<DEdge> actionEdges = new List<DEdge>()
            {
                new DEdge(st1,st2,"add coffee"),
                new DEdge(st2,st3,"add water"),
                new DEdge(st5,st3,"add coffee"),
                new DEdge(st1,st5,"add water"),
                new DEdge(st3,st4, "boil")
            };

            DLMGraph actionGraph = new DLMGraph(actionEdges);

            Vertex af1 = new Vertex("affordance 1");
            Vertex af2 = new Vertex("affordance 2");
            Vertex af3 = new Vertex("affordance 3");
            Vertex af4 = new Vertex("affordance 4");
            Vertex af5 = new Vertex("affordance 5");


            List<DEdge> affordanceEdges = new List<DEdge>()
            {
                new DEdge(af1,af2,"pour coffee grounds on the bottom of the Turk"),
                new DEdge(af2,af3,"pour water on top"),
                new DEdge(af5,af3,"pour coffee grounds on the bottom of the Turk"),
                new DEdge(af1,af5,"pour water on top"),
                new DEdge(af3,af4,"place in stove until brewing process is complete")
            };

            DLMGraph affordanceGraph = new DLMGraph(affordanceEdges);

            Vertex start = new Vertex("initial stage");
            Vertex int1 = new Vertex("intermediate stage 1");
            Vertex int2 = new Vertex("intermediate stage 2");
            Vertex pen = new Vertex("penultimate stage");
            Vertex final = new Vertex("final stage");

            List<DEdge> edges = new List<DEdge>() {
                new DEdge(start, int1, "add water"),
                new DEdge(start, int2, "add coffee grounds"),
                new DEdge(int1, pen, "add coffee grounds"),
                new DEdge(int2, pen, "add water"),
                new DEdge(int1, int1, "intermediate steps"),
                new DEdge(int2, int2, "intermediate steps"),
                new DEdge(pen, pen, "intermediate steps"),
                new DEdge(pen, final, "cook")
            };
            DLMGraph abstractGraph = new DLMGraph(edges);


            Dictionary<Vertex, Vertex> acab_v = new Dictionary<Vertex, Vertex>()
                { {st1,start },{st2,int2},{st3,pen}, {st4,final }, {st5,int1 } };

            Dictionary<string, string> acab_e = new Dictionary<string, string>()
                {{"add water","add water"},
                {"add coffee","add coffee grounds"},
                {"boil", "cook"}
            };
            DLMGHomomorphism ac_ab = new DLMGHomomorphism(actionGraph, abstractGraph, acab_v, acab_e);

            //affordance to abstract
            Dictionary<Vertex, Vertex> afab_v = new Dictionary<Vertex, Vertex>()
                { {af1,start },{af2,int2},{af3,pen}, {af4,final },{af5,int1 }};

            Dictionary<string, string> afab_e = new Dictionary<string, string>()
                {{"pour coffee grounds on the bottom of the Turk","add coffee grounds"},
                {"pour water on top","add water"},
                {"place in stove until brewing process is complete", "cook"}
            };
            DLMGHomomorphism af_ab = new DLMGHomomorphism(affordanceGraph, abstractGraph, afab_v, afab_e);

            DLMGCategory category = new DLMGCategory();

            var pullback = category.GetPullback(ac_ab, af_ab);
            return pullback;
        }

        public static Pushout<DLMGraph> CoffeeMachineAbstraction()
        {
            Vertex start = new Vertex("initial stage");
            Vertex int1 = new Vertex("intermediate stage 1");
            Vertex int2 = new Vertex("intermediate stage 2");
            Vertex pen = new Vertex("penultimate stage");
            Vertex final = new Vertex("final stage");

            List<DEdge> edges = new List<DEdge>() {
                new DEdge(start, int1, "add water"),
                new DEdge(start, int2, "add coffee grounds"),
                new DEdge(int1, pen, "add coffee grounds"),
                new DEdge(int2, pen, "add water"),
                new DEdge(int1, int1, "intermediate steps"),
                new DEdge(int2, int2, "intermediate steps"),
                new DEdge(pen, pen, "intermediate steps"),
                new DEdge(pen, final, "cook")
            };
            DLMGraph CabstractGraph = new DLMGraph(edges);


            Vertex off = new Vertex("off");
            Vertex on = new Vertex("on");

            List<DEdge> edges1 = new List<DEdge>()
            {
                new DEdge(off,on,"turn on"),
                new DEdge(on,off,"turn off")
            };
            DLMGraph MabstractGraph = new DLMGraph(edges1);

            Vertex basev = new Vertex("start"); //vertex indicates that smth is ready to work
            DLMGraph baseGraph = new DLMGraph();
            baseGraph.Add(basev);

            Dictionary<Vertex, Vertex> b_cab_v = new Dictionary<Vertex, Vertex>()
                { {basev,start }};

            Dictionary<string, string> b_cab_e = new Dictionary<string, string>() { };

            DLMGHomomorphism b_cab = new DLMGHomomorphism(baseGraph, CabstractGraph, b_cab_v, b_cab_e);

            Dictionary<Vertex, Vertex> b_mab_v = new Dictionary<Vertex, Vertex>()
                { {basev,on }};

            Dictionary<string, string> b_mab_e = new Dictionary<string, string>() { };

            DLMGHomomorphism b_mab = new DLMGHomomorphism(baseGraph, MabstractGraph, b_mab_v, b_mab_e);

            DLMGCategory category = new DLMGCategory();

            var pushout = category.GetPushout(b_mab, b_cab);
            return pushout;

        }

        public static Pullback<DLMGraph> CoffeeMachineMaker()
        {
            var abstractGraph = CoffeeMachineAbstraction().pushout;

            //action graph (what task does person do)
            Vertex st1 = new Vertex("state 1");
            Vertex st2 = new Vertex("state 2");
            Vertex st3 = new Vertex("state 3");
            Vertex st4 = new Vertex("state 4");
            Vertex st5 = new Vertex("state 5");
            Vertex st6 = new Vertex("state 6");
            Vertex st7 = new Vertex("state 7");

            List<DEdge> actionEdges = new List<DEdge>() {
                new DEdge(st1, st2, "turn on"),
                new DEdge(st2, st1, "turn off"),
                new DEdge(st2, st3, "pour coffee beans"),
                new DEdge(st3, st4, "grind coffee beans"),
                new DEdge(st4, st5, "add water"),
                new DEdge(st5, st6, "prepare the cup"),
                new DEdge(st6, st7, "cook coffee")
            };
            DLMGraph actionGraph = new DLMGraph(actionEdges);
            Console.WriteLine(actionGraph);
            //action to abstract
            Dictionary<Vertex, Vertex> acab_v = new Dictionary<Vertex, Vertex>()
              { {st1,abstractGraph.Vertices[1] },
                {st2,abstractGraph.Vertices[0]},
                {st3,abstractGraph.Vertices[3]},
                {st4,abstractGraph.Vertices[3] },
                {st5,abstractGraph.Vertices[4]},
                {st6,abstractGraph.Vertices[4]},
                {st7,abstractGraph.Vertices[5] },
            };

            Dictionary<string, string> acab_e = new Dictionary<string, string>(){
                {"turn on","turn on - 0"},
                {"turn off","turn off - 0"},
                {"pour coffee beans","add coffee grounds - 1"},
                {"add water", "add water - 1"},
                {"grind coffee beans","intermediate steps - 1" },
                {"prepare the cup","intermediate steps - 1"},
                {"cook coffee", "cook - 1" }
            };
            DLMGHomomorphism ac_ab = new DLMGHomomorphism(actionGraph, abstractGraph, acab_v, acab_e);


            //affordance graph (how does person do task)
            Vertex af1 = new Vertex("affordances 1");
            Vertex af2 = new Vertex("affordances 2");
            Vertex af3 = new Vertex("affordances 3");
            Vertex af4 = new Vertex("affordances 4");
            Vertex af5 = new Vertex("affordances 5");
            Vertex af6 = new Vertex("affordances 6");
            Vertex af7 = new Vertex("affordances 7");

            List<DEdge> affordanceEdges = new List<DEdge>() {
                new DEdge(af1, af2, "press the button Power"),
                new DEdge(af2, af1, "press the button Power off"),
                new DEdge(af2, af3, "fill the container for coffee beans"),
                new DEdge(af3, af4, "press the button Grind"),
                new DEdge(af4, af5, "fill the water container"),
                new DEdge(af5, af6, "put the cup"),
                new DEdge(af6, af7, "press the button with coffee icon"),
            };
            DLMGraph affordanceGraph = new DLMGraph(affordanceEdges);

           
            //affordance to abstract
            Dictionary<Vertex, Vertex> afab_v = new Dictionary<Vertex, Vertex>()
            {
                { af1,abstractGraph.Vertices[1] },
                { af2,abstractGraph.Vertices[0]},
                { af3,abstractGraph.Vertices[3]},
                { af4,abstractGraph.Vertices[3] },
                { af5,abstractGraph.Vertices[4]},
                { af6,abstractGraph.Vertices[4]},
                { af7,abstractGraph.Vertices[5] },
            };

            Dictionary<string, string> afab_e = new Dictionary<string, string>()
                {{"press the button Power","turn on - 0"},
                {"press the button Power off","turn off - 0"},
                {"fill the container for coffee beans","add coffee grounds - 1"},
                {"fill the water container", "add water - 1"},
                {"press the button Grind","intermediate steps - 1" },
                {"put the cup","intermediate steps - 1"},
                {"press the button with coffee icon", "cook - 1" }
            };
            DLMGHomomorphism af_ab = new DLMGHomomorphism(affordanceGraph, abstractGraph, afab_v, afab_e);

            DLMGCategory category = new DLMGCategory();

            var pullback = category.GetPullback(ac_ab, af_ab);
            return pullback;
        }

    }
}


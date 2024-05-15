using System;
using System.Collections.Generic;
using System.Linq;

namespace Categories
{
    public class DLMGCategory : ICategory<DLMGraph>  //directed labeled graph category
    {
        private DLMGraph initialObject;
        private DLMGraph terminalObject;

        public DLMGraph GetInitialObject()
        {
            if(initialObject is null)
            {
                initialObject = new DLMGraph();
            }
            return initialObject;
        }

        public DLMGraph GetTerminalObject()
        {
            if (terminalObject is null)
            {
                terminalObject = new DLMGraph();
                var v = new Vertex("id");
                var e = new DEdge(v, v, "id");
                terminalObject.Add(v);
                terminalObject.Add(e);
            }
            return terminalObject;
        }

        public Coproduct<DLMGraph> GetCoproduct(DLMGraph t1, DLMGraph t2)
        {
            DLMGraph copruduct = new DLMGraph();

            Dictionary<Vertex, Vertex> vMap1 = new Dictionary<Vertex, Vertex>();
            Dictionary<string, string> eMap1 = new Dictionary<string, string>();
            Dictionary<Vertex, Vertex> vMap2 = new Dictionary<Vertex, Vertex>();
            Dictionary<string, string> eMap2 = new Dictionary<string, string>();

            foreach (var v in t1.Vertices)
            {
                var newVertex = new Vertex(v.Label, "0");
                copruduct.Add(newVertex);
                vMap1.Add(v, newVertex);
            };

            foreach (var e in t1.Edges)
            {
                var newEdge = new DEdge(
                   vMap1[e.Tail],
                   vMap1[e.Head],
                   e.Label,"0");
                copruduct.Add(newEdge);
                if (!eMap1.ContainsKey(e.Label))
                    eMap1.Add(e.Label, newEdge.Label);
            };

            foreach (var v in t2.Vertices)
            {
                var newVertex = new Vertex(v.Label,"1");
                copruduct.Add(newVertex);
                vMap2.Add(v, newVertex);
            };

            foreach (var e in t2.Edges)
            {
                var newEdge = new DEdge(
                    vMap2[e.Tail],
                    vMap2[e.Head],
                    e.Label,"1");
                copruduct.Add(newEdge);
                if (!eMap2.ContainsKey(e.Label))
                    eMap2.Add(e.Label, newEdge.Label);
            }

            DLMGHomomorphism hom1 = new DLMGHomomorphism(t1, copruduct, vMap1, eMap1);
            DLMGHomomorphism hom2 = new DLMGHomomorphism(t2, copruduct, vMap2, eMap2);

            return new Coproduct<DLMGraph>(copruduct, hom1, hom2);
        }

        public Product<DLMGraph> GetProduct(DLMGraph t1, DLMGraph t2)
        {
            DLMGraph product = new DLMGraph();

            Dictionary<Vertex, Vertex> vMap1 = new Dictionary<Vertex, Vertex>();
            Dictionary<Vertex, Vertex> vMap2 = new Dictionary<Vertex, Vertex>();

            foreach (var v in t1.Vertices)
            {
                foreach (var w in t2.Vertices)
                {
                    var newVertex = new Vertex($"{v.Label} - {w.Label}");
                    product.Add(newVertex);
                    vMap1.Add(newVertex, v);
                    vMap2.Add(newVertex, w);
                }
            }

            Dictionary<string, string> eMap1 = new Dictionary<string, string>();
            Dictionary<string, string> eMap2 = new Dictionary<string, string>();

            foreach (var e in t1.Edges)
            {
                foreach (var h in t2.Edges)
                {
                    var newEdge = new DEdge(
                        new Vertex($"{e.Tail.Label} - {h.Tail.Label}"),
                        new Vertex($"{e.Head.Label} - {h.Head.Label}"),
                        $"{e.Label} - {h.Label}"
                        );
                    product.Add(newEdge);
                    if (!eMap1.ContainsKey(newEdge.Label))
                        eMap1.Add(newEdge.Label, e.Label);
                    if (!eMap2.ContainsKey(newEdge.Label))
                        eMap2.Add(newEdge.Label, h.Label);
                }
            }
            DLMGHomomorphism hom1 = new DLMGHomomorphism(product, t1, vMap1, eMap1);
            DLMGHomomorphism hom2 = new DLMGHomomorphism(product, t2, vMap2, eMap2);

            return new Product<DLMGraph>(product, hom1, hom2);
        }

        public Pullback<DLMGraph> GetPullback(Map<DLMGraph> h1, Map<DLMGraph> h2)
        {
            if (h1.Target != h2.Target) return null;

            DLMGraph pullback = new DLMGraph();

            Dictionary<Vertex, Vertex> vMap1 = new Dictionary<Vertex, Vertex>();
            Dictionary<Vertex, Vertex> vMap2 = new Dictionary<Vertex, Vertex>();
            Dictionary<string, string> eMap1 = new Dictionary<string, string>();
            Dictionary<string, string> eMap2 = new Dictionary<string, string>();

            foreach (var v in h1.Source.Vertices)
            {
                foreach (var w in h2.Source.Vertices)
                {
                    if (((DLMGHomomorphism)h1).Vertex_Map[v] ==
                       ((DLMGHomomorphism)h2).Vertex_Map[w])
                    {
                        var newVertex = new Vertex(v.Label, w.Label);
                        pullback.Add(newVertex);
                        vMap1.Add(newVertex, v);
                        vMap2.Add(newVertex, w);
                    }
                }
            }

            foreach (var e in h1.Source.Edges)
            {
                foreach (var h in h2.Source.Edges)
                {
                    if (((DLMGHomomorphism)h1).Edge_Map[e] ==
                       ((DLMGHomomorphism)h2).Edge_Map[h])
                    {
                        var newEdge = new DEdge(
                        new Vertex(e.Tail.Label, h.Tail.Label),
                        new Vertex(e.Head.Label, h.Head.Label),
                        e.Label, h.Label
                        );
                        pullback.Add(newEdge);
                        if (!eMap1.ContainsKey(newEdge.Label))
                            eMap1.Add(newEdge.Label, e.Label);
                        if (!eMap2.ContainsKey(newEdge.Label))
                            eMap2.Add(newEdge.Label, h.Label);
                    }
                }
            }

            DLMGHomomorphism hom1 = new DLMGHomomorphism(pullback, h1.Source, vMap1, eMap1);
            DLMGHomomorphism hom2 = new DLMGHomomorphism(pullback, h2.Source, vMap2, eMap2);

            return new Pullback<DLMGraph>(pullback, hom1, hom2);
        }

        public Pushout<DLMGraph> GetPushout(Map<DLMGraph> h1, Map<DLMGraph> h2)
        {
            if (h1.Source != h2.Source) return null;

            var coproduct = GetCoproduct(h1.Target, h2.Target);

            var h1_upd = h1 * coproduct.hom1;
            var h2_upd = h2 * coproduct.hom2;

            var eq_v = GenerateVertexEquivalenceClasses(h1_upd, h2_upd);
            var eq_l = GenerateLabelEquivalenceClasses(h1_upd, h2_upd);

            var pushout = GlueCoproduct(coproduct.coproduct, eq_v, eq_l);

            var tau = GenerateHomomorphism(coproduct.coproduct, pushout, eq_v, eq_l);

            var f = coproduct.hom1 * tau;
            var g = coproduct.hom2 * tau;
            return new Pushout<DLMGraph>(pushout, f, g);
        }

        internal List<EquivalenceClass<Vertex>> GenerateVertexEquivalenceClasses(Map<DLMGraph> h1, Map<DLMGraph> h2)
        {
            List<EquivalenceClass<Vertex>> eq_v = new List<EquivalenceClass<Vertex>>();
            var c1 = h1.Source;
            var c2 = h1.Target;
            foreach (var vertex in c1.Vertices)
            {
                var a = ((DLMGHomomorphism)h1).Vertex_Map[vertex];
                var b = ((DLMGHomomorphism)h2).Vertex_Map[vertex];

                var eq_v_a = eq_v.FirstOrDefault(x => x.elements.Contains(a));
                if (eq_v_a != null)
                {
                    if (!eq_v_a.elements.Contains(b))
                        eq_v_a.elements.Add(b);
                    continue;
                }

                var eq_v_b = eq_v.FirstOrDefault(x => x.elements.Contains(b));
                if (eq_v_b != null)
                {
                    if (!eq_v_b.elements.Contains(a))
                        eq_v_b.elements.Add(a);
                    continue;
                }

                var @class = new EquivalenceClass<Vertex>(vertex);
                @class.elements.Add(a);
                @class.elements.Add(b);
                eq_v.Add(@class);
            }

            foreach (var vertex in c2.Vertices)
            {
                var eq_class = eq_v.FirstOrDefault(x => x.elements.Contains(vertex));
                if (eq_class != null) continue;

                var @class = new EquivalenceClass<Vertex>(vertex);
                @class.elements.Add(vertex);
                eq_v.Add(@class);
            }

            return eq_v;
        }

        internal List<EquivalenceClass<string>> GenerateLabelEquivalenceClasses(Map<DLMGraph> h1, Map<DLMGraph> h2)
        {
            List<EquivalenceClass<string>> eq_l = new List<EquivalenceClass<string>>();
            var c1 = h1.Source;
            var c2 = h1.Target;
            foreach (var edge in c1.Edges)
            {
                var a = ((DLMGHomomorphism)h1).Edge_Labeles_Map[edge.Label];
                var b = ((DLMGHomomorphism)h2).Edge_Labeles_Map[edge.Label];

                var eq_v_a = eq_l.FirstOrDefault(x => x.elements.Contains(a));
                if (eq_v_a != null)
                {
                    if (!eq_v_a.elements.Contains(b))
                        eq_v_a.elements.Add(b);
                    continue;
                }

                var eq_v_b = eq_l.FirstOrDefault(x => x.elements.Contains(b));
                if (eq_v_b != null)
                {
                    if (!eq_v_b.elements.Contains(a))
                        eq_v_b.elements.Add(a);
                    continue;
                }

                var @class = new EquivalenceClass<string>(edge.Label);
                @class.elements.Add(a);
                @class.elements.Add(b);
                eq_l.Add(@class);
            }

            foreach (var edge in c2.Edges)
            {
                var eq_class = eq_l.FirstOrDefault(x => x.elements.Contains(edge.Label));
                if (eq_class != null) continue;

                var @class = new EquivalenceClass<string>(edge.Label);
                @class.elements.Add(edge.Label);
                eq_l.Add(@class);
            }
            return eq_l;
        }

        internal DLMGraph GlueCoproduct(DLMGraph coproduct, List<EquivalenceClass<Vertex>> v, List<EquivalenceClass<string>> l)
        {
            var pushout = new DLMGraph();
            var reps = v.Select(x => x.representative).ToList();
            reps.ForEach(x => pushout.Add(x));

            foreach (var edge in coproduct.Edges)
            {
                var tail = v.FirstOrDefault(x => x.elements.Contains(edge.Tail)).representative;
                var head = v.FirstOrDefault(x => x.elements.Contains(edge.Head)).representative;
                var label = l.FirstOrDefault(x => x.elements.Contains(edge.Label)).representative;
                var newEdge = new DEdge(tail, head, label);
                pushout.Add(newEdge);
            }

            return pushout;
        }

        internal DLMGHomomorphism GenerateHomomorphism(DLMGraph coproduct, DLMGraph pushout, List<EquivalenceClass<Vertex>> v, List<EquivalenceClass<string>> l)
        {
            var v_map = new Dictionary<Vertex, Vertex>();
            v.ForEach(eq => eq.elements.ForEach(e => v_map.Add(e, eq.representative)));
            var l_map = new Dictionary<string, string>();
            l.ForEach(eq => eq.elements.ForEach(e => l_map.Add(e, eq.representative)));

            return new DLMGHomomorphism(coproduct, pushout, v_map, l_map);
        }
    }
}











    /*public Pushout<DLMGraph> GetPushout1(Map<DLMGraph> h1, Map<DLMGraph> h2)
        {
            if (h1.Source != h2.Source) return null;

            DLMGraph pushout = new DLMGraph();

            Dictionary<Vertex, Vertex> vMap1 = new Dictionary<Vertex, Vertex>();
            Dictionary<Vertex, Vertex> vMap2 = new Dictionary<Vertex, Vertex>();
            Dictionary<string, string> eMap1 = new Dictionary<string, string>();
            Dictionary<string, string> eMap2 = new Dictionary<string, string>();

            Glue(h1, h2, ref vMap1, ref vMap2, ref eMap1, ref eMap2);

            foreach (var v in h1.Target.Vertices)
            {
                if (!vMap1.ContainsKey(v))
                {
                    var newVertex = new Vertex($"{v.Label} - 0");
                    pushout.Add(newVertex);
                    vMap1.Add(v, newVertex);
                }

            };
            foreach (var v in h2.Target.Vertices)
            {
                if (!vMap2.ContainsKey(v))
                {
                    var newVertex = new Vertex($"{v.Label} - 1");
                    pushout.Add(newVertex);
                    vMap2.Add(v, newVertex);
                }
            };

            foreach (var e in h1.Target.Edges)
            {
                string label = eMap1.ContainsKey(e.Label)
                    ? eMap1[e.Label] : $"{e.Label} - 0";
                var newEdge = new DEdge(
                    vMap1[e.Tail],
                    vMap1[e.Head],
                    label);
                pushout.Add(newEdge);
                if (!eMap1.ContainsKey(e.Label))
                    eMap1.Add(e.Label, newEdge.Label);
            };

            foreach (var e in h2.Target.Edges)
            {
                string label = eMap2.ContainsKey(e.Label)
                    ? eMap2[e.Label] : $"{e.Label} - 1";
                var newEdge = new DEdge(
                    vMap2[e.Tail],
                    vMap2[e.Head],
                    label);
                pushout.Add(newEdge);
                if (!eMap2.ContainsKey(e.Label))
                    eMap2.Add(e.Label, newEdge.Label);
            }

            DLMGHomomorphism hom1 = new DLMGHomomorphism(h1.Target, pushout, vMap1, eMap1);
            DLMGHomomorphism hom2 = new DLMGHomomorphism(h2.Target, pushout, vMap2, eMap2);

            return new Pushout<DLMGraph>(pushout, hom1, hom2);
        }



        private void Glue(Map<DLMGraph> h1,
            Map<DLMGraph> h2,
            ref Dictionary<Vertex, Vertex> vMap1,
            ref Dictionary<Vertex, Vertex> vMap2,
            ref Dictionary<string, string> eMap1,
            ref Dictionary<string, string> eMap2)
        {
            List<Tuple<Vertex, Vertex>> vertices = new List<Tuple<Vertex, Vertex>>();
            List<Tuple<string, string>> labeles = new List<Tuple<string, string>>();

            foreach (var edge in h1.Source.Edges)
            {
                var edge1 = ((DLMGHomomorphism)h1).Edge_Map[edge];
                var edge2 = ((DLMGHomomorphism)h2).Edge_Map[edge];
                vertices.Add(new Tuple<Vertex, Vertex>(edge1.Tail, edge2.Tail));
                vertices.Add(new Tuple<Vertex, Vertex>(edge1.Head, edge2.Head));
                labeles.Add(new Tuple<string, string>(edge1.Label, edge2.Label));
            }
            for(int i= 0; i< vertices.Count; i++)
            {
                AddRecursivelyVertices(vertices[i].Item1, vertices[i].Item1.Label, true, vertices, ref vMap1, ref vMap2);
            }
            for (int i = 0; i < labeles.Count; i++)
            {
                AddRecursivelyLabeles(labeles[i].Item1, labeles[i].Item1, true, labeles, ref eMap1, ref eMap2);
            }

        }


        private void AddRecursivelyVertices(Vertex a,string val, bool isLeft,
            List<Tuple<Vertex, Vertex>> vertices,
            ref Dictionary<Vertex, Vertex> vMap1,
            ref Dictionary<Vertex, Vertex> vMap2)
        {
            List<Vertex> equal;
            if (isLeft)
            {
                if (vMap1.ContainsKey(a)) return;
               
                vMap1.Add(a, new Vertex(val));

                equal = vertices.Where(x => x.Item1 == a).Select(x => x.Item2).ToList(); 
            }
            else
            {
                if (vMap2.ContainsKey(a)) return;
                
                vMap2.Add(a, new Vertex(val));

                equal = vertices.Where(x => x.Item2 == a).Select(x => x.Item1).ToList();
            }

            foreach (var e in equal)
            {
                AddRecursivelyVertices(e, val, !isLeft, vertices, ref vMap1, ref vMap2);
            }
        }


        private void AddRecursivelyLabeles(string a, string val, bool isLeft,
            List<Tuple<string, string>> labeles,
            ref Dictionary<string, string> eMap1,
            ref Dictionary<string, string> eMap2)
        {
            List<string> equal;
            if (isLeft)
            {
                if (eMap1.ContainsKey(a)) return;

                eMap1.Add(a, val);

                equal = labeles.Where(x => x.Item1 == a).Select(x => x.Item2).ToList();
            }
            else
            {
                if (eMap2.ContainsKey(a)) return;

                eMap2.Add(a,val);

                equal = labeles.Where(x => x.Item2 == a).Select(x => x.Item1).ToList();
            }

            foreach (var e in equal)
            {
                AddRecursivelyLabeles(e, val, !isLeft, labeles, ref eMap1, ref eMap2);
            }
        }
        */

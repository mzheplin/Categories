using System;
using System.Collections.Generic;
using System.Linq;

namespace Categories
{
    public class DLGCategory : ICategory<DLGraph>  //directed labeled graph category
    {

        public Coproduct<DLGraph> GetCoproduct(DLGraph t1, DLGraph t2)
        {
            DLGraph copruduct = new DLGraph();

            Dictionary<Vertex, Vertex> vMap1 = new Dictionary<Vertex, Vertex>();
            Dictionary<string, string> eMap1 = new Dictionary<string, string>();
            Dictionary<Vertex, Vertex> vMap2 = new Dictionary<Vertex, Vertex>();
            Dictionary<string, string> eMap2 = new Dictionary<string, string>();

            foreach (var v in t1.Vertices)
            {
                var newVertex = new Vertex($"{v.Label} - 0");
                copruduct.Add(newVertex);
                vMap1.Add(v, newVertex);
            };

            foreach (var e in t1.Edges)
            {
                var newEdge = new DEdge(
                    new Vertex($"{e.Tail.Label} - 0"),
                    new Vertex($"{e.Head.Label} - 0"),
                    $"{e.Label} - 0");
                copruduct.Add(newEdge);
                if (!eMap1.ContainsKey(e.Label))
                    eMap1.Add(e.Label, newEdge.Label);
            };

            foreach (var v in t2.Vertices)
            {
                var newVertex = new Vertex($"{v.Label} - 1");
                copruduct.Add(newVertex);
                vMap2.Add(v, newVertex);
            };

            foreach (var e in t2.Edges)
            {
                var newEdge = new DEdge(
                    new Vertex($"{e.Tail.Label} - 1"),
                    new Vertex($"{e.Head.Label} - 1"),
                    $"{e.Label} - 1");
                copruduct.Add(newEdge);
                if (!eMap2.ContainsKey(e.Label))
                    eMap2.Add(e.Label, newEdge.Label);
            }

            DLGHomomorphism hom1 = new DLGHomomorphism(t1, copruduct, vMap1, eMap1);
            DLGHomomorphism hom2 = new DLGHomomorphism(t2, copruduct, vMap2, eMap2);

            return new Coproduct<DLGraph>(copruduct, hom1, hom2);
        }

        public Product<DLGraph> GetProduct(DLGraph t1, DLGraph t2)
        {
            DLGraph product = new DLGraph();

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
            DLGHomomorphism hom1 = new DLGHomomorphism(product, t1, vMap1, eMap1);
            DLGHomomorphism hom2 = new DLGHomomorphism(product, t2, vMap2, eMap2);

            return new Product<DLGraph>(product, hom1, hom2);
        }

        public Pullback<DLGraph> GetPullback(Map<DLGraph> h1, Map<DLGraph> h2)
        {
            if (h1.Target != h2.Target) return null;

            DLGraph pullback = new DLGraph();

            Dictionary<Vertex, Vertex> vMap1 = new Dictionary<Vertex, Vertex>();
            Dictionary<Vertex, Vertex> vMap2 = new Dictionary<Vertex, Vertex>();
            Dictionary<string, string> eMap1 = new Dictionary<string, string>();
            Dictionary<string, string> eMap2 = new Dictionary<string, string>();

            foreach (var v in h1.Source.Vertices)
            {
                foreach (var w in h2.Source.Vertices)
                {
                    if (((DLGHomomorphism)h1).Vertex_Map[v] ==
                       ((DLGHomomorphism)h2).Vertex_Map[w])
                    {
                        var newVertex = new Vertex($"{v.Label} - {w.Label}");
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
                    if (((DLGHomomorphism)h1).Edge_Map[e] ==
                       ((DLGHomomorphism)h2).Edge_Map[h])
                    {
                        var newEdge = new DEdge(
                        new Vertex($"{e.Tail.Label} - {h.Tail.Label}"),
                        new Vertex($"{e.Head.Label} - {h.Head.Label}"),
                        $"{e.Label} - {h.Label}"
                        );
                        pullback.Add(newEdge);
                        if (!eMap1.ContainsKey(newEdge.Label))
                            eMap1.Add(newEdge.Label, e.Label);
                        if (!eMap2.ContainsKey(newEdge.Label))
                            eMap2.Add(newEdge.Label, h.Label);
                    }
                }
            }

            DLGHomomorphism hom1 = new DLGHomomorphism(pullback, h1.Source, vMap1, eMap1);
            DLGHomomorphism hom2 = new DLGHomomorphism(pullback, h2.Source, vMap2, eMap2);

            return new Pullback<DLGraph>(pullback, hom1, hom2);
        }

        public Pushout<DLGraph> GetPushout(Map<DLGraph> h1, Map<DLGraph> h2)
        {
            if (h1.Source != h2.Source) return null;

            DLGraph pushout = new DLGraph();

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

            DLGHomomorphism hom1 = new DLGHomomorphism(h1.Target, pushout, vMap1, eMap1);
            DLGHomomorphism hom2 = new DLGHomomorphism(h2.Target, pushout, vMap2, eMap2);

            return new Pushout<DLGraph>(pushout, hom1, hom2);
        }



        private void Glue(Map<DLGraph> h1,
            Map<DLGraph> h2,
            ref Dictionary<Vertex, Vertex> vMap1,
            ref Dictionary<Vertex, Vertex> vMap2,
            ref Dictionary<string, string> eMap1,
            ref Dictionary<string, string> eMap2)
        {
            List<Tuple<Vertex, Vertex>> vertices = new List<Tuple<Vertex, Vertex>>();
            List<Tuple<string, string>> labeles = new List<Tuple<string, string>>();

            foreach (var edge in h1.Source.Edges)
            {
                var edge1 = ((DLGHomomorphism)h1).Edge_Map[edge];
                var edge2 = ((DLGHomomorphism)h2).Edge_Map[edge];
                vertices.Add(new Tuple<Vertex, Vertex>(edge1.Tail, edge2.Tail));
                vertices.Add(new Tuple<Vertex, Vertex>(edge1.Head, edge2.Head));
                labeles.Add(new Tuple<string, string>(edge1.Label, edge2.Label));
            }
            for(int i= 0; i< vertices.Count; i++)
            {
                AddRecursivelyVertices(vertices[i].Item1,$"{i+1}", true, vertices, ref vMap1, ref vMap2);
            }
            for (int i = 0; i < labeles.Count; i++)
            {
                AddRecursivelyLabeles(labeles[i].Item1, $"{i + 1}", true, labeles, ref eMap1, ref eMap2);
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

    }
}

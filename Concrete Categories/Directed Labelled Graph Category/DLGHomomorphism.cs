using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Categories
{
    public class DLMGHomomorphism : Map<DLMGraph>
    {
        private readonly Dictionary<Vertex, Vertex> vertex_map_;
        private readonly Dictionary<string, string> edge_labeles_map_;
        private Dictionary<DEdge, DEdge> edge_map_;

        public Dictionary<Vertex, Vertex> Vertex_Map => vertex_map_;
        public Dictionary<string, string> Edge_Labeles_Map => edge_labeles_map_;
        public Dictionary<DEdge, DEdge> Edge_Map => edge_map_;

        public DLMGHomomorphism(
            DLMGraph source,
            DLMGraph target,
            Dictionary<Vertex, Vertex> v_map,
            Dictionary<string, string> l_map
            )
        {
            var edge_map = CheckEdgeHomomorphism(source, target, v_map, l_map);
            if (edge_map == null) return; 

            vertex_map_ = v_map;
            edge_map_ = edge_map;
            edge_labeles_map_ = l_map;
            source_ = source;
            target_ = target;
        }

        private static Dictionary<DEdge, DEdge>  CheckEdgeHomomorphism(
            DLMGraph graph1,
            DLMGraph graph2,
            Dictionary<Vertex, Vertex> v_map,
            Dictionary<string, string> l_map) 
        {
            if (!CheckEdgeLabelesHomomorphism(graph1, graph2, l_map) ||
               !CheckVertexHomomorphism(graph1, graph2, v_map))
                return null;

            Dictionary<DEdge, DEdge> edgeMaps = new Dictionary<DEdge, DEdge>();

            foreach (var e in graph1.Edges)
            {
                var mapEdge = new DEdge(v_map[e.Tail], v_map[e.Head], l_map[e.Label]);
                if (!graph2.Edges.Contains(mapEdge)) return null;
                edgeMaps.Add(e,mapEdge);
            }
            return edgeMaps;
        }

        private static bool CheckVertexHomomorphism(
            DLMGraph graph1,
            DLMGraph graph2,
            Dictionary<Vertex, Vertex> v_map)
        {
            foreach (var i in graph1.Vertices)
            {
                if (!v_map.ContainsKey(i) ||
                    !graph2.Vertices.Contains(v_map[i]))
                    return false;
            }
            return true;
        }


        private static bool CheckEdgeLabelesHomomorphism(
           DLMGraph graph1,
           DLMGraph graph2,
           Dictionary<string, string> e_map)
        {
            var labeles = graph1.Edges.Select(e => e.Label)
                                      .Distinct()
                                      .ToList();
            foreach(var label in labeles)
            {
                if (!e_map.ContainsKey(label) ||
                    !graph2.Edges.Any(e => e.Label == e_map[label]))
                    return false;
            }
            return true;
        }

        public override bool IsSurjective()
        {
            throw new NotImplementedException();
        }

        public override bool IsInjective()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Souce:");
            builder.AppendLine(source_.ToString());
            builder.AppendLine("Target:");
            builder.AppendLine(target_.ToString());

            builder.AppendLine("on vertices:");
            foreach(var v in vertex_map_)
            {
                builder.AppendLine($"{v.Key.Label} => {v.Value.Label}");
            }
            
            builder.AppendLine("on labeles:");
            foreach (var v in edge_labeles_map_)
            {
                builder.AppendLine($"{v.Key} => {v.Value}");
            }

            builder.AppendLine("on edges:");
            foreach (var v in edge_map_)
            {
                builder.AppendLine($"{v.Key} => {v.Value}");
            }

            return builder.ToString();
        }

        protected override Map<DLMGraph> Composite(Map<DLMGraph> map)
        {
            DLMGHomomorphism h1 = map as DLMGHomomorphism;
            if (h1 == null) return null;
            if (this.target_ != h1.source_) return null;

            var source = this.source_;
            var target = h1.target_;
            var vertex_map = new Dictionary<Vertex, Vertex>();
            var label_map = new Dictionary<string, string>();

            foreach(var v in this.vertex_map_)
                vertex_map.Add(v.Key, h1.vertex_map_[v.Value]);

            foreach (var l in this.edge_labeles_map_)
                label_map.Add(l.Key, h1.edge_labeles_map_[l.Value]);

            return new DLMGHomomorphism(source,target,vertex_map,label_map);
        }
    }
}

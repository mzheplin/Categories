using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Categories
{
    public class DLGHomomorphism : Homomorphism<DLGraph>
    {
        private readonly Dictionary<Vertex, Vertex> vertex_map_;
        private readonly Dictionary<string, string> edge_labeles_map_;
        private Dictionary<DEdge, DEdge> edge_map_;

        public Dictionary<Vertex, Vertex> Vertex_Map => vertex_map_;
        public Dictionary<string, string> Edge_Labeles_Map => edge_labeles_map_;
        public Dictionary<DEdge, DEdge> Edge_Map => edge_map_;

        public DLGHomomorphism(
            DLGraph source,
            DLGraph target,
            Dictionary<Vertex, Vertex> v_map,
            Dictionary<string, string> e_map
            )
        {
            var edge_map = CheckEdgeHomomorphism(source, target, v_map, e_map);
            if (edge_map == null)
                throw new ArgumentException("the arguments don't represent a directed labeled graph homomorphism");

            vertex_map_ = v_map;
            edge_map_ = edge_map;
            edge_labeles_map_ = e_map;
            source_ = source;
            target_ = target;
        }

        private Dictionary<DEdge, DEdge> CheckEdgeHomomorphism(
            DLGraph graph1,
            DLGraph graph2,
            Dictionary<Vertex, Vertex> v_map,
            Dictionary<string, string> e_map) 
        {
            if (!CheckEdgeLabelesHomomorphism(graph1, graph2, e_map) ||
               !CheckVertexHomomorphism(graph1, graph2, v_map))
                return null;

            Dictionary<DEdge, DEdge> edgeMaps = new Dictionary<DEdge, DEdge>();

            foreach (var e in graph1.Edges)
            {
                var mapEdge = new DEdge(v_map[e.Tail], v_map[e.Head], e_map[e.Label]);
                if (!graph2.Edges.Contains(mapEdge)) return null;
                edgeMaps.Add(e,mapEdge);
            }
            return edgeMaps;
        }

        private bool CheckVertexHomomorphism(
            DLGraph graph1,
            DLGraph graph2,
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


        private bool CheckEdgeLabelesHomomorphism(
           DLGraph graph1,
           DLGraph graph2,
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
                builder.AppendLine($"{v.Key.ToString()} => {v.Value.ToString()}");
            }

            return builder.ToString();
        }
    }
}

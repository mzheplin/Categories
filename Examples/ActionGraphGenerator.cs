using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Categories.Examples
{
    public static class ActionGraphGenerator
    {
        public static void GenerateActionGraph(
            int[] array,
            int pole_,
            Dictionary<int, List<Tuple<int, int>>> dict,
            ref DLMGraph res,
            ref Dictionary<string, string> vertex_map
            )
        {
            Queue<(int[], int)> stateQueue = new Queue<(int[], int)>();
            stateQueue.Enqueue((array, pole_));
            while (stateQueue.Count > 0)
            {
                var (arr, pole) = stateQueue.Dequeue();
                var strArr = ArrayToString(arr);

                var arrVertex = new Vertex(strArr);
                vertex_map.Add(strArr, (pole + 1).ToString());
                var transformations = dict[pole];

                foreach (var tr in transformations)
                {
                    var newArr = MoveContent(arr, tr.Item1, tr.Item2);
                    var strNewArr = ArrayToString(newArr);

                    if (res.Vertices.Any(vertex => vertex.Label == strNewArr))
                    {
                        var vertex = res.Vertices.First(v => v.Label == strNewArr);
                        DEdge edge = new DEdge(arrVertex, vertex, TupleToString(tr));
                        res.Add(edge);
                        continue;
                    }

                    var newVertex = new Vertex(strNewArr);
                    DEdge newEdge = new DEdge(arrVertex, newVertex, TupleToString(tr));
                    res.Add(newEdge);
                    stateQueue.Enqueue((newArr, tr.Item1));
                }
            }
        }


        static int[] MoveContent(int[] array, int index1, int index2)
        {
            var arr = (int[])array.Clone();
            int temp1 = array[index1];
            int temp2 = array[index2];

            arr[index1] = temp2;
            arr[index2] = temp1;
            return arr;
        }

        static string ArrayToString(int[] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
                sb.Append(array[i] == 0 ? "_" : array[i]);
            return sb.ToString();
        }

        static string TupleToString(Tuple<int, int> tuple)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tuple.Item1 + 1);
            sb.Append("->");
            sb.Append(tuple.Item2 + 1);
            return sb.ToString();
        }
    }
}


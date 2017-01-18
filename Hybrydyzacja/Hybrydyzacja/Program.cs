using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hybrydyzacja
{
    class Program
    {
        private static string[] _sampleWords =
        {
            "AAA", "AAC", "ACA", "CAC", "CAA",
            "ACG", "CGC", "GCA", "ACT", "CTT", "TTA", "TAA"
        };

        private static string[] _sampleWords2 =
        {
            "AAAG", "AATC", "ATCA", "CGAC", "ACAA",
            "ATCG", "CGCC", "GCAA", "ACTA", "CTCT", "TGTA", "TACA"
        };

        static void Main(string[] args)
        {
            var g = BuildGraph(_sampleWords.ToList(), 3);
            g.Print();
            if (g.EulerPathExists())
            {
                g.FindEulerPath();
            }
            else
            {
                Console.WriteLine("No solution");
            }
            Console.Read();
        }

        static Graph BuildGraph(List<string> words, int k)
        {
            var graph = new Graph { Vertices = new List<Vertex>() };
            foreach (var word in words)
            {
                var prefix = word.Substring(0, word.Length - 1);
                var suffix = word.Substring(1, word.Length - 1);
                if (!graph.ContainsVertex(prefix))
                {
                    graph.Vertices.Add(new Vertex() { k = k, Value = prefix, Edges = new List<Edge>()});
                }
                if (!graph.ContainsVertex(suffix))
                {
                    graph.Vertices.Add(new Vertex() { k = k, Value = suffix, Edges = new List<Edge>()});
                }
                var prefixVertex = graph.Vertices.Single(x => x.Value == prefix);
                var suffixVertex = graph.Vertices.Single(x => x.Value == suffix);
                prefixVertex.Edges.Insert(0, new Edge() { From = prefixVertex, To = suffixVertex, Suffix = suffix.Last() });
                prefixVertex.OutDegree++;
                suffixVertex.InDegree++;
                graph.Edges++;
            }
            return graph;
        }
    }

}

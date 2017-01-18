using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrydyzacja
{
    class Graph
    {
        public List<Vertex> Vertices { get; set; }
        public Vertex StartVertex { get; set; }
        public Vertex EndVertex { get; set; }
        public int Edges { get; set; }

        public bool ContainsVertex(string value)
        {
            return Vertices.Any(x => x.Value == value);
        }

        public bool EulerPathExists()
        {
            var begin = false;
            var end = false;
            foreach (var vertex in Vertices)
            {
                if (vertex.InDegree != vertex.OutDegree)
                {
                    if (vertex.InDegree - vertex.OutDegree == 1)
                    {
                        if (end) return false;
                        end = true;
                        EndVertex = vertex;
                    }
                    else if (vertex.OutDegree - vertex.InDegree == 1)
                    {
                        if (begin) return false;
                        begin = true;
                        StartVertex = vertex;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return begin && end;
        }

        public bool EulerCycleExists()
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.InDegree != vertex.OutDegree)
                {
                    return false;
                }
            }
            return true;
        }

        public void FindEulerPath()
        {
            var path = new List<Edge>();
            var currentVertex = StartVertex;
            var cycles = 1;
            while (currentVertex.Edges.Any())
            {
                var nextVertex = currentVertex.Edges[0].To;
                path.Add(currentVertex.Edges[0]);
                currentVertex.Edges.RemoveAt(0);
                Edges--;
                currentVertex = nextVertex;
            }
            if (Edges > 0)
            {
                AppendCycles(path);
            }

            foreach (var edge in path)
            {
                Console.WriteLine(string.Format("From {0} to {1}", edge.From.Value, edge.To.Value));
            }
        }

        private void AppendCycles(List<Edge> path)
        {
            for (var i = 0; i < path.Count; i++)
            {
                var cyclesLength = 0;
                while (path[i].From.Edges.Any())
                {
                    var cycle = FindCycle(path[i].From);
                    path.InsertRange(i, cycle);
                    cyclesLength += cycle.Count;
                }
                i += cyclesLength;
            }
        }

        private List<Edge> FindCycle(Vertex vertex)
        {
            var cycle = new List<Edge> {vertex.Edges[0]};
            var nextVertex = vertex.Edges[0].To; 
            vertex.Edges.RemoveAt(0);
            Edges--;
            while (nextVertex != vertex)
            {
                var nVertex = nextVertex.Edges[0].To;
                cycle.Add(nextVertex.Edges[0]);
                nextVertex.Edges.RemoveAt(0);
                Edges--;
                nextVertex = nVertex;
            }
            return cycle;
        }

        public void Print()
        {
            foreach (var vertex in Vertices)
            {
                Console.WriteLine(vertex.Value);
                foreach (var edge in vertex.Edges)
                {
                    Console.WriteLine(string.Format("    To: {0}, value: {1}", edge.To.Value, edge.Suffix));
                }
            }
        }
    }
}
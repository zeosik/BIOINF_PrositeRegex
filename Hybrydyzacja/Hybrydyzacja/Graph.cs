using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Hybrydyzacja
{
    class Graph
    {
        private char[] _alphabet = {'A', 'C', 'G', 'T'};

        public List<Vertex> Vertices { get; set; }
        public Vertex StartVertex { get; set; }
        public Vertex EndVertex { get; set; }
        public int Edges { get; set; }
        private bool _solutionExists;
        public int k { get; set; }

        public Graph() { }

        public Graph(List<string> words, int k)
        {
            BuildGraph(words, k);
        }

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
            if (EulerCycleExists())
            {
                StartVertex = Vertices[0];
            }
            else if (!EulerPathExists())
            {
                _solutionExists = false;
                Console.WriteLine("No solution");
                return;
            }
            _solutionExists = true;
            var path = new List<Edge>();
            var currentVertex = StartVertex;
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
            Console.Write(path[0].From.Value);
            foreach (var edge in path)
            {
                Console.Write(edge.Value);
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

        private void AddWord(string word)
        {
            var prefix = word.Substring(0, word.Length - 1);
            var suffix = word.Substring(1, word.Length - 1);
            if (!ContainsVertex(prefix))
            {
                Vertices.Add(new Vertex() {k=k, Edges = new List<Edge>(), Value = prefix});
            }
            if (!ContainsVertex(suffix))
            {
                Vertices.Add(new Vertex() { k = k, Edges = new List<Edge>(), Value = suffix });
            }
            var prefixVertex = Vertices.Single(x => x.Value == prefix);
            var suffixVertex = Vertices.Single(x => x.Value == suffix);
            prefixVertex.Edges.Insert(0, new Edge() { From = prefixVertex, To = suffixVertex, Value = suffix.Last() });
            prefixVertex.OutDegree++;
            suffixVertex.InDegree++;
            Edges++;
        }

        private void RemoveWord(string word)
        {
            var prefix = word.Substring(0, word.Length - 1);
            var suffix = word.Substring(1, word.Length - 1);
            var prefixVertex = Vertices.Single(x => x.Value == prefix);
            var suffixVertex = Vertices.Single(x => x.Value == suffix);
            var edgeValue = suffix[k - 2];
            var edge = prefixVertex.Edges.Single(x => x.Value == edgeValue);
            prefixVertex.OutDegree--;
            suffixVertex.InDegree--;
            Edges--;
            if (!prefixVertex.Edges.Remove(edge))
            {
                throw new Exception();
            }
            if (prefixVertex.InDegree == 0 && prefixVertex.OutDegree == 0)
            {
                if (!Vertices.Remove(prefixVertex))
                {
                    throw new Exception();
                }
            }
            if (suffixVertex.InDegree == 0 && suffixVertex.OutDegree == 0)
            {
                if (!Vertices.Remove(prefixVertex))
                {
                    throw new Exception();
                }
            }
        }

        private void BuildGraph(List<string> words, int k)
        {
            Vertices = new List<Vertex>();
            foreach (var word in words)
            {
                AddWord(word);
            }
        }

        private void HandleErrors(int errorsCount)
        {
            if (!_solutionExists)
            {
                
            }
        }

        private void HandlePositiveErrors(int errorsCount)
        {
            if (!_solutionExists)
            {

            }
        }

        private void HandleNegativeErrors(int errorsCount)
        {
            var words = GenerateLackingWords();
            foreach (var word in words)
            {
                AddWord(word);
                // check something
                RemoveWord(word);
            }
        }

        private string[] GenerateLackingWords()
        {
            for (var i = 0; i < k; i++)
            {
                for (var j = 0; j < k; j++)
                {
                    for (var l = 0; l < k; l++)
                    {
                        for (var m = 0; m < k; m++)
                        {
                            
                        }
                    }
                }
            }
            return null;
        }

        public void Print()
        {
            foreach (var vertex in Vertices)
            {
                Console.WriteLine(vertex.Value);
                foreach (var edge in vertex.Edges)
                {
                    Console.WriteLine(String.Format("    To: {0}, value: {1}", edge.To.Value, edge.Value));
                }
            }
        }

       
    }
}
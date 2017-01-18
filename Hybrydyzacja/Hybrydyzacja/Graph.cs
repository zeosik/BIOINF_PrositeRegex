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
        private List<string> _words;
        public List<Vertex> Vertices { get; set; }
        public Vertex StartVertex { get; set; }
        public Vertex EndVertex { get; set; }
        public int Edges { get; set; }
        private bool _solutionExists;
        public int k { get; set; }

        public Graph() { }

        public Graph(List<string> words, int k)
        {
            _words = words;
            this.k = k;
            BuildGraph(words);
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
            var g = new Graph(_words, k);
            if (g.EulerCycleExists())
            {
                g.StartVertex = Vertices[0];
            }
            else if (!g.EulerPathExists())
            {
                _solutionExists = false;
                Console.WriteLine("No solution");
                return;
            }
            _solutionExists = true;
            var path = new List<Edge>();
            var currentVertex = g.StartVertex;
            while (currentVertex.Edges.Any())
            {
                var nextVertex = currentVertex.Edges[0].To;
                path.Add(currentVertex.Edges[0]);
                currentVertex.Edges.RemoveAt(0);
                g.Edges--;
                currentVertex = nextVertex;
            }
            if (g.Edges > 0)
            {
                g.AppendCycles(path);
            }
            Console.Write("Rozwiązanie: ");
            Console.Write(path[0].From.Value);
            foreach (var edge in path)
            {
                Console.Write(edge.Value);
            }
            Console.WriteLine();
            HandleNegativeErrors(1);
            HandlePositiveErrors(1);
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
            if (prefixVertex != suffixVertex)
            {
                if (prefixVertex.InDegree == 0 && prefixVertex.OutDegree == 0)
                {
                    if (!Vertices.Remove(prefixVertex))
                    {
                        throw new Exception();
                    }
                }
                if (suffixVertex.InDegree == 0 && suffixVertex.OutDegree == 0)
                {
                    if (!Vertices.Remove(suffixVertex))
                    {
                        throw new Exception();
                    }
                }
            }
            else
            {
                if (prefixVertex.InDegree == 0 && prefixVertex.OutDegree == 0)
                {
                    if (!Vertices.Remove(prefixVertex))
                    {
                        throw new Exception();
                    }
                }
            }
           
        }

        private void BuildGraph(List<string> words)
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
                foreach (var word in _words)
                {
                    RemoveWord(word);
                    var cycleOrPath = EulerCycleExists() || EulerPathExists();
                    if (cycleOrPath)
                    {
                        Console.WriteLine("Jeśli słowo '{0}' nie było obecne w oryginalnej sekwencji (błąd pozytywny), to istnieje rozwiązanie:", word);
                        FindEulerPath();
                    }
                    AddWord(word);
                }
            }
            else
            {
                foreach (var word in _words)
                {
                    RemoveWord(word);
                    var cycleOrPath = EulerCycleExists() || EulerPathExists();
                    if (!cycleOrPath)
                    {
                        Console.WriteLine(
                            "Jeśli słowo '{0}' nie było obecne w oryginalnej sekwencji (błąd pozytywny), to nie istnieje rozwiązanie.", word);
                    }
                    AddWord(word);
                }
            }
        }

        private void HandleNegativeErrors(int errorsCount)
        {
            var words = Generator.AllCombinations(k);
            if (_solutionExists)
            {
                foreach (var word in words)
                {
                    if (_words.Contains(word)) continue;
                    AddWord(word);
                    var cycleOrPath = EulerCycleExists() || EulerPathExists();
                    if (!cycleOrPath)
                    {
                        Console.WriteLine("Jeśli słowo '{0}' było obecne w oryginalnej sekwencji (błąd negatywny), to nie istnieje rozwiązanie.", word);
                    }
                    RemoveWord(word);
                }
            }
            else
            {
                foreach (var word in words)
                {
                    AddWord(word);
                    var cycleOrPath = EulerCycleExists() || EulerPathExists();
                    if (cycleOrPath)
                    {
                        Console.WriteLine("Jeśli słowo '{0}' było obecne w oryginalnej sekwencji (błąd negatywny), to istnieje rozwiązanie:", word);
                        FindEulerPath();
                    }
                    RemoveWord(word);
                }
            }
           
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
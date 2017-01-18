using System.Collections.Generic;

namespace Hybrydyzacja
{
    class Vertex
    {
        public string Value { get; set; }
        public int k { get; set; }
        public int InDegree { get; set; }
        public int OutDegree { get; set; }
        public List<Edge> Edges { get; set; }
    }
}
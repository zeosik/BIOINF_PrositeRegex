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
            var sample = _sampleWords.ToList();// Generator.CorruptedExample();// Generator.GenerateSomeData(4, 12);
            var g = new Graph(sample, 3);
            g.Print();
            g.FindEulerPath(true);
            Console.Read();
        }
    }

}

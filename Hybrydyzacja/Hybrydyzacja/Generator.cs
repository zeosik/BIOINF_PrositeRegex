using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hybrydyzacja
{
    class Generator
    {
        public static List<string> GenerateSomeData(int k, int lenght)
        {
            string sequence = "";
            char[] letters = {'A', 'C', 'G', 'T'};
            var random = new Random();

            for (int i = 0; i < lenght; i++)
                sequence += letters[random.Next(0, 3)];

            Console.WriteLine("Wylosowalem cos takiego: " + sequence);

            var output = new List<string>();
            for (int i = 0; i <= lenght - k; i++)
            {
                output.Add(sequence.Substring(i, k));
            }

            return output;
        }
    }

}

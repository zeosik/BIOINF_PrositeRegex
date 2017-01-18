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
            char[] letters = { 'A', 'C', 'G', 'T' };
            var random = new Random();
            bool sequenceNotvalid = true;
            List<string> output = new List<string>();
            string sequence = "";

            while (sequenceNotvalid)
            {
                sequenceNotvalid = false;
                sequence = "";
                output = new List<string>();

                for (int i = 0; i < lenght; i++)
                    sequence += letters[random.Next(0, 3)];
                
                for (int i = 0; i <= lenght - k; i++)
                {
                    foreach (var str in output)
                        if (sequence.Substring(i, k) == str)
                            sequenceNotvalid = true;
                    output.Add(sequence.Substring(i, k));
                }
            }

            Console.WriteLine("Wylosowalem cos takiego: " + sequence);
            return output;
        }

        public static List<string> AllCombinations(int k)
        {
            string[] letters = { "A", "C", "G", "T" };
            if (k == 1)
                return letters.ToList();
            
            List<string> output = new List<string>();
            foreach (var str in AllCombinations(k - 1))
                for (int i=0; i<4; i++)
                    output.Add(str + letters[i]);

            return output;
        }
    }

}

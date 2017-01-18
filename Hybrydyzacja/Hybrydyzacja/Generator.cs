using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrydyzacja
{
    class Generator
    {
        public static List<string> CorruptedExample()
        {
            string[] exaple = {"TCA", "CAT", "ATG", "TGG", "GGT", "GTA", "CAG"};
            return exaple.ToList();
        }

        public static List<string> GenerateSomeData(int k, int length)
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

                for (int i = 0; i < length; i++)
                    sequence += letters[random.Next(0, 4)];

                for (int i = 0; i <= length - k; i++)
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
                for (int i = 0; i < 4; i++)
                    output.Add(str + letters[i]);

            return output;
        }
    }
}

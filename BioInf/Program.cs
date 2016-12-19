using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BioInf.AminoAcids;

namespace BioInf
{
    class Program
    {
        static void Main(string[] args)
        {
            var regex = "x";
            //var regex = "[AB](1,2)-[AB](1,2)";
            var str = "AB|ABB|AAB|AABB";
            var r = PrositeRegex.find(regex, str);
            Console.WriteLine(r);
            Console.ReadLine();
        }
    }

    public class PrositeRegex
    {
        private string regexRaw;
        private IList<RepeatedAcid> elements;
        private AcidCombination acidCombinations;

        public PrositeRegex(string regexRaw)
        {
            this.regexRaw = regexRaw;
            elements = regexRaw.Split('-').Select(RepeatedAcid.createRepeatedAminoAcid).ToList();
            acidCombinations = allAcidCombinations(elements);
        }

        public static PrositeRegexResults find(string regex, string str)
        {
            return new PrositeRegex(regex).find(str);
        }

        private PrositeRegexResults find(string str)
        {
            return new PrositeRegexResults(str, matches(str).ToList());
        }

        private AcidCombination allAcidCombinations(IList<RepeatedAcid> elements)
        {
            var root = elements[0].combinations();
            return elements.Skip(1).Select(element => element.combinations()).Aggregate(root, (current, comb) => current.join(comb));
        }

        public IEnumerable<SingleMatch> matches(string str)
        {
            for (int str_i = 0; str_i < str.Length; str_i++)
            {
                string toTest = str.Substring(str_i);

                foreach (var seq in acidCombinations.list)
                {
                    if (seq.match(toTest))
                    {
                        var length = seq.list.Count;
                        yield return new SingleMatch() { match = toTest.Substring(0, length), matchFrom = str_i, matchTo = str_i + length };
                    }
                }
            }
        }
    }
}

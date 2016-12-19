using System.Linq;
using BioInf.AminoAcids;

namespace BioInf
{
    public class RepeatedAcid
    {
        public IAminoAcid aminoAcid;
        public int from;
        public int to;

        //x(1,3) -> x, xx, xxx
        public AcidCombination combinations()
        {
            return new AcidCombination(Enumerable.Range(from, to - from + 1).Select(i => new AcidSequence(Enumerable.Repeat(aminoAcid, i))));
        }

        public static RepeatedAcid createRepeatedAminoAcid(string str)
        {
            if (str.StartsWith("["))
            {
                var i = str.IndexOf("]");
                return new RepeatedAcid(new AminoAcidFromList(str.Substring(1, i - 1).ToCharArray()), str.Substring(i + 1));
            }
            if (str.StartsWith("{"))
            {
                var i = str.IndexOf("}");
                return new RepeatedAcid(new AminoAcidNotFromList(str.Substring(1, i - 1).ToCharArray()), str.Substring(i + 1));
            }
            if (str.StartsWith("x"))
            {
                return new RepeatedAcid(new AnyAminoAcid(), str.Substring(1));
            }
            return new RepeatedAcid(new AminoAcid(str[0]), str.Substring(1));
        }

        public RepeatedAcid(IAminoAcid aminoAcid, string times)
        {
            this.aminoAcid = aminoAcid;

            if (times.StartsWith("(") && times.EndsWith(")"))
            {
                string withoutBrackets = times.Substring(1, times.Length - 2);
                if (withoutBrackets.Contains(",")) //"(1,2)
                {
                    var numbers = withoutBrackets.Split(',');
                    from = int.Parse(numbers[0]);
                    to = int.Parse(numbers[1]);
                }
                else // "(1)"
                {
                    to = from = int.Parse(times);
                }
            }
            else //""
            {
                from = to = 1;
            }
        }
    }
}
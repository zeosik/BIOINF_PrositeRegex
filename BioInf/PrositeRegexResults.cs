using System.Collections.Generic;
using System.Text;

namespace BioInf
{
    public class PrositeRegexResults
    {
        public string original;
        public List<SingleMatch> results;

        public PrositeRegexResults(string str, List<SingleMatch> matches)
        {
            original = str;
            results = matches;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(original);
            foreach (var result in results)
            {
                builder.AppendLine(result.ToString());
            }
            return builder.ToString();
        }
    }
}
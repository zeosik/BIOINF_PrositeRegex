using System.Collections.Generic;
using System.Linq;

namespace BioInf.AminoAcids
{
    public class AminoAcidFromList : IAminoAcid
    {
        protected List<char> list;

        public AminoAcidFromList(IEnumerable<char> list)
        {
            this.list = list.ToList();
        }

        public virtual bool accept(char c)
        {
            return list.Contains(c);
        }

        public override string ToString()
        {
            return "[" + string.Join("", list) + "]";
        }
    }
}
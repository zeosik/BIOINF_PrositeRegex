using System.Collections.Generic;
using System.Linq;
using BioInf.AminoAcids;

namespace BioInf
{
    public class AcidSequence
    {
        public IList<IAminoAcid> list;

        public AcidSequence(IEnumerable<IAminoAcid> list)
        {
            this.list = list.ToList();
        }

        public bool match(string str)
        {
            if (str.Length < list.Count)
            {
                return false;
            }
            return !list.Where((t, i) => !t.accept(str[i])).Any();
        }

        public AcidSequence add(AcidSequence other)
        {
            return new AcidSequence(list.Concat(other.list));
        }

        public override string ToString()
        {
            return string.Join("-", list.Select(x => x.ToString()));
        }
    }
}
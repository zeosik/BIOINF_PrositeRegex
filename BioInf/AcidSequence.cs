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
            for (int i = 0; i < list.Count; i++)
            {
                if (!(i < str.Length && list[i].accept(str[i])))
                {
                    return false;
                }
            }
            return true;
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
using System.Collections.Generic;
using System.Linq;

namespace BioInf
{
    public class AcidCombination
    {
        public IEnumerable<AcidSequence> list;

        public AcidCombination(IEnumerable<AcidSequence> list)
        {
            this.list = list;
        }

        //crossjoin
        //{A,B} join {C,D} -> {AC, AD, BD, BD}
        public AcidCombination join(AcidCombination other)
        {
            return new AcidCombination(list.SelectMany(x => other.list, (my, o) => my.add(o)));
        }
    }
}
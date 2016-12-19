namespace BioInf.AminoAcids
{
    public class AminoAcidNotFromList : AminoAcidFromList
    {
        public AminoAcidNotFromList(char[] list) : base(list)
        {
        }
        public override bool accept(char c)
        {
            return !base.accept(c);
        }

        public override string ToString()
        {
            return "{" + string.Join("", list) + "}";
        }
    }
}
namespace BioInf.AminoAcids
{
    public class AnyAminoAcid : IAminoAcid
    {
        public bool accept(char c)
        {
            return true;
        }

        public override string ToString()
        {
            return "x";
        }
    }
}
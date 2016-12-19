namespace BioInf.AminoAcids
{
    public class AminoAcid : IAminoAcid
    {
        private char c;

        public AminoAcid(char c)
        {
            this.c = c;
        }

        public bool accept(char c)
        {
            return this.c == c;
        }

        public override string ToString()
        {
            return c.ToString();
        }
    }
}
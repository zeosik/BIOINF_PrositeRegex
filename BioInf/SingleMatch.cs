namespace BioInf
{
    public class SingleMatch
    {
        public string match;
        public int matchFrom;
        public int matchTo;

        public override string ToString()
        {
            return match.PadLeft(matchTo, ' ');
        }
    }
}
namespace Uno.Runtime.Domain
{
    public class DrawFourWildCard : WildCard
    {
        public override string ToString()
        {
            return $"[{base.ToString()}, DrawFour]";
        }
    }
}
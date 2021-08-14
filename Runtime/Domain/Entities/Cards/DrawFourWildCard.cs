namespace Uno.Runtime.Domain.Entities.Cards
{
    public class DrawFourWildCard : WildCard
    {
        public override string ToString()
        {
            return $"[{base.ToString()}, DrawFour]";
        }
    }
}
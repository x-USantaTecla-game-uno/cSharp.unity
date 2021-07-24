namespace Uno.Runtime.Domain
{
    public class WildCard : Card
    {
        public WildCard(Color color) : base(color) { }
        public WildCard() : base(Color.Any) { }
    }
}
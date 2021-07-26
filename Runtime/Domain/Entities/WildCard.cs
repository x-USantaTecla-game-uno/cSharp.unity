namespace Uno.Runtime.Domain
{
    public class WildCard : Card
    {
        public WildCard(Color color) : base(color) { }
        public WildCard() : base(Color.Any) { }

        public override string ToString()
        {
            return $"[{base.ToString()}, WildCard]";
        }
    }
}
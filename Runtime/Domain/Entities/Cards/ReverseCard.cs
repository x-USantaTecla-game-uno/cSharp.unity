namespace Uno.Runtime.Domain.Entities.Cards
{
    public class ReverseCard : Card
    {
        public ReverseCard(Color color) : base(color) { }

        public override string ToString()
        {
            return $"[{base.ToString()}, Reverse]";
        }
    }
}
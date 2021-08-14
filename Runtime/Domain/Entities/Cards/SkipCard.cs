namespace Uno.Runtime.Domain.Entities.Cards
{
    public class SkipCard : Card
    {
        public SkipCard(Color color) : base(color) { }

        public override string ToString()
        {
            return $"[{base.ToString()}, Skip]";
        }
    }
}
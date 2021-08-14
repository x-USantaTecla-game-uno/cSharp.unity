namespace Uno.Runtime.Domain.Entities.Cards
{
    public class DrawTwoCard : Card
    {
        public DrawTwoCard(Color color) : base(color) { }

        public override string ToString()
        {
            return $"{base.ToString()}, DrawTwo";
        }
    }
}
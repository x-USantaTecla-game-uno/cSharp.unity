namespace Uno.Runtime.Domain
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
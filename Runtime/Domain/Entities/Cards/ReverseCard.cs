namespace Uno.Runtime.Domain
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
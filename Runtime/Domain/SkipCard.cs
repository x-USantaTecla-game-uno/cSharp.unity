namespace Uno.Runtime.Domain
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
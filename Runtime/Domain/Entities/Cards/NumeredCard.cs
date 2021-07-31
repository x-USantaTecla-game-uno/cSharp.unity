namespace Uno.Runtime.Domain
{
    public class NumeredCard : Card
    {
        readonly int number;

        public NumeredCard(Color color, int number) : base(color)
        {
            this.number = number;
        }

        protected override bool MatchesHook(Card o)
        {
            return o is NumeredCard other &&
                   number == other.number;
        }
        
        #region Formatting members
        public override string ToString()
        {
            return $"[{base.ToString()}, {nameof(number)}: {number}]";
        }
        
        #endregion
    }
}
namespace Uno.Runtime.Domain
{
    public abstract class Card
    {
        readonly Color color;

        #region Constructors
        internal Card() : this(Color.Any) { }
        protected Card(Color color)
        {
            this.color = color;
        }
        #endregion

        public bool Matches(Card other)
        {
            return color.Matches(other.color) &&
                   MatchesHook(other);
        }

        protected virtual bool MatchesHook(Card other) => true;

        #region Formatting members
        public override string ToString()
        {
            return $"[{nameof(color)}: {color}]";
        }
        #endregion
    }
}
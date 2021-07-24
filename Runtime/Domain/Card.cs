namespace Uno.Runtime.Domain
{
    public abstract class Card
    {
        readonly Color color;

        protected Card(Color color)
        {
            this.color = color;
        }

        public bool Matches(Card other)
        {
            return color.Matches(other.color) &&
                   MatchesHook(other);
        }

        protected virtual bool MatchesHook(Card other) => true;
    }
}
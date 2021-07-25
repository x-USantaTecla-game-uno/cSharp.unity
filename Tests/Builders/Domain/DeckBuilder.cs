using Uno.Runtime.Domain;

namespace Uno.Tests.Builders.Domain
{
    public class DeckBuilder : Builder<Deck>
    {
        Card[] cards = new Card[0];
        
        #region Fluent API
        public DeckBuilder WithCards(params Card[] cards)
        {
            this.cards = cards;
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        DeckBuilder() { }

        internal static DeckBuilder New() => new DeckBuilder();
        #endregion

        #region Builder implementation
        public override Deck Build() => new Deck(cards);
        #endregion
    }
}
using System.Collections.Generic;
using System.Linq;
using Uno.Runtime.Domain;
using Uno.Runtime.Domain.Entities;
using Uno.Runtime.Domain.Entities.Cards;

namespace Uno.Tests.Builders.Domain
{
    public class PlayerBuilder : Builder<Player>
    {
        Card[] cards;
        
        #region Fluent API
        public PlayerBuilder WithHand(IEnumerable<Card> cards) => WithHand(cards.ToArray());

        public PlayerBuilder WithHand(params Card[] cards)
        {
            this.cards = cards;
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        PlayerBuilder() { }

        internal static PlayerBuilder New() => new PlayerBuilder();
        #endregion

        #region Builder implementation
        public override Player Build() => cards is null ? new Player() : new Player(cards);
        #endregion
    }
}
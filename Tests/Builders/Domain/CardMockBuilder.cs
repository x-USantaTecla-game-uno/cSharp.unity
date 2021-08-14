using System.Collections.Generic;
using Uno.Runtime.Domain;
using Uno.Runtime.Domain.Entities.Cards;

namespace Uno.Tests.Builders.Domain
{
    public class CardMockBuilder : MockBuilder<Card>
    {
        #region ObjectMother/FactoryMethods
        CardMockBuilder() { }

        internal static CardMockBuilder New() => new CardMockBuilder();

        public ICollection<Card> BunchOf(int cardsAmount)
        {
            var bunchOfCards = new List<Card>();

            for(var i = 0; i < cardsAmount; i++)
                bunchOfCards.Add(Build());
            
            return bunchOfCards;
        }
        #endregion
    }
}
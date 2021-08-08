using System.Collections.Generic;
using Uno.Runtime.Services;

namespace Uno.Runtime.Domain
{
    public class UnoDeckFactory : IFactory<Deck>
    {
        public Deck Create()
        {
            return new Deck(CreateUnoCards());
        }

        #region Support methods
        static IEnumerable<Card> CreateUnoCards()
        {
            var unoCards = new List<Card>();

            unoCards.AddRange(CreateNumeredCardsWithoutZero());
            unoCards.AddRange(CreateNumeredCardsWithZero());
            unoCards.AddRange(CreateActionCards());
            unoCards.AddRange(CreateWildCards());
            
            return unoCards;
        }
        
        static IEnumerable<Card> CreateNumeredCardsWithoutZero()
        {
            foreach(var color in ColorExtensions.PlayableColors)
                for(var number = 1; number < 10; number++)
                    yield return new NumeredCard(color, number);
        }

        static IEnumerable<Card> CreateNumeredCardsWithZero()
        {
            foreach(var color in ColorExtensions.PlayableColors)
                for(var number = 0; number < 10; number++)
                    yield return new NumeredCard(color, number);
        }

        static IEnumerable<Card> CreateActionCards()
        {
            foreach(var color in ColorExtensions.PlayableColors)
            {
                yield return new ReverseCard(color);
                yield return new ReverseCard(color);
                
                yield return new SkipCard(color);
                yield return new SkipCard(color);
                
                yield return new DrawTwoCard(color);
                yield return new DrawTwoCard(color);
            }
        }

        static IEnumerable<Card> CreateWildCards()
        {
            foreach(var playableColor in ColorExtensions.PlayableColors)
            {
                yield return new WildCard();
                yield return new DrawFourWildCard();
            }
        }
        #endregion
    }
}
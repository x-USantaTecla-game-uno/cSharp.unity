using System;
using System.Collections.Generic;
using System.Linq;
using Uno.Runtime.Domain;
using Uno.Runtime.Domain.Entities;
using Uno.Runtime.Domain.Entities.Cards;

namespace Uno.Tests.Builders.Domain
{
    public class DeckBuilder : Builder<Deck>
    {
        Card[] cards = Array.Empty<Card>();
        
        #region Fluent API
        public DeckBuilder WithCards(params Card[] cards)
        {
            this.cards = cards;
            return this;
        }

        public DeckBuilder WithCards(IEnumerable<Card> cards)
        {
            this.cards = cards.ToArray();
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
    
    #region Testing superAPI
    public static class DeckTestingExtensions
    {
        public static IEnumerable<Card> AllCards(this Deck deck)
        {
            while(deck.TotalCards > 0)
                yield return deck.Draw();
        }

        public static IEnumerable<Card> CardsWithNumber(this Deck deck, int number)
        {
            return deck.AllCards().Where(card => card is NumeredCard &&
                                                 card.Matches(Build.NumeredCard().WithNumber(number)));
        }
        
        public static IEnumerable<Card> ActionCards(this Deck deck)
        {
            return deck.AllCards().Where(card => card is ReverseCard ||
                                                 card is SkipCard ||
                                                 card is DrawTwoCard);
        }
        
        public static IEnumerable<Card> WildCards(this Deck deck)
        {
            return deck.AllCards().Where(card => card is WildCard);
        }
    }
    
    public static class ColorTestingExtensions
    {
        public static int PerColor(this int howMany) => howMany * ColorExtensions.PlayableColors.Count();
    }
    #endregion
}
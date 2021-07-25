using System;
using System.Collections.Generic;
using System.Linq;
using Kalendra.Commons.Runtime.Architecture.Services;
using Kalendra.Commons.Runtime.Infraestructure.Services;

namespace Uno.Runtime.Domain
{
    public class Deck
    {
        Queue<Card> drawPile;
        readonly Stack<Card> discardPile;
        
        public Card LastDiscard => discardPile.Peek();
     
        //TODO: invert this dependency.
        readonly IRandomService random = new SystemRandomService();
        
        public Deck(IEnumerable<Card> cards)
        {
            drawPile = new Queue<Card>(ShuffleCards(cards));
            discardPile = new Stack<Card>();
        }

        public Card Draw()
        {
            if(!drawPile.Any())
                ReShuffle();
            
            return drawPile.Dequeue();
        }

        public void Play(Card playedCard)
        {
            discardPile.Push(playedCard);
        }

        static int test = 1;
        #region Support methods
        IEnumerable<Card> ShuffleCards(IEnumerable<Card> cards)
        {
            var shuffledCards = new List<Card>();
            var remainingCards = cards.ToList();

            while(remainingCards.Any())
            {
                var randomCard = random.GetRandom(remainingCards);
                remainingCards.Remove(randomCard);
                
                shuffledCards.Add(randomCard);
            }
            
            return shuffledCards;
        }
        
        void ReShuffle()
        {
            if(!discardPile.Any() && !drawPile.Any())
                throw new InvalidOperationException();
            
            var lastDiscard = discardPile.Pop();
            drawPile = new Queue<Card>(discardPile);
            
            discardPile.Clear();
            discardPile.Push(lastDiscard);
        }
        #endregion
    }
}
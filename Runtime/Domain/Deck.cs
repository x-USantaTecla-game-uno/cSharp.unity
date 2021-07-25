using System;
using System.Collections.Generic;
using System.Linq;

namespace Uno.Runtime.Domain
{
    public class Deck
    {
        Queue<Card> drawPile;
        readonly Stack<Card> discardPile;

        public Card LastDiscard => discardPile.Peek();
        
        public Deck(IEnumerable<Card> cards)
        {
            drawPile = new Queue<Card>(cards);
            discardPile = new Stack<Card>();
        }

        public Card Draw()
        {
            if(!drawPile.Any())
                ReShuffle();
            
            return drawPile.Dequeue(); //TODO: randomize.
        }

        public void Play(Card playedCard)
        {
            discardPile.Push(playedCard);
        }
        
        #region Support methods
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
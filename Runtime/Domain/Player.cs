using System;
using System.Collections.Generic;
using System.Linq;

namespace Uno.Runtime.Domain
{
    public class Player
    {
        readonly List<Card> hand;

        public int RemainingCards => hand.Count;

        public Player()
        {
            hand = new List<Card>();
        }

        public void AddCard(Card card)
        {
            hand.Add(card);
        }

        //TODO: fails because Card misses equality override.
        public void RemoveCard(Card card)
        {
            if(!hand.Contains(card))
                throw new ArgumentOutOfRangeException();
            
            hand.Remove(card);
        }
    }
}
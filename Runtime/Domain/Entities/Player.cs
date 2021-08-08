using System;
using System.Collections.Generic;

namespace Uno.Runtime.Domain
{
    public class Player
    {
        readonly ICollection<Card> hand;

        public int RemainingCards => hand.Count;

        #region Constructors
        public Player() : this(new List<Card>()) { }
        
        public Player(ICollection<Card> startingHand)
        {
            hand = startingHand;
        }
        #endregion

        public void AddCard(Card card)
        {
            hand.Add(card);
        }

        //TODO: fails because Card misses equality override.
        public void RemoveCard(Card card)
        {
            if(!hand.Contains(card))
                throw new InvalidOperationException();
            
            hand.Remove(card);
        }
    }
}
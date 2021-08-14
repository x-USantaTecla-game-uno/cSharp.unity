using System;
using System.Collections.Generic;
using System.Linq;
using Uno.Runtime.Domain.Entities.Cards;

namespace Uno.Runtime.Domain.Entities
{
    public class Player
    {
        readonly List<Card> hand;

        public int RemainingCards => hand.Count;

        #region Constructors
        public Player() : this(new List<Card>()) { }
        
        public Player(IEnumerable<Card> startingHand)
        {
            hand = startingHand.ToList();
        }
        #endregion

        public void AddCard(Card card)
        {
            hand.Add(card);
        }

        public void RemoveCard(Card card)
        {
            if(!hand.Contains(card))
                throw new InvalidOperationException();
            
            hand.Remove(card);
        }
    }
}
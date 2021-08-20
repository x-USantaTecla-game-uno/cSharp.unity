using System;
using System.Collections.Generic;
using System.Linq;

namespace Uno.Runtime.Domain
{
    public class Uno
    {
        const int START_HAND_CARDS = 7;
        
        Turn turn;
        Deck deck;

        #region Constructors
        public Uno(int numberOfPlayers, int humanPlayers)
        {
            deck = new UnoDeckFactory().Create();
            turn = new Turn(CreatePlayers(numberOfPlayers, humanPlayers));
        }
        #endregion
        
        #region Support methods

        private IEnumerable<Player> CreatePlayers(int numberOfPlayers, int humanPlayers)
        {
            if (deck == null)
            {
                throw new InvalidOperationException();
            }
            
            // TODO Add human and machine players and create them.
            for (int i = 0; i < numberOfPlayers; i++)
            {
                yield return new Player(GetPlayerStartingHand().ToList());
            }
        }

        private IEnumerable<Card> GetPlayerStartingHand()
        {
            if (deck == null)
            {
                throw new InvalidOperationException();
            }

            for (int i = 0; i < START_HAND_CARDS; i++)
            {
                yield return deck.Draw();
            }
        }
        
        #endregion
    }
}

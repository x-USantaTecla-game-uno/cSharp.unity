using Uno.Runtime.Application.Contracts;
using Uno.Runtime.Domain.Entities;
using Uno.Runtime.Domain.Entities.Cards;

namespace Uno.Runtime.Application
{
    public class PlayCardInteractor : PlayCardInputPort
    {
        readonly PlayCardOutputPort successHandler;
        readonly PlayCardErrorOutputPort errorHandler;

        public PlayCardInteractor(PlayCardOutputPort successHandler, PlayCardErrorOutputPort errorHandler)
        {
            this.successHandler = successHandler;
            this.errorHandler = errorHandler;
        }

        public void RequestPlayCard(CardPlayRequest request)
        {
            var currentPlayer = GetCurrentPlayer();
            var requestedCard = GetCardFromPlayer(currentPlayer, request);

            if(IsPlayableCard(requestedCard))
                PlayCard(requestedCard);
            else
                NotifyPlayCardError(requestedCard);
        }

        void NotifyPlayCardError(Card notPlayableCard)
        {
            errorHandler?.ResponsePlayCardError();
        }

        void PlayCard(Card cardToPlay)
        {
            //TODO: proceso de jugado de la carta.
            throw new System.NotImplementedException();
            
            successHandler?.ResponseCardPlayed();
        }

        bool IsPlayableCard(Card requestedCard)
        {
            throw new System.NotImplementedException();
        }

        Card GetCardFromPlayer(Player currentPlayer, int cardIndex)
        {
            throw new System.NotImplementedException();
        }

        Player GetCurrentPlayer()
        {
            throw new System.NotImplementedException();
        }
    }
}
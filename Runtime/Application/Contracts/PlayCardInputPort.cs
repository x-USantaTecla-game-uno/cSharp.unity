namespace Uno.Runtime.Application.Contracts
{
    public interface PlayCardInputPort
    {
        void RequestPlayCard(CardPlayRequest request);
    }
}
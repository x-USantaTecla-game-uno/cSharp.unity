namespace Uno.Runtime.Application.Contracts
{
    public struct CardPlayRequest
    {
        public int CardIndex { get; set; }

        public static implicit operator int(CardPlayRequest r) => r.CardIndex;
    }
}
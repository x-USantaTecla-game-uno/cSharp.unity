namespace Uno.Runtime.Application
{
    public readonly struct CreateUnoRequest
    {
        public int NumberOfPlayers { get; }
        public int NumberOfHuman { get; }

        #region Constructors
        public CreateUnoRequest(int numberOfPlayers, int numberOfHumans)
        {
            NumberOfPlayers = numberOfPlayers;
            NumberOfHuman = numberOfHumans;
        }
        #endregion
    }
}

namespace Uno.Runtime.Application
{
    public class UnoRequest
    {
        private int numberOfHumans;
        private int numberOfPlayers;

        #region Properties
        public int NumberOfPlayers
        {
            get => this.numberOfPlayers;
        }
        
        public int NumberOfHuman
        {
            get => this.numberOfHumans;
        }
        #endregion

        #region Constructors
        public UnoRequest(int numberOfPlayers, int numberOfHumans)
        {
            this.numberOfPlayers = numberOfPlayers;
            this.numberOfHumans = numberOfHumans;
        }
        #endregion
    }
}

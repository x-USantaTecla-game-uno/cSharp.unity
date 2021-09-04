using Uno.Runtime.Domain;

namespace Uno.Runtime.Application
{
    public class BeginInteractor: BeginInputPort
    {
        private readonly UnoPlayersNumberPolicy unoPlayerNumberPolicy;
        private readonly BeginOutputPort outputPort;

        public Domain.Uno Uno { get; private set; }
        
        #region Constructors
        public BeginInteractor(BeginOutputPort outputPort)
        {
            unoPlayerNumberPolicy = new UnoPlayersNumberPolicy();
            this.outputPort = outputPort;
        }
        #endregion

        public void CreateUno(CreateUnoRequest request)
        {
            Uno = new Domain.Uno(request.NumberOfPlayers, request.NumberOfHuman);
        }

        public bool IsValidNumberOfPlayers(int numberOfPlayers)
        {
            return unoPlayerNumberPolicy.IsValidNumberOfPlayers(numberOfPlayers);
        }

        public bool IsValidNumberOfHumans(int numberOfPlayers, int numberOfHumans)
        {
            return unoPlayerNumberPolicy.IsValidNumberOfHumans(numberOfPlayers, numberOfHumans);
        }

        public void NoticeWantNumberOfPlayers()
        {
            outputPort.NoticeWantNumberOfPlayers();
        }

        public void NoticeWantNumberOfHumans()
        {
            outputPort.NoticeWantNumberOfHumans();
        }
    }
}

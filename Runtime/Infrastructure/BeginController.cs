using System.Threading.Tasks;
using Uno.Runtime.Application;

namespace Uno.Runtime.Infrastructure
{
    public class BeginController
    {
        readonly BeginInputPort beginInputPort;
        readonly Console console;

        int numberOfPlayers;
        int numberOfHumans;

        #region Constructors
        public BeginController(BeginInputPort beginInputPort, Console console)
        {
            this.beginInputPort = beginInputPort;
            this.console = console;
        }
        #endregion

        public async Task Begin()
        {
            await AskForNumberOfPlayers();
            await AskForNumberOfHumans();

            var request = new CreateUnoRequest(numberOfPlayers, numberOfHumans);

            beginInputPort.CreateUno(request);
        }

        async Task AskForNumberOfPlayers()
        {
            var isValidInput = false;

            while(!isValidInput)
            {
                numberOfPlayers = await console.ReadInteger();
                isValidInput = beginInputPort.IsValidNumberOfPlayers(numberOfPlayers);
            }
        }

        async Task AskForNumberOfHumans()
        {
            var isValidInput = false;

            while (!isValidInput)
            {
                numberOfHumans = await this.console.ReadInteger();
                isValidInput = beginInputPort.IsValidNumberOfHumans(numberOfPlayers, numberOfHumans);
            }
        }
    }
}

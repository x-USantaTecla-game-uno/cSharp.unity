using System.Collections;
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

        public IEnumerator Begin()
        {
            yield return AskForNumberOfPlayers();
            yield return AskForNumberOfHumans();

            var request = new CreateUnoRequest(numberOfPlayers, numberOfHumans);

            beginInputPort.CreateUno(request);
        }

        IEnumerator AskForNumberOfPlayers()
        {
            var isValidInput = false;

            while(!isValidInput)
            {
                yield return console.Read();
                //TODO: Console tiene que tener un NumberRead que te haga la conversión por dentro.
                numberOfPlayers = int.Parse(console.CharacterRead); 

                isValidInput = beginInputPort.IsValidNumberOfPlayers(numberOfPlayers);
            }
        }

        IEnumerator AskForNumberOfHumans()
        {
            var isValidInput = false;

            while (!isValidInput)
            {
                yield return console.Read();
                //TODO: Console tiene que tener un NumberRead que te haga la conversión por dentro.
                numberOfHumans =  int.Parse(console.CharacterRead);
                isValidInput = beginInputPort.IsValidNumberOfHumans(numberOfHumans);
            }
        }
    }
}

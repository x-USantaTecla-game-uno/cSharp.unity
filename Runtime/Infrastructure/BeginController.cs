using System.Collections;
using Uno.Runtime.Application;

namespace Uno.Runtime.Infrastructure
{
    public class BeginController
    {
        private BeginInputPort beginInputPort;
        private Console console;

        private int numberOfPlayers;
        private int numberOfHumans;

        #region Constructors

        public BeginController(BeginInputPort beginInputPort)
        {
            this.beginInputPort = beginInputPort;
        }

        #endregion

        public IEnumerator Begin()
        {
            yield return GetNumberOfPlayers();
            yield return GetNumberOfHumans();

            UnoRequest request = new UnoRequest(numberOfPlayers, numberOfHumans);

            beginInputPort.CreateUno(request);
        }

        private IEnumerator GetNumberOfPlayers()
        {
            bool isValidInput;

            do
            {
                beginInputPort.NoticeWantNumberOfPlayers();
                yield return console.Read();
                numberOfPlayers =  int.Parse(console.CharacterRead);

                isValidInput = beginInputPort.IsValidNumberOfPlayers(numberOfPlayers);
            } while (!isValidInput);
        }
        
        private IEnumerator GetNumberOfHumans()
        {
            bool isValidInput;

            do
            {
                beginInputPort.NoticeWantNumberOfHumans();
                yield return console.Read();
                numberOfHumans =  int.Parse(console.CharacterRead);
                isValidInput = beginInputPort.IsValidNumberOfHumans(numberOfHumans);
            } while (!isValidInput);
        }
    }
}

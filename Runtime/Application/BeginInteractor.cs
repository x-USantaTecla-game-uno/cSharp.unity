
namespace Uno.Runtime.Application
{
    public class BeginInteractor: BeginInputPort
    {
        public BeginInteractor()
        {
            
        }
        
        public void NoticeWantNumberOfPlayers()
        {
            
        }

        public void NoticeWantNumberOfHumans()
        {
            
        }

        public void CreateUno(UnoRequest request)
        {
            new Domain.Uno(request.NumberOfPlayers, request.NumberOfHuman);
        }

        public bool IsValidNumberOfPlayers(int numberOfPlayers)
        {
            return false;
        }

        public bool IsValidNumberOfHumans(int numberOfHumans)
        {
            return false;
        }
    }
}

namespace Uno.Runtime.Application
{
    public interface BeginInputPort
    {
        void NoticeWantNumberOfPlayers();
        void NoticeWantNumberOfHumans();
        void CreateUno(UnoRequest request);
        bool IsValidNumberOfPlayers(int numberOfPlayers);
        bool IsValidNumberOfHumans(int numberOfHumans);
    }
}
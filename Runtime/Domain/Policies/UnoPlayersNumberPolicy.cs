namespace Uno.Runtime.Domain
{
    public class UnoPlayersNumberPolicy
    {
        public const int MAX_PLAYER_NUMBER = 10;
        public const int MIN_PLAYER_NUMBER = 2;
        
        public bool IsValidNumberOfPlayers(int numberOfPlayers)
        {
            return numberOfPlayers <= MAX_PLAYER_NUMBER && numberOfPlayers >= MIN_PLAYER_NUMBER;
        }
    }
}

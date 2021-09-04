namespace Uno.Runtime.Application
{
    public interface BeginInputPort
    {
        void CreateUno(CreateUnoRequest request);

        //TODO: tirando un poco más de DDD que en el resto de comentarios, aquí se me ocurre jugar con la request en ambos métodos.
        //TODO: aunque desde nuestro punto de vista como programadores/diseñadores quede más raro, refuerza el ubiquituous language.
        bool IsValidNumberOfPlayers(int numberOfPlayers);
        bool IsValidNumberOfHumans(int numberOfPlayers, int numberOfHumans);
    }
}
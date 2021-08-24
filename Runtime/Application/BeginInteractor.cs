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

        public void CreateUno(CreateUnoRequest request)
        {
            //TODO: tiene que almacenarse la referencia en algún lado.
            new Domain.Uno(request.NumberOfPlayers, request.NumberOfHuman);
        }

        public bool IsValidNumberOfPlayers(int numberOfPlayers)
        {
            //TODO: tiene que preguntarle a una UnoPlayersNumberPolicy, que devuelva que sí si está entre 2 y 10.
            //TODO: esta Policy podría estar "embebida" en la API pública de la clase Uno, incluso estáticamente. 
            return false;
        }

        public bool IsValidNumberOfHumans(int numberOfHumans)
        {
            //TODO: tiene que preguntarle a alguien del dominio, de nuevo puede ser a Uno estáticamente.
            //TODO: a fin de cuentas es que sea un número <= al total de jugadores, pero no es responsabilidad suya la operación.
            return false;
        }
    }
}

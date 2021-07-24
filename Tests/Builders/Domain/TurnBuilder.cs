using Uno.Runtime.Domain;

namespace Uno.Tests.Builders.Domain
{
    public class TurnBuilder : Builder<Turn>
    {
        Player[] players = new Player[0];
        
        #region Fluent API
        public TurnBuilder WithPlayers(params Player[] players)
        {
            this.players = players;
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        TurnBuilder() { }

        internal static TurnBuilder New() => new TurnBuilder();
        #endregion

        #region Builder implementation
        public override Turn Build() => new Turn(players);
        #endregion
    }
}
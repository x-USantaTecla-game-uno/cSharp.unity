using Uno.Runtime.Domain;

namespace Uno.Tests.Builders.Domain
{
    public class PlayerBuilder : Builder<Player>
    {
        #region Fluent API

        #endregion

        #region ObjectMother/FactoryMethods
        PlayerBuilder() { }

        internal static PlayerBuilder New() => new PlayerBuilder();
        #endregion

        #region Builder implementation
        public override Player Build() => new Player();
        #endregion
    }
}
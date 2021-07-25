using Uno.Runtime.Domain;

namespace Uno.Tests.Builders.Domain
{
    public class UnoDeckBuilder : Builder<UnoDeck>
    {
        #region Fluent API

        #endregion

        #region ObjectMother/FactoryMethods
        UnoDeckBuilder()
        {
        }

        public static UnoDeckBuilder New() => new UnoDeckBuilder();
        #endregion

        #region Builder implementation
        public override UnoDeck Build() => new UnoDeck();
        #endregion
    }
}
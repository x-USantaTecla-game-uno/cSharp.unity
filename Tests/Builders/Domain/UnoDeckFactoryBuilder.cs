using Uno.Runtime.Domain;
using Uno.Runtime.Domain.Entities.Cards;

namespace Uno.Tests.Builders.Domain
{
    public class UnoDeckFactoryBuilder : Builder<UnoDeckFactory>
    {
        #region ObjectMother/FactoryMethods
        UnoDeckFactoryBuilder() { }

        public static UnoDeckFactoryBuilder New() => new UnoDeckFactoryBuilder();
        #endregion

        #region Builder implementation
        public override UnoDeckFactory Build() => new UnoDeckFactory();
        #endregion
    }
}
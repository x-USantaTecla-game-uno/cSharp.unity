using Uno.Runtime.Domain;

namespace Uno.Tests.Builders.Domain
{
    public class NumeredCardBuilder : Builder<NumeredCard>
    {
        Color color = Color.Any;
        int number = 0;
        
        #region Fluent API
        public NumeredCardBuilder WithColor(Color color)
        {
            this.color = color;
            return this;
        }

        public NumeredCardBuilder WithNumber(int number)
        {
            this.number = number;
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        NumeredCardBuilder() { }

        internal static NumeredCardBuilder New() => new NumeredCardBuilder();
        #endregion

        #region Builder implementation
        public override NumeredCard Build() => new NumeredCard(color, number);
        #endregion
    }
}
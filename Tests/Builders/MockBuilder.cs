using NSubstitute;

namespace Uno.Tests.Builders
{
    public class MockBuilder<T> : Builder<T> where T : class // T : interface is not a thing yet.
    {
        protected readonly T mock = Substitute.For<T>();
        
        public override T Build() => mock;
    }
}
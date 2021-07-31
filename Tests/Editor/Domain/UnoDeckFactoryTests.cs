using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;
using Uno.Tests.Builders.Domain;

namespace Uno.Tests.Editor.Domain
{
    public class UnoDeckFactoryTests
    {
        [Test]
        public void NewUnoDeck_ContainsExactly108Cards()
        {
            UnoDeckFactory sut = Build.UnoDeckFactory();

            var createdDeck = sut.Create();

            createdDeck.TotalCards.Should().Be(108);
        }
    }
}
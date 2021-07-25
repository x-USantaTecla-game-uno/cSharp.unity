using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;
using Uno.Tests.Builders.Domain;

namespace Uno.Tests.Editor.Domain
{
    public class UnoDeckTests
    {
        [Test]
        public void NewUnoDeck_ContainsExactly108Cards()
        {
            UnoDeck sut = Build.UnoDeck();

            var containedCardsAmount = sut.TotalCards;

            containedCardsAmount.Should().Be(108);
        }
    }
}
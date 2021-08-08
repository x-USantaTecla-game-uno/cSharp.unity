using FluentAssertions;
using FluentAssertions.Extensions;
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

            createdDeck.AllCards().Should().HaveCount(108);
        }
        
        [Test]
        public void NewUnoDeck_ContainsJustOneZero_PerColor()
        {
            UnoDeckFactory sut = Build.UnoDeckFactory();

            var createdDeck = sut.Create();

            createdDeck.CardsWithNumber(0).Should().HaveCount(1.PerColor());
        }
        
        [Test]
        public void NewUnoDeck_ContainsTwoCardsOfEachPositiveNumber_PerColor([Range(1, 9)]int cardNumber)
        {
            UnoDeckFactory sut = Build.UnoDeckFactory();

            var createdDeck = sut.Create();

            createdDeck.CardsWithNumber(cardNumber).Should().HaveCount(2.PerColor());
        }
        
        [Test]
        public void NewUnoDeck_ContainsSixActionCards_PerColor()
        {
            UnoDeckFactory sut = Build.UnoDeckFactory();

            var createdDeck = sut.Create();

            createdDeck.ActionCards().Should().HaveCount(6.PerColor());
        }
        
        [Test]
        public void NewUnoDeck_ContainsEightWildCards()
        {
            UnoDeckFactory sut = Build.UnoDeckFactory();

            var createdDeck = sut.Create();

            createdDeck.WildCards().Should().HaveCount(8);
        }
    }
}
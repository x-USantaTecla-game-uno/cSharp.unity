using System;
using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;
using Uno.Tests.Builders.Domain;

namespace Uno.Tests.Editor.Domain
{
    public class PlayerTests
    {
        [Test]
        public void NewPlayer_HasNotCards()
        {
            Player sut = Build.Player();

            var playerRemainingCards = sut.RemainingCards;

            playerRemainingCards.Should().Be(0);
        }

        [Test]
        public void NewPlayer_WithStartingHand_HasCards()
        {
            Player sut = Build.Player().WithHand(Fake.Card());

            var playerRemainingCards = sut.RemainingCards;

            playerRemainingCards.Should().BePositive();
        }

        [Test]
        public void NewPlayer_AfteAddCard_HasNotZeroCards()
        {
            Player sut = Build.Player();
            sut.AddCard(Fake.Card());

            var playerRemainingCards = sut.RemainingCards;

            playerRemainingCards.Should().BePositive();
        }

        [Test, Category("TODO"), Ignore("TODO")]
        public void PlayerWithCards_AfterRemoveCard_HasLessCards()
        {
            var docCard = Fake.Card();
            Player sut = Build.Player().WithHand(docCard);

            var cardsBefore = sut.RemainingCards;
            sut.RemoveCard(docCard);
            var cardsAfter = sut.RemainingCards;

            cardsAfter.Should().BeLessThan(cardsBefore);
        }

        [Test]
        public void RemovingCard_FromPlayerWhoDoesNotOwnThatCard_ThrowsException()
        {
            var docCard = Fake.Card();
            Player sut = Build.Player();

            Action act = () => sut.RemoveCard(docCard);

            act.Should().Throw<InvalidOperationException>();
        }
    }
}
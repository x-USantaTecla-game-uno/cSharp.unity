using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;
using Uno.Runtime.Domain.Entities;
using Uno.Runtime.Domain.Entities.Cards;
using Uno.Tests.Builders.Domain;

namespace Uno.Tests.Editor.Domain
{
    public class DeckTests
    {
        [Test]
        public void Draw_FromDeckWithOneCard_ReturnsThatCard()
        {
            Card docCard = Fake.Card();
            Deck sut = Build.Deck().WithCards(docCard);

            var drawnCard = sut.Draw();

            drawnCard.Should().Be(docCard);
        }

        [Test]
        public void PlayAnyCard_ThenThatCardIsLastDiscard()
        {
            Deck sut = Build.Deck();

            Card playedCard = Fake.Card();
            sut.Play(playedCard);

            sut.LastDiscard.Should().Be(playedCard);
        }

        [Test]
        public void Draw_FromDeckWithoutDrawableCards_ReturnsCardFromDiscards()
        {
            //Arrange
            Deck sut = Build.Deck();
            Card playedCard = Fake.Card();
            sut.Play(playedCard);
            
            sut.Play(Fake.Card());
            
            //Act     
            var drawnCard = sut.Draw();

            //Assert
            drawnCard.Should().Be(playedCard);
        }

        [Test]
        public void Draw_FromDeckWithoutAnyCard_ThrowsException()
        {
            Deck sut = Build.Deck();
            
            Action act = () => sut.Draw();

            act.Should().Throw<InvalidOperationException>();
        }
        
        [Test]
        public void Draw_FromDeckWithJustOneCard_IfThatCardWasDiscarded_ThrowsException()
        {
            Deck sut = Build.Deck();
            sut.Play(Fake.Card());
            
            Action act = () => sut.Draw();

            act.Should().Throw<InvalidOperationException>();
        }

        [Test, Retry(1000)]
        public void Draw_FromPlentyCards_DoesNotReturnAlwaysTheFirstInsertedCard()
        {
            var docCards = Build.NumeredCard().BunchOf(50);
            Deck sut = Build.Deck().WithCards(docCards);
            
            var drawnCard = sut.Draw();

            var firstInsertedCard = docCards.First();
            drawnCard.Should().NotBe(firstInsertedCard);
        }
    }
}
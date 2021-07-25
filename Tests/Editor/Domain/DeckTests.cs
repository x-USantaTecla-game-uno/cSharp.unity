using System;
using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;
using Uno.Tests.Builders.Domain;

namespace Uno.Tests.Editor.Domain
{
    public class DeckTests
    {
        [Test]
        public void Draw_FromDeckWithOneCard_ReturnsThatCard()
        {
            Card docCard = Build.NumeredCard();
            Deck sut = Build.Deck().WithCards(docCard);

            var drawnCard = sut.Draw();

            drawnCard.Should().Be(docCard);
        }

        [Test]
        public void PlayAnyCard_ThenThatCardIsLastDiscard()
        {
            Deck sut = Build.Deck();

            Card playedCard = Build.NumeredCard();
            sut.Play(playedCard);

            sut.LastDiscard.Should().Be(playedCard);
        }

        [Test]
        public void Draw_FromDeckWithoutDrawableCards_ReturnsCardFromDiscards()
        {
            //Arrange
            Deck sut = Build.Deck();
            Card playedCard = Build.NumeredCard();
            sut.Play(playedCard);
            
            sut.Play(Build.NumeredCard());
            
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
            sut.Play(Build.NumeredCard());
            
            Action act = () => sut.Draw();

            act.Should().Throw<InvalidOperationException>();
        }
    }
}
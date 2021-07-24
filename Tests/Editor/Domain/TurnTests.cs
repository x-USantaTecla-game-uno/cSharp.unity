using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;
using Uno.Tests.Builders.Domain;

namespace Uno.Tests.Editor.Domain
{
    public class TurnTests
    {
        [Test]
        public void FirstTurn_DirectionIsForwards()
        {
            Turn sut = Build.Turn();

            var result = sut.IsForwards;

            result.Should().BeTrue();
        }

        [Test]
        public void Turn_SwitchDirection_DirectionChanges()
        {
            Turn sut = Build.Turn();

            var resultBefore = sut.IsForwards;
            sut.SwitchDirection();
            var resultAfter = sut.IsForwards;

            resultAfter.Should().Be(!resultBefore);
        }

        [Test]
        public void Turn_SwitchDirectionTwice_DirectionIsTheSame()
        {
            Turn sut = Build.Turn();

            var resultBefore = sut.IsForwards;
            sut.SwitchDirection();
            sut.SwitchDirection();
            var resultAfter = sut.IsForwards;

            resultAfter.Should().Be(resultBefore);
        }

        [Test]
        public void FirstTurn_CurrentPlayerIsFirstPlayer()
        {
            Player firstPlayer = Build.Player();
            Turn sut = Build.Turn().WithPlayers(firstPlayer, Build.Player());

            var result = sut.CurrentPlayer;

            result.Should().Be(firstPlayer);
        }

        [Test]
        public void FirstTurn_NextPlayerIsSecondPlayer()
        {
            Player secondPlayer = Build.Player();
            Turn sut = Build.Turn().WithPlayers(Build.Player(), secondPlayer);

            var result = sut.NextPlayer;

            result.Should().Be(secondPlayer);
        }

        [Test]
        public void SecondTurn_CurrentPlayerIsSecondPlayer()
        {
            Player secondPlayer = Build.Player();
            Turn sut = Build.Turn().WithPlayers(Build.Player(), secondPlayer);

            sut.ToNext();
            var result = sut.CurrentPlayer;

            result.Should().Be(secondPlayer);
        }
    }
}
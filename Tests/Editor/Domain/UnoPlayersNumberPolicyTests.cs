using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;

namespace Uno.Tests.Editor.Domain
{
    public class UnoPlayersNumberPolicyTests
    {
        [Theory]
        public void MinNumberOfPlayers_Validates_IsValidInput()
        {
            var sut = new UnoPlayersNumberPolicy();

            var result = sut.IsValidNumberOfPlayers(UnoPlayersNumberPolicy.MIN_PLAYER_NUMBER);

            result.Should().BeTrue();
        }
        
        [Theory]
        public void MaxNumberOfPlayers_Validates_IsValidInput()
        {
            var sut = new UnoPlayersNumberPolicy();

            var result = sut.IsValidNumberOfPlayers(UnoPlayersNumberPolicy.MAX_PLAYER_NUMBER);

            result.Should().BeTrue();
        }
        
        [Theory]
        public void OneLessThanMinNumberOfPlayers_Validates_IsNotValidInput()
        {
            var sut = new UnoPlayersNumberPolicy();

            var result = sut.IsValidNumberOfPlayers(UnoPlayersNumberPolicy.MIN_PLAYER_NUMBER - 1);

            result.Should().BeFalse();
        }
        
        [Theory]
        public void OneMoreThanMaxNumberOfPlayers_Validates_IsNotValidInput()
        {
            var sut = new UnoPlayersNumberPolicy();

            var result = sut.IsValidNumberOfPlayers(UnoPlayersNumberPolicy.MAX_PLAYER_NUMBER + 1);

            result.Should().BeFalse();
        }
        
        [Theory]
        public void NumberOfPlayersAndBiggerNumberHumans_Validates_IsNotValidInput()
        {
            var sut = new UnoPlayersNumberPolicy();
            var numberOfPlayers = 3;

            var result = sut.IsValidNumberOfHumans(numberOfPlayers, numberOfPlayers + 1);

            result.Should().BeFalse();
        }
        
        
        [Theory]
        public void NumberOfPlayersAndSameNumberOfHumans_Validates_IsValidInput()
        {
            var sut = new UnoPlayersNumberPolicy();
            var numberOfPlayers = 3;
            
            var result = sut.IsValidNumberOfHumans(numberOfPlayers, numberOfPlayers);

            result.Should().BeTrue();
        }
    }
}

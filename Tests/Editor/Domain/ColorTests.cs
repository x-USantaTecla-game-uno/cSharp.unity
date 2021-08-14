using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;
using Uno.Runtime.Domain.Entities;

namespace Uno.Tests.Editor.Domain
{
    public class ColorTests
    {
        [Theory]
        public void AnyColor_MatchesToAllOtherColors(Color otherColor)
        {
            var sut = Color.Any;

            var result = sut.Matches(otherColor);

            result.Should().BeTrue();
        }
    }
}
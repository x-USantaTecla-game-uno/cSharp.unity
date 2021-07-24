using FluentAssertions;
using NUnit.Framework;
using Uno.Runtime.Domain;
using Uno.Tests.Builders.Domain;

namespace Uno.Tests.Editor.Domain
{
    public class NumeredCardTests
    {
        [Test]
        public void NumeredCard_MatchesOther_IfNumberIsTheSame()
        {
            NumeredCard sut1 = Build.NumeredCard().WithNumber(2);
            NumeredCard sut2 = Build.NumeredCard().WithNumber(2);

            var result = sut1.Matches(sut2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void NumeredCard_DoesNotMatchOther_IfNumberIsNotTheSame()
        {
            NumeredCard sut1 = Build.NumeredCard().WithNumber(2);
            NumeredCard sut2 = Build.NumeredCard().WithNumber(3);

            var result = sut1.Matches(sut2);

            result.Should().BeFalse();
        }
    }
}
using System;
using FluentAssertions;
using NUnit.Framework;
using static RGV.Testing.Runtime.TestApi;

namespace RGV.Testing.Tests
{
    public class TestApiTests
    {
        const Action Whatever = null;

        [TestCase(-1), TestCase(0), TestCase(1)]
        public void AtLeast_MoreThanTwo_Repetitions(int n)
        {
            Action act = () => Repeat(Whatever!).For(n.Times());
            act.Should().Throw<Exception>();
        }

        [Test]
        public void Repeat_Iterations()
        {
            var result = 0;

            Repeat(() => result++).For(37.Times());

            result.Should().Be(37);
        }
    }
}
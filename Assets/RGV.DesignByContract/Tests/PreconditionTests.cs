using System;
using FluentAssertions;
using NUnit.Framework;
using RGV.DesignByContract.Runtime;

namespace RGV.DesignByContract.Tests
{
    public class PreconditionTests
    {
        [Test]
        public void Negated_AndTwiceNegated_Differ()
        {
            var sut = new Precondition<bool>(true);

            sut.Satisfy(b => b).Should().Be
            (
                sut.Not.Not.Satisfy(b => b)
            ).And.Be
            (
                !sut.Not.Satisfy(b => b)
            );
        }

        [Test]
        public void Not_TruthTable()
        {
            var sut = new Precondition<bool>(true);

            sut.Satisfy(b => b).Should().BeTrue();
            sut.Not.Satisfy(b => b).Should().BeFalse();

            sut.Satisfy(b => !b).Should().BeFalse();
            sut.Not.Satisfy(b => !b).Should().BeTrue();
        }

        [Test]
        public void Not_ReturnsNewInstance_ToAvoidAliasing()
        {
            var sut = new Precondition<bool>(true);
            sut.Not.Not.Should().NotBeSameAs(sut);
        }

        #region Fixture
        public class ExactlyThisException : Exception { }
        #endregion
    }
}
using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using static RGV.DesignByContract.Runtime.Precondition;

namespace RGV.DesignByContract.Tests
{
    public class RequireTests
    {
        [Test]
        public void GreaterThan_NotThrowing()
        {
            Action actNotThrowing = () => Require(1).GreaterThan(0);
            actNotThrowing.Should().NotThrow();
        }

        [Test]
        public void GreaterThan_Throwing()
        {
            Action actThrowing = () => Require(1).GreaterThan(1);
            actThrowing.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void LesserThan_NotThrowing()
        {
            Action actNotThrowing = () => Require(-1).LesserThan(0);
            actNotThrowing.Should().NotThrow();
        }

        [Test]
        public void LesserThan_Throwing()
        {
            Action actThrowing = () => Require(1).LesserThan(1);
            actThrowing.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Not_NotThrowing()
        {
            Action actNotThrowing = () => Require(0).Not.GreaterThan(1);
            actNotThrowing.Should().NotThrow();
        }

        [Test]
        public void Not_Throwing()
        {
            Action actThrowing = () => Require(1).Not.GreaterThan(0);
            actThrowing.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void NotNull_Throwing()
        {
            Action act = () => Require(null).Not.Null();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        [Obsolete("In the past it cannot")]
        public void Can_Negate_Twice()
        {
            Action act = () => Require(1).Not.Not.GreaterThan(0);
            act.Should().NotThrow();
        }

        [Test]
        public void False_NotThrowing()
        {
            Action act = () => Require(() => true).Not.False();
            act.Should().NotThrow();
        }

        [Test]
        public void True_Throwing()
        {
            Action act = () => Require(false).True();
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void True_NotThrowing()
        {
            Action act = () => Require(true).True();
            act.Should().NotThrow();
        }

        [Test]
        public void Default_Throwing()
        {
            Action act = () => Require(default(int)).Not.Default();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Default_NotThrowing()
        {
            Action act = () => Require(default(int)).Default();
            act.Should().NotThrow();
        }

        [Test]
        public void Midnight_Throwing()
        {
            Action act = () => Require(DateTime.MinValue).Not.AtMidnight();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Midnight_NotThrowing()
        {
            Action act = () => Require(1.December(1990).At(10.Hours())).Not.AtMidnight();
            act.Should().NotThrow();
        }

        [Test]
        public void String_Throwing()
        {
            Action act = () => Require("").Not.NullOrEmpty();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void String_NotThrowing()
        {
            Action act = () => Require("").NullOrEmpty();
            act.Should().NotThrow();
        }

        [Test]
        public void NullOrWhitespace_Trowing()
        {
            Action act = () => Require("").Not.NullOrWhiteSpace();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void NullOrWhitespace_NotThrowing()
        {
            Action act = () => Require("").NullOrWhiteSpace();
            act.Should().NotThrow();
        }

        [Test]
        public void LetClientSpecifyException()
        {
            Action act = () => Require<OverflowException>(true).False();
            act.Should().Throw<OverflowException>();
        }

        [Test]
        public void Between_Throwing()
        {
            Action act = () => Require(1).Between(2, 3);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Between_NotThrowing()
        {
            Action act = () => Require(1).Between(1, 3);
            act.Should().NotThrow();
        }

        [Test]
        public void Zero_NotThrowing()
        {
            Action act = () => Require(1).Not.Zero();
            act.Should().NotThrow();
        }

        [Test]
        public void Zero_Throwing()
        {
            Action act = () => Require(0).Not.Zero();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void AproxZero_NotThrowing()
        {
            Action act = () => Require(0f).AproxZero();
            act.Should().NotThrow();
        }

        [Test]
        public void AproxZero_Throwing()
        {
            Action act = () => Require(0.000001f).Not.AproxZero(0.000001f);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void AproxZero_DefaultError_IsEpsilon()
        {
            Action act = () => Require(float.Epsilon).Not.AproxZero();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Positive_Throwing()
        {
            Action act = () => Require(-1).Positive();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Positive_NotThrowing()
        {
            Action act = () => Require(1).Positive();
            act.Should().NotThrow();
        }

        [Test]
        public void Negative_Throwing()
        {
            Action act = () => Require(0).Negative();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Negative_NotThrowing()
        {
            Action act = () => Require(-1).Negative();
            act.Should().NotThrow();
        }
    }
}
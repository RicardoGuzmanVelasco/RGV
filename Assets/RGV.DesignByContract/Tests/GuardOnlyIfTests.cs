using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using RGV.DesignByContract.Runtime;

namespace RGV.DesignByContract.Tests
{
    public class GuardOnlyIfTests
    {
        [Test]
        public void GreaterThan_Assignation()
        {
            var result = Guard.OnlyIf(1).GreaterThan(0);
            result.Should().Be(1);
        }

        [Test]
        public void GreaterThan_NotThrowing()
        {
            Action actNotThrowing = () => Guard.OnlyIf(1).GreaterThan(0);
            actNotThrowing.Should().NotThrow();
        }

        [Test]
        public void GreaterThan_Throwing()
        {
            Action actThrowing = () => Guard.OnlyIf(1).GreaterThan(1);
            actThrowing.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void LesserThan_Assignation()
        {
            var result = Guard.OnlyIf(-1).LesserThan(0);
            result.Should().Be(-1);
        }

        [Test]
        public void LesserThan_NotThrowing()
        {
            Action actNotThrowing = () => Guard.OnlyIf(-1).LesserThan(0);
            actNotThrowing.Should().NotThrow();
        }

        [Test]
        public void LesserThan_Throwing()
        {
            Action actThrowing = () => Guard.OnlyIf(1).LesserThan(1);
            actThrowing.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Not_Assignation()
        {
            var result = Guard.OnlyIf(0).Not.GreaterThan(1);
            result.Should().Be(0);
        }

        [Test]
        public void Not_NotThrowing()
        {
            Action actNotThrowing = () => Guard.OnlyIf(0).Not.GreaterThan(1);
            actNotThrowing.Should().NotThrow();
        }

        [Test]
        public void Not_Throwing()
        {
            Action actThrowing = () => Guard.OnlyIf(1).Not.GreaterThan(0);
            actThrowing.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void NotNull_Throwing()
        {
            Action act = () => Guard.OnlyIf(null).Not.Null();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void NotNull_Assignation()
        {
            var expected = new object();

            var result = Guard.OnlyIf(expected).Not.Null();

            result.Should().Be(expected);
        }

        [Test]
        [Obsolete("In the past it cannot")]
        public void Can_Negate_Twice()
        {
            Action act = () => Guard.OnlyIf(1).Not.Not.GreaterThan(0);
            act.Should().NotThrow();
        }

        [Test]
        public void False_Throwing()
        {
            Action act = () => Guard.OnlyIf(() => true).False();
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void False_NotThrowing()
        {
            Action act = () => Guard.OnlyIf(() => true).Not.False();
            act.Should().NotThrow();
        }

        [Test]
        public void False_Assignation()
        {
            //Las precondiciones de bool no tiene sentido que devuelvan.

            //Esto es solo un test informativo.
            true.Should().BeTrue();
        }

        [Test]
        public void True_Throwing()
        {
            Action act = () => Guard.OnlyIf(false).True();
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void True_NotThrowing()
        {
            Action act = () => Guard.OnlyIf(true).True();
            act.Should().NotThrow();
        }

        [Test]
        public void True_Assignation()
        {
            //Las precondiciones de bool no tiene sentido que devuelvan.

            //Esto es solo un test informativo.
            true.Should().BeTrue();
        }

        [Test]
        public void Default_Throwing()
        {
            Action act = () => Guard.OnlyIf(default(int)).Not.Default();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Default_NotThrowing()
        {
            Action act = () => Guard.OnlyIf(default(int)).Default();
            act.Should().NotThrow();
        }

        [Test]
        public void Default_Assignation()
        {
            var result = Guard.OnlyIf(2).Not.Default();
            result.Should().Be(2);
        }

        [Test]
        public void Midnight_Throwing()
        {
            Action act = () => Guard.OnlyIf(DateTime.MinValue).Not.AtMidnight();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Midnight_NotThrowing()
        {
            Action act = () => Guard.OnlyIf(1.December(1990).At(10.Hours())).Not.AtMidnight();
            act.Should().NotThrow();
        }

        [Test]
        public void Midnight_Assignation()
        {
            var result = Guard.OnlyIf(1.December(1990).At(10.Hours())).Not.AtMidnight();
            result.Should().Be(1.December(1990).At(10.Hours()));
        }

        [Test]
        public void String_Throwing()
        {
            Action act = () => Guard.OnlyIf("").Not.NullOrEmpty();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void String_NotThrowing()
        {
            Action act = () => Guard.OnlyIf("").NullOrEmpty();
            act.Should().NotThrow();
        }

        [Test]
        public void String_Assignation()
        {
            var result = Guard.OnlyIf("asdf").Not.NullOrEmpty();
            result.Should().Be("asdf");
        }

        [Test]
        public void NullOrWhitespace_Trowing()
        {
            Action act = () => Guard.OnlyIf("").Not.NullOrWhiteSpace();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void NullOrWhitespace_NotThrowing()
        {
            Action act = () => Guard.OnlyIf("").NullOrWhiteSpace();
            act.Should().NotThrow();
        }

        [Test]
        public void NullOrWhitespace_Assignation()
        {
            var result = Guard.OnlyIf("asdf").Not.NullOrWhiteSpace();
            result.Should().Be("asdf");
        }

        [Test]
        public void LetClientSpecifyException()
        {
            Action act = () => Guard.OnlyIf<OverflowException>(true).False();
            act.Should().Throw<OverflowException>();
        }

        [Test]
        public void Between_Throwing()
        {
            Action act = () => Guard.OnlyIf(1).Between(2, 3);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Between_NotThrowing()
        {
            Action act = () => Guard.OnlyIf(1).Between(1, 3);
            act.Should().NotThrow();
        }

        [Test]
        public void Between_Assignation()
        {
            var result = Guard.OnlyIf(2).Between(1, 3);
            result.Should().Be(2);
        }

        [Test]
        public void Zero_NotThrowing()
        {
            Action act = () => Guard.OnlyIf(1).Not.Zero();
            act.Should().NotThrow();
        }

        [Test]
        public void Zero_Throwing()
        {
            Action act = () => Guard.OnlyIf(0).Not.Zero();
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Zero_Assignation()
        {
            var result = Guard.OnlyIf(1).Not.Zero();
            result.Should().Be(1);
        }

        [Test]
        public void AproxZero_NotThrowing()
        {
            Action act = () => Guard.OnlyIf(0f).AproxZero();
            act.Should().NotThrow();
        }

        [Test]
        public void AproxZero_Throwing()
        {
            Action act = () => Guard.OnlyIf(0.000001f).Not.AproxZero(0.000001f);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void AproxZero_Assignation()
        {
            var result = Guard.OnlyIf(1f).Not.AproxZero();
            result.Should().Be(1);
        }

        [Test]
        public void AproxZero_DefaultError_IsEpsilon()
        {
            Action act = () => Guard.OnlyIf(float.Epsilon).Not.AproxZero();
            act.Should().Throw<ArgumentException>();
        }
    }
}
using System;
using FluentAssertions;
using NUnit.Framework;
using RGV.Math.Runtime.Roman;

namespace RGV.Math.Tests
{
    public class RomanNumeralTests
    {
        #region Creation
        [Test]
        public void RomanNumeral_IsI_ByDefault()
        {
            var sut = new RomanNumeral();

            sut.Should().Be(new RomanNumeral("I"));
        }

        [Test]
        public void Constructor_Fails_IfNotJustRomanSymbols()
        {
            Action act = () => new RomanNumeral("ILj");

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Constructor_EffectivelyTakes_RomanSymbols()
        {
            var sut = new RomanNumeral("V");

            sut.Should().NotBe(new RomanNumeral());
        }

        [TestCase("IIII")]
        [TestCase("XXXX")]
        [TestCase("CCCC")]
        public void AdditiveNotation_IsNotSupported(string additiveNotation)
        {
            Action act = () => new RomanNumeral(additiveNotation);

            act.Should().ThrowExactly<NotSupportedException>();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void RomanNumeral_CreatedByNumber_MustBePositive(int nonPositive)
        {
            Action act = () => new RomanNumeral(nonPositive);
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        #endregion
    }
}
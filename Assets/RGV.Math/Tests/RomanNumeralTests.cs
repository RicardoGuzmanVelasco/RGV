using System;
using System.Collections.Generic;
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
        
        #region Formatting
        [Test]
        public void RomanNumeral_ToString_ByRomanSymbols()
        {
            var sut = new RomanNumeral("XLVIII");

            sut.ToString().Should().Be("XLVIII");
        }

        [Test]
        public void RomanNumeral_Implicit_FromString()
        {
            RomanNumeral sut = "CMII";

            sut.Should().Be(new RomanNumeral("CMII"));
        }
        #endregion
        
        #region To number, Partition & EquivalenceClasses
        [TestCase("I", 1)]
        [TestCase("V", 5)]
        [TestCase("X", 10)]
        [TestCase("L", 50)]
        [TestCase("C", 100)]
        [TestCase("D", 500)]
        [TestCase("M", 1000)]
        public void RomanSymbols_RespectivelyEquivalent_ToNumbers(string symbols, int number)
        {
            TestEquality(symbols, number);
        }

        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("VI", 6)]
        [TestCase("CLV", 155)]
        public void NonSubtractive_IsEquivalentTo_ItsSymbolsSum(string symbols, int number)
        {
            TestEquality(symbols, number);
        }

        [TestCase("IV", 4)]
        [TestCase("IX", 9)]
        public void JustSubtractive_IsEquivalentTo_ItsReversedSymbolsSubstraction(string symbols, int number)
        {
            TestEquality(symbols, number);
        }

        [TestCase("CMIV", 904)]
        [TestCase("MDCCLXXIV", 1774)]
        [TestCase("MCMXCIX", 1999)]
        public void SomeSubtractive_IsEquivalentTo_ItsIndependentSubstractionAdded(string symbols, int number)
        {
            TestEquality(symbols, number);
        }
        #endregion
        
        #region From number, Partition & EquivalenceClasses
        [TestCase(1, "I")]
        [TestCase(2, "II")]
        [TestCase(3, "III")]
        [TestCase(5, "V")]
        [TestCase(11, "XI")]
        [TestCase(1565, "MDLXV")]
        public void RomanNumeral_CreatedFromNumber_WithJustAdditiveSymbols(int number, string symbols)
        {
            new RomanNumeral(number).Should().Be(new RomanNumeral(symbols));
        }
        
        [TestCase(4, "IV")]
        [TestCase(900, "CM")]
        public void RomanNumeral_CreatedFromNumber_WithJustSubtractiveSymbols(int number, string symbols)
        {
            TestEquality(number, symbols);
        }
        
        [TestCase(19, "XIX")]
        [TestCase(2904, "MMCMIV")]
        [TestCase(3549, "MMMDXLIX")]
        public void RomanNumeral_CreatedFromNumber_BothAdditiveAndSubtractive(int number, string symbols)
        {
            TestEquality(number, symbols);
        }
        #endregion
        
        #region SupportMethods
        static void TestEquality(int number, string symbols)
        {
            new RomanNumeral(number).Should().Be(new RomanNumeral(symbols));
        }
        static void TestEquality(string symbols, int number)
        {
            int sut = new RomanNumeral(symbols);
            sut.Should().Be(number);
        }
        #endregion
    }
}
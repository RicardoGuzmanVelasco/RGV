using System;
using FluentAssertions;
using NUnit.Framework;
using RGV.Extensions.Runtime;

namespace RGV.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [TestCase(" ")]
        [TestCase("5555")]
        public void LongestContiguous_OfSameCharRepeated_IsSameThanLength(string sut)
        {
            sut.LongestContiguous().Should().Be(sut.Length);
        }


        [Test]
        public void LongestContiguous_OfEmptyOrNull_IsZero()
        {
            string.Empty.LongestContiguous().Should().Be(0);
            ((string)null).LongestContiguous().Should().Be(0);
        }

        [TestCase("aabbbaa", 3)]
        [TestCase("120012012", 2)]
        public void LongestContiguous_OfMixed_IgnoresTotalCount(string sut, int expected)
        {
            sut.LongestContiguous().Should().Be(expected);
        }

        [Test]
        public void LongestContiguous_TakesCount_OfLastChainInString()
        {
            "0001138181ddddd".LongestContiguous().Should().Be(5);
        }
    }
}
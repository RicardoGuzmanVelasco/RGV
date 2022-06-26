using FluentAssertions;
using NUnit.Framework;
using RGV.Math.Runtime.LargeNumbers;

namespace RGV.Math.Tests
{
    public class LargeNumbersTests
    {
        [Test]
        public void SameNumber()
        {
            new Numbig(1000).Should().Be(new Numbig(1000));
            new Numbig(78.9f, "T").Should().Be(new Numbig(78.9f, "T"));
        }

        [Test]
        public void NextSuffix()
        {
            new Numbig(1, "k").Should().Be(new Numbig(1000));
            new Numbig(8, "M").Should().Be(new Numbig(8000, "k"));
            new Numbig(8.513f, "B").Should().Be(new Numbig(8513, "M"));
        }

        [Test]
        public void NotContiguousSuffix()
        {
            new Numbig(8, "M").Should().Be(new Numbig(8000000));
        }

        //ignore case
        //non negative?
    }
}
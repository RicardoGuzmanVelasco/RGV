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
        public void Next()
        {
            new Numbig(1, "k").Should().Be(new Numbig(1000));
            new Numbig(8, "M").Should().Be(new Numbig(8000, "k"));
            new Numbig(8.513f, "B").Should().Be(new Numbig(8513, "M"));
        }

        [Test]
        public void NotContiguous()
        {
            new Numbig(8, "M").Should().Be(new Numbig(8000000));
        }

        [Test]
        public void FromSingleToNotSingle()
        {
            new Numbig(1040, "T").Should().Be(new Numbig(1.04f, "aa"));
            new Numbig(1000000, "B").Should().Be(new Numbig(1, "aa"));
        }

        [Test]
        public void FromNotSingleToNotSingleWithSameLength()
        {
            new Numbig(1000, "aa").Should().Be(new Numbig(1, "ab"));
            new Numbig(1000, "zf").Should().Be(new Numbig(1, "zg"));
        }

        [Test]
        public void FromNotSingleToNotSingle_WithDifferentLength()
        {
            new Numbig(1000, "zz").Should().Be(new Numbig(1, "aaa"));
            new Numbig(1000, "zzz").Should().Be(new Numbig(1, "aaaa"));
        }

        //ignore case
        //non negative?
        //addition
    }
}
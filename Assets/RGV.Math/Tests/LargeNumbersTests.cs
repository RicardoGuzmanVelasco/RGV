using System;
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
            Numbig.ToSuffix("k").Should().Be("K");
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

        [Test]
        public void SameLargeNumbersIgnoresCase()
        {
            new Numbig(1, "T").Should().Be(new Numbig(1, "t"));
            new Numbig(1, "b").Should().Be(new Numbig(1, "B"));
            new Numbig(1, "k").Should().Be(new Numbig(1, "K"));
            new Numbig(1, "aa").Should().Be(new Numbig(1, "AA"));
        }

        [Test]
        public void IncorrectSuffixes()
        {
            ((Action)(() =>
                    new Numbig(1, "d")))
                .Should().Throw<ArgumentException>();

            ((Action)(() =>
                    new Numbig(1, "a5z")))
                .Should().Throw<ArgumentException>();
        }

        //to number??? probably avoidable!
        //non negative?
        //addition
    }
}
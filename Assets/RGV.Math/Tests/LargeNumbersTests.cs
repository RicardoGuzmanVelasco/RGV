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
            new Numbig("78.9T").Should().Be(new Numbig(78.9f, "T"));
        }

        [Test]
        public void Next()
        {
            new Numbig("1k").Should().Be(new Numbig(1000));
            new Numbig("8M").Should().Be(new Numbig(8000, "k"));
            new Numbig("8.513B").Should().Be(new Numbig(8513, "M"));
        }

        [Test]
        public void NotContiguous()
        {
            new Numbig("8M").Should().Be(new Numbig(8000000));
        }

        [Test]
        public void FromSingleToNotSingle()
        {
            new Numbig("1040T").Should().Be(new Numbig(1.04f, "aa"));
            new Numbig("1000000B").Should().Be(new Numbig(1, "aa"));
        }

        [Test]
        public void FromNotSingleToNotSingleWithSameLength()
        {
            new Numbig("1000aa").Should().Be(new Numbig(1, "ab"));
            new Numbig("1000zf").Should().Be(new Numbig(1, "zg"));
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

        [Test]
        public void Parse()
        {
            Numbig.Parse("1").Should().Be(new Numbig(1));
            Numbig.Parse("1.0").Should().Be(new Numbig(1));
            Numbig.Parse("1.0k").Should().Be(new Numbig(1, "k"));
            Numbig.Parse("1.0M").Should().Be(new Numbig(1, "m"));
            Numbig.Parse("1.2B").Should().Be(new Numbig(1.2f, "b"));
            Numbig.Parse("1.08T").Should().Be(new Numbig(1.08f, "t"));
            Numbig.Parse("1.0al").Should().Be(new Numbig(1, "al"));
        }

        [Test, Ignore("TODO")]
        public void Addition()
        {
            new Numbig(1, "T").Plus(new Numbig(1, "T")).Should().Be(new Numbig(2, "T"));
        }

        [Test, Ignore("TODO to implement addition, then remove")]
        public void METHOD()
        {
            // new Numbig.Suffix("k").FactorTo(new Numbig.Suffix("")).Should().Be(1000);
            // new Numbig.Suffix("").FactorTo(new Numbig.Suffix("k")).Should().Be(1f/1000);
        }

        //suffix order < > =
        //to number??? probably avoidable!
        //non negative?
    }
}
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
        public void Last()
        {
            new Numbig("0.001k").Should().Be(new Numbig(1));
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
            new Numbig("1000zz").Should().Be(new Numbig("1aaa"));
            new Numbig("1000zzz").Should().Be(new Numbig("1aaaa"));
        }

        [Test]
        public void SameLargeNumbersIgnoresCase()
        {
            new Numbig("1T").Should().Be(new Numbig("1t"));
            new Numbig("1b").Should().Be(new Numbig("1B"));
            new Numbig("1k").Should().Be(new Numbig("1K"));
            new Numbig("1aa").Should().Be(new Numbig("1AA"));
        }

        [TestCase("1d")]
        [TestCase("xa5z")]
        [TestCase("t8h3")]
        [TestCase("2u6")]
        public void IncorrectParse(string toParse)
        {
            ((Action)(() =>
                    new Numbig(toParse)))
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

        [Test]
        public void Addition()
        {
            new Numbig("1T").Plus(new Numbig("1T")).Should().Be(new Numbig("2T"));
            new Numbig("1k").Plus(new Numbig("500")).Should().Be(new Numbig("1.5k"));
        }

        [Test]
        public void Conversion()
        {
            //TODO: esto no tiene sentido porque por dentro se factorizan ya.
            //TODO: lo que se quiere es poder tener el factor de conversión solamente.
            new Numbig("1m").ConvertTo("k").Should().Be(new Numbig("1000k"));
        }

        //to number??? probably avoidable!
        //before, with aa, aaa, az, etc.
    }
}
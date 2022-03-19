using FluentAssertions;
using NUnit.Framework;
using RGV.Extensions.Runtime;

namespace RGV.Extensions.Tests
{
    public class NumericExtensionsTests
    {
        [Test]
        public void FactorizeBase10()
        {
            0.FactorizeBase10().Should().BeEmpty();
            1.FactorizeBase10().Should().BeEquivalentTo(1);
            10.FactorizeBase10().Should().BeEquivalentTo(10);
            11.FactorizeBase10().Should().ContainInOrder(10, 1);
            609.FactorizeBase10().Should().ContainInOrder(600, 9);
            37854.FactorizeBase10().Should().ContainInOrder(30000, 7000, 800, 50, 4);
        }

        [TestCase(-1, -1)]
        [TestCase(int.MinValue, -1)]
        [TestCase(1, 1)]
        [TestCase(int.MaxValue, 1)]
        [TestCase(0, 1)]
        public void SignByNumber(int number, int expected)
        {
            number.Sign().Should().Be(expected);
        }

        [Test]
        public void SignByBool()
        {
            true.Sign().Should().Be(1);
            false.Sign().Should().Be(-1);
        }

        [Test]
        public void SignDeclarative()
        {
            1f.Sign().Should().BePositive();
            (-1).Sign().Should().BeNegative();

            int.MaxValue.Sign().Should().BePositive();
            float.MinValue.Sign().Should().BeNegative();

            0.Sign().Should().BePositive();
        }
    }
}
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
            1.FactorizeBase10().Should().BeEquivalentTo(1);
            10.FactorizeBase10().Should().BeEquivalentTo(10);
            11.FactorizeBase10().Should().ContainInOrder(10, 1);
            609.FactorizeBase10().Should().ContainInOrder(600, 9);
            37854.FactorizeBase10().Should().ContainInOrder(30000, 7000, 800, 50, 4);
        }
    }
}
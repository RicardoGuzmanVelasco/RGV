using System;
using Assets.RGV.Extensions.Runtime.Domain;
using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;

namespace RGV.Extensions.Tests.Editor
{
    public class NumericExtensionsTests
    {
        [DatapointSource, UsedImplicitly]
        int[] EqClassesInts => new[]
        {
            int.MinValue,
            -1,
            0,
            1,
            int.MaxValue
        };

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

        [Test]
        public void SignDeclarative()
        {
            1f.Sign().Should().BePositive();
            (-1).Sign().Should().BeNegative();

            int.MaxValue.Sign().Should().BePositive();
            float.MinValue.Sign().Should().BeNegative();

            0.Sign().Should().BePositive();

            true.Sign().Should().BePositive();
            false.Sign().Should().BeNegative();
        }

        [Theory]
        public void Sign_IsUnitary(int number)
        {
            var sut = number.Sign();

            Math.Abs(sut).Should().Be(1);
        }
    }
}
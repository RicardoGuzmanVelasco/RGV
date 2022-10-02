using System;
using FluentAssertions;
using NUnit.Framework;
using static RGV.DesignByContract.Runtime.Contract;

namespace RGV.DesignByContract.Tests
{
    public class MessagesTests
    {
        [Test]
        public void Message_Contains_ContractTypeName()
        {
            Action pre = () => Require(true).False();
            pre.Should().Throw<Exception>().WithMessage("*Precondition failed*");

            Action post = () => Ensure(true).False();
            post.Should().Throw<Exception>().WithMessage("*Postcondition failed*");

            Action inv = () => Invariant(true).False();
            inv.Should().Throw<Exception>().WithMessage("*Invariant failed*");
        }
    }
}
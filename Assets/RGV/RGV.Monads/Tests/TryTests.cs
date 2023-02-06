using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RGV.Monads.Runtime;

namespace RGV.Monads.Tests
{
    public class TryTests
    {
        [Test]
        public async Task Try_WhenSucceeds_ReturnsOk()
        {
            var result = await Try.To(Try.FromOk);

            result.Should().Be(await Try.FromOk());
        }

        [Test]
        public async Task Try_WhenFails_CallsCommandWithError()
        {
            var result = await Try.FromError("NotCalled");

            await Try.To(Try.FromError("Called")).IfError(e => result = e);

            result.Should().Be(await Try.FromError("Called"));
        }

        [Test]
        public async Task Try_WhenFails_CallsErrorCommand()
        {
            var failed = false;

            await Try.To(Try.FromError("AnyError")).IfError(() => failed = true);

            failed.Should().BeTrue();
        }

        [Test]
        public async Task Try_WhenSucceeds_DoesNotCallErrorCommand()
        {
            var failed = false;

            await Try.To(Try.FromOk).IfError(() => failed = true);

            failed.Should().BeFalse();
        }

        [Test]
        public async Task Try_WhenFails_StopChainingOperations()
        {
            var called = false;

            await Try.To(Try.FromError("AnyError"))
                .Then(Try.FromOk)
                .IfOk(() => called = true);

            called.Should().BeFalse();
        }

        [Test]
        public async Task Try_WhenSucceeds_ContinueChainingOperations()
        {
            var count = 0;

            await Try.To(Try.FromOk)
                .Then(Try.FromOk(() => count++))
                .Then(Try.FromOk(() => count++));

            count.Should().Be(2);
        }

        [Test]
        public async Task Try_WhenFails_SpreadTheCorrectError()
        {
            var result = await Try.To(Try.FromOk)
                .Then(Try.FromOk)
                .Then(Try.FromError("Error that stops the chain"))
                .Then(Try.FromOk)
                .Then(Try.FromOk)
                .Then(Try.FromError("Error not even called"));

            result.Should().Be(new Try.Error("Error that stops the chain"));
        }

        [Test]
        public async Task Try_WhenFails_MatchesToError()
        {
            Try.Error result = null;

            await Try.To(Try.FromError("Whatever"))
                .Resolve
                (
                    ok: null,
                    error: e => result = e
                );

            result.Should().Be(new Try.Error("Whatever"));
        }

        [Test]
        public async Task Try_WhenSucceeds_MatchesToOk()
        {
            var result = false;

            await Try.To(Try.FromOk())
                .Resolve
                (
                    ok: () => result = true,
                    error: null
                );

            result.Should().BeTrue();
        }
    }
}
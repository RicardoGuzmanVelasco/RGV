using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;
using NUnit.Framework;
using RGV.Math.Runtime.Dates.RecurringEvents;

namespace RGV.Math.Tests
{
    public class RecurringEventsTests
    {
        [Test]
        public void RelativeDayOfMonth()
        {
            using var _ = new AssertionScope();

            new RelativeDayEveryMonth(1, DayOfWeek.Sunday)
                .Includes(6.December(1992))
                .Should().BeTrue("because the 6th was the 1st Sunday of December 1992");

            new RelativeDayEveryMonth(1, DayOfWeek.Sunday)
                .Includes(5.December(1992))
                .Should().BeFalse("because the 5th was not the 1st Sunday");

            new RelativeDayEveryMonth(2, DayOfWeek.Sunday)
                .Includes(13.December(1992))
                .Should().BeTrue("because the 13th was the 2nd Sunday of December 1992");

            new RelativeDayEveryMonth(-1, DayOfWeek.Sunday)
                .Includes(6.December(1992))
                .Should().BeFalse("because the 6th was not the last Sunday of December 1992");

            new RelativeDayEveryMonth(-1, DayOfWeek.Sunday)
                .Includes(27.December(1992))
                .Should().BeTrue("because the 27th was the last Sunday of December 1992");
        }
    }
}
using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;
using NUnit.Framework;
using RGV.Math.Runtime.Dates.TemporalExpressions;

namespace RGV.Math.Tests
{
    public class DateRelatedTests
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

        [Test]
        public void DateRanges_Ctor()
        {
            using var _ = new AssertionScope();

            new DateRange(1.January(2020), 31.December(2020))
                .Includes(1.January(2020))
                .Should().BeTrue("because the date range is begin-inclusive by default");

            new DateRange(1.January(2020), 31.December(2020))
                .Includes(31.December(2020))
                .Should().BeTrue("because the date range is end-inclusive by default");

            new DateRange(1.January(2020), 31.December(2020))
                .Includes(1.January(2020).AddDays(-1))
                .Should().BeFalse("because is before the begin");

            new DateRange(1.January(2020), 31.December(2020))
                .Includes(31.December(2020).AddDays(1))
                .Should().BeFalse("because is after the end");
        }

        [Test]
        public void DateRanges_FactoryProperties()
        {
            DateRange.AllTime
                .Includes(DateTime.MinValue)
                .Should().BeTrue("because is the past of all-time range");

            DateRange.AllTime
                .Includes(DateTime.MaxValue)
                .Should().BeTrue("because is the future of all-time range");

            DateRange.Empty
                .Includes(DateTime.MinValue)
                .Should().BeFalse("because empty range contains nothing, nor past");

            DateRange.Empty
                .Includes(DateTime.MaxValue)
                .Should().BeFalse("because empty range contains nothing, nor future");

            DateRange.Empty
                .Includes(new DateTime())
                .Should().BeFalse("because empty range contains nothing, nor default value");
        }

        [Test]
        public void DateRanges_OpenEnded()
        {
            using var _ = new AssertionScope();

            DateRange.Since(1.January(2020))
                .Includes(1.January(2020))
                .Should().BeTrue("because is the right open-ended range");

            DateRange.Since(1.January(2020))
                .Includes(DateTime.MaxValue)
                .Should().BeTrue("because is the future of right open-ended range");

            DateRange.Until(31.December(2020))
                .Includes(31.December(2020))
                .Should().BeTrue("because is the left open-ended range");

            DateRange.Until(31.December(2020))
                .Includes(DateTime.MinValue)
                .Should().BeTrue("because is the past of left open-ended range");
        }
    }
}
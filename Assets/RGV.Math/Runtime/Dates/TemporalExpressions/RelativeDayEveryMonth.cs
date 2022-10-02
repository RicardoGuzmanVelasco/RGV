using System;
using RGV.DesignByContract.Runtime;

namespace RGV.Math.Runtime.Dates.TemporalExpressions
{
    /// <example>Last (-1) tuesday of the month, second (2) friday of the month.</example>>
    public class RelativeDayEveryMonth : TemporalExpression
    {
        readonly DayOfWeek dayOfWeek;
        readonly int relativePositionInMonth;

        public RelativeDayEveryMonth(int relativePositionInMonth, DayOfWeek dayOfWeek)
        {
            Contract.Require(relativePositionInMonth).Not.Zero();

            this.dayOfWeek = dayOfWeek;
            this.relativePositionInMonth = relativePositionInMonth;
        }

        public virtual bool Includes(DateTime when)
        {
            return DayMatches(when) && WeekMatches(when);
        }

        bool DayMatches(DateTime when) => when.DayOfWeek == dayOfWeek;

        bool WeekMatches(DateTime when)
        {
            var dayToCountFrom = relativePositionInMonth > 0
                ? when.Day
                : when.DaysLeftInMonth() + 1;

            return dayToCountFrom.WeekInMonth() == System.Math.Abs(relativePositionInMonth);
        }
    }
}
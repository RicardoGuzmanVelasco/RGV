using System;

namespace RGV.Math.Runtime.Dates.TemporalExpressions
{
    public readonly struct DateRange : TemporalExpression
    {
        readonly DateTime begin;
        readonly DateTime end;

        public DateRange(DateTime begin, DateTime end)
        {
            this.begin = begin;
            this.end = end;
        }

        public static DateRange Since(DateTime when)
        {
            return new DateRange(when, DateTime.MaxValue);
        }

        public static DateRange Until(DateTime when)
        {
            return new DateRange(DateTime.MinValue, when);
        }

        public bool Includes(DateTime when)
        {
            return when >= begin && when <= end;
        }
    }
}
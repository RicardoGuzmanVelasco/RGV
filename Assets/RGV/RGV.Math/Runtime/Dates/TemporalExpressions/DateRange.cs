using System;
using static RGV.DesignByContract.Runtime.Contract;

namespace RGV.Math.Runtime.Dates.TemporalExpressions
{
    /// https://martinfowler.com/eaaDev/Range.html
    public partial class DateRange : TemporalExpression
    {
        readonly DateTime begin;
        readonly DateTime end;

        public DateRange(DateTime begin, DateTime end)
        {
            Require(begin <= end).True();

            this.begin = begin;
            this.end = end;
        }

        public static DateRange Empty { get; } = new EmptyDateRange();

        public static DateRange AllTime { get; } = new(DateTime.MinValue, DateTime.MaxValue);

        public virtual bool Includes(DateTime when)
        {
            return when >= begin && when <= end;
        }

        public static DateRange Since(DateTime when)
        {
            return new DateRange(when, DateTime.MaxValue);
        }

        public static DateRange Until(DateTime when)
        {
            return new DateRange(DateTime.MinValue, when);
        }
    }
}
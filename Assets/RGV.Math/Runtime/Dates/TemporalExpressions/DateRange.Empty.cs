using System;

namespace RGV.Math.Runtime.Dates.TemporalExpressions
{
    public partial class DateRange
    {
        class EmptyDateRange : DateRange
        {
            internal EmptyDateRange() : base(DateTime.MinValue, DateTime.MinValue) { }

            public override bool Includes(DateTime when)
            {
                return false;
            }
        }
    }
}
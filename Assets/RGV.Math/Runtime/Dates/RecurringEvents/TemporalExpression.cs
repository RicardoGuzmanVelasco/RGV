using System;

namespace RGV.Math.Runtime.Dates.RecurringEvents
{
    public abstract class TemporalExpression
    {
        public abstract bool Includes(DateTime when);
    }
}
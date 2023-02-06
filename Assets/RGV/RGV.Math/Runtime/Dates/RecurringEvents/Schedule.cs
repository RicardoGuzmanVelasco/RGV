using System;
using System.Collections.Generic;
using RGV.Math.Runtime.Dates.TemporalExpressions;

namespace RGV.Math.Runtime.Dates.RecurringEvents
{
    public abstract class Schedule
    {
        public abstract bool IsOccuring(RecurringEvent what, DateTime when);
        public abstract DateTime NextOcurrence(RecurringEvent what, DateTime when);

        public abstract IEnumerable<DateTime> Ocurrences(RecurringEvent what, DateRange when);
    }
}
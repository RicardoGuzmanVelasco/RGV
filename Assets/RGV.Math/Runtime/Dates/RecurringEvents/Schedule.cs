using System;
using System.Collections.Generic;

namespace RGV.Math.Runtime.Dates.RecurringEvents
{
    public abstract class Schedule
    {
        public abstract bool IsOccuring(RecurringEvent what, DateTime when);
        public abstract DateTime NextOcurrence(RecurringEvent what, DateTime when);

        //to daterange.
        public abstract IEnumerable<DateTime> Ocurrences(RecurringEvent what, DateTime from, DateTime to);
    }
}
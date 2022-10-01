using System;
using System.Collections.Generic;

namespace RGV.Math.Runtime.RecurringEvents
{
    public abstract class Schedule
    {
        public abstract bool IsOccuring(RecurringEvent what, DateTime when);
        public abstract DateTime NextOcurrence(RecurringEvent what, DateTime when);

        public abstract IEnumerable<DateTime>
            Ocurrences(RecurringEvent what, DateTime from, DateTime to); //to daterange.
    }

    public abstract class RecurringEvent
    {
        public abstract bool Includes(DateTime when);
    }

    class DayEveryMonth : RecurringEvent
    {
        int day;

        public DayEveryMonth(int day)
        {
            this.day = day;
        }

        public override bool Includes(DateTime when)
        {
            throw new NotImplementedException();
        }
    }
}
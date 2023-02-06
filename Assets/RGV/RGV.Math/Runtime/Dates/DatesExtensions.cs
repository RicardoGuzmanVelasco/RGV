using System;

namespace RGV.Math.Runtime.Dates
{
    public static class DatesExtensions
    {
        public static int DaysLeftInMonth(this DateTime when)
        {
            var daysInMonth = DateTime.DaysInMonth(when.Year, when.Month);
            return daysInMonth - when.Day;
        }

        public static int WeekInMonth(this int dayNumber)
        {
            return (dayNumber - 1) / 7 + 1;
        }
    }
}
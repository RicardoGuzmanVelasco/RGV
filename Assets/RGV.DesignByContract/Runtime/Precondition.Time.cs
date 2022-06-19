using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static DateTime AtMidnight(this Precondition<DateTime> precondition)
        {
            precondition.Evaluate
            (
                d => d.TimeOfDay == TimeSpan.Zero,
                new ArgumentException("Should be at midnight")
            );

            return precondition;
        }
    }
}
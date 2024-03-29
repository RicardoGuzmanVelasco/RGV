﻿using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void AtMidnight(this Contract<DateTime> contract)
        {
            contract.Evaluate(d => d.TimeOfDay == TimeSpan.Zero);
        }
    }
}
﻿using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Null<T>(this Contract<T> precondition)
            where T : class
        {
            precondition.Evaluate<ArgumentNullException>
            (
                target => target is null
            );
        }
    }
}
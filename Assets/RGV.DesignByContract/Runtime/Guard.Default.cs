﻿using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class GuardExtensions
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static T Default<T>(this Precondition<T> precondition)
            where T : struct
        {
            precondition.Evaluate<ArgumentException>
            (
                target => Equals(target, default(T))
            );

            return precondition;
        }
    }
}
using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Default<T>(this Precondition<T> precondition)
            where T : struct
        {
            precondition.Evaluate<ArgumentException>
            (
                target => Equals(target, default(T))
            );
        }
    }
}
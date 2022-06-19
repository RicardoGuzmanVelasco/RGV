using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Precondition<T> Null<T>(this Precondition<T> precondition)
            where T : class
        {
            precondition.Evaluate<ArgumentNullException>
            (
                target => target is null
            );

            return precondition;
        }
    }
}
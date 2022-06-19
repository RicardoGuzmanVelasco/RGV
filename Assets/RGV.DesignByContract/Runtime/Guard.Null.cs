using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class GuardExtensions
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static T Null<T>(this Precondition<T> precondition)
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
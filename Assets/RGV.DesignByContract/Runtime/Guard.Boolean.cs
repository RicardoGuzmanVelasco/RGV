using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class GuardExtensions
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void False(this Precondition<Func<bool>> precondition)
        {
            precondition.Not.True();
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void False(this Precondition<bool> precondition)
        {
            precondition.Not.True();
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void True(this Precondition<bool> precondition)
        {
            precondition.Evaluate<InvalidOperationException>
            (
                b => b
            );
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden, UsedImplicitly]
        public static void True(this Precondition<Func<bool>> precondition)
        {
            precondition.Evaluate<InvalidOperationException>
            (
                b => b.Invoke()
            );
        }
    }
}
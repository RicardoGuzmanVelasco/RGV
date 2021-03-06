using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Precondition<string> NullOrEmpty(this Precondition<string> precondition)
        {
            precondition.Evaluate<ArgumentException>
            (
                string.IsNullOrEmpty
            );

            return precondition;
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Precondition<string> NullOrWhiteSpace(this Precondition<string> precondition)
        {
            precondition.Evaluate<ArgumentException>
            (
                string.IsNullOrWhiteSpace
            );

            return precondition;
        }
    }
}
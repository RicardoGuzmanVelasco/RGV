using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class GuardExtensions
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static string NullOrEmpty(this Precondition<string> precondition)
        {
            precondition.Evaluate<ArgumentException>
            (
                string.IsNullOrEmpty
            );

            return precondition;
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static string NullOrWhiteSpace(this Precondition<string> precondition)
        {
            precondition.Evaluate<ArgumentException>
            (
                string.IsNullOrWhiteSpace
            );

            return precondition;
        }
    }
}
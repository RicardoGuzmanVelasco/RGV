using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void NullOrEmpty(this Contract<string> contract)
        {
            contract.Evaluate<ArgumentException>
            (
                string.IsNullOrEmpty
            );
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void NullOrWhiteSpace(this Contract<string> contract)
        {
            contract.Evaluate<ArgumentException>
            (
                string.IsNullOrWhiteSpace
            );
        }
    }
}
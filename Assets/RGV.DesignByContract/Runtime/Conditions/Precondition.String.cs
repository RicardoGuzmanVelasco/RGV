using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Contract<string> NullOrEmpty(this Contract<string> contract)
        {
            contract.Evaluate<ArgumentException>
            (
                string.IsNullOrEmpty
            );

            return contract;
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Contract<string> NullOrWhiteSpace(this Contract<string> contract)
        {
            contract.Evaluate<ArgumentException>
            (
                string.IsNullOrWhiteSpace
            );

            return contract;
        }
    }
}
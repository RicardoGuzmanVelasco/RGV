using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void False(this Contract<bool> contract)
        {
            contract.Not.True();
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void True(this Contract<bool> contract)
        {
            contract.Evaluate(b => b);
        }
    }
}
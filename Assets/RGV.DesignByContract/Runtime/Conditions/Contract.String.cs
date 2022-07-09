using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void NullOrEmpty(this Contract<string> contract)
        {
            contract.Evaluate(string.IsNullOrEmpty);
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void NullOrWhiteSpace(this Contract<string> contract)
        {
            contract.Evaluate(string.IsNullOrWhiteSpace);
        }
    }
}
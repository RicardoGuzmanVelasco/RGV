using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Default<T>(this Contract<T> contract, string reason = null)
            where T : struct
        {
            contract.Evaluate(target => Equals(target, default(T)));
        }
    }
}
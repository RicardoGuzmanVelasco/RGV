using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Default<T>(this Contract<T> contract)
            where T : struct
        {
            contract.Evaluate(o => Equals(o, default(T)));
        }
    }
}
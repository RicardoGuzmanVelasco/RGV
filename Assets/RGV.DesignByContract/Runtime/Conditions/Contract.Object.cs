using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Contract<object> Null(this Contract<object> contract)
        {
            contract.Evaluate(o => o is null);
            return contract;
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Contract<T> Default<T>(this Contract<T> contract)
            where T : struct
        {
            contract.Evaluate(o => Equals(o, default(T)));
            return contract;
        }
    }
}
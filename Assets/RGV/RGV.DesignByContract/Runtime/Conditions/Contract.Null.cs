using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Null<T>(this Contract<T> contract)
            where T : class
        {
            contract.Evaluate(target => target is null);
        }
    }
}
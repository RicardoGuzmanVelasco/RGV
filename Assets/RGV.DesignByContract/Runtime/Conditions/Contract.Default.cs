using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Default<T>(this Precondition<T> precondition)
            where T : struct
        {
            precondition.Evaluate(target => Equals(target, default(T)));
        }
    }
}
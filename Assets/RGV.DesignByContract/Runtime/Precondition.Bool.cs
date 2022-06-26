using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [Pure]
        public static Precondition<bool> Require(bool target)
        {
            return new Precondition<bool>(target);
        }

        [Pure]
        public static Precondition<bool> Require<T>(bool target)
            where T : Exception, new()
        {
            return new Precondition<bool>(target, new T());
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Precondition<bool> False(this Precondition<bool> precondition)
        {
            precondition.Not.True();
            return precondition;
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Precondition<bool> True(this Precondition<bool> precondition)
        {
            precondition.Evaluate<InvalidOperationException>
            (
                b => b
            );
            return precondition;
        }
    }
}
using System;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Guard
    {
        [Pure]
        public static Precondition<object> OnlyIf<T>(object target)
            where T : Exception, new()
        {
            return new Precondition<object>(target, new T());
        }

        [Pure]
        public static Precondition<object> OnlyIf(object target)
        {
            return new Precondition<object>(target);
        }
    }
}
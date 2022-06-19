using System;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [Pure]
        public static Precondition<object> Require<T>(object target)
            where T : Exception, new()
        {
            return new Precondition<object>(target, new T());
        }

        [Pure]
        public static Precondition<object> Require(object target)
        {
            return new Precondition<object>(target);
        }
    }
}
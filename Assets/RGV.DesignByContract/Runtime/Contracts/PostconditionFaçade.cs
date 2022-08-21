using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [Pure]
        public static Contract<object> Ensure(object target)
        {
            return new Postcondition<object>(target);
        }

        [Pure]
        public static Contract<bool> Ensure(bool target)
        {
            return new Postcondition<bool>(target);
        }

        [Pure]
        public static Contract<T> Ensure<T>(T target) where T : IComparable
        {
            return new Postcondition<T>(target);
        }

        [Pure]
        public static Contract<IEnumerable<T>> Ensure<T>(IEnumerable<T> target)
        {
            return new Postcondition<IEnumerable<T>>(target);
        }
    }
}
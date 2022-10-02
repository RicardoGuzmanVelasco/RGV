using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [Pure]
        public static Contract<object> Require(object target)
        {
            return new Precondition<object>(target);
        }

        [Pure]
        public static Contract<bool> Require(bool target)
        {
            return new Precondition<bool>(target);
        }

        [Pure]
        public static Contract<T> Require<T>(T target) where T : IComparable
        {
            return new Precondition<T>(target);
        }

        [Pure]
        public static Contract<IEnumerable<T>> Require<T>(IEnumerable<T> target)
        {
            return new Precondition<IEnumerable<T>>(target);
        }
    }
}
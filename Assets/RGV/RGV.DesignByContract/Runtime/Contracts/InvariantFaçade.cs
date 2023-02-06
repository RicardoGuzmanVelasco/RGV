using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [Pure]
        public static Contract<object> Invariant(object target)
        {
            return new Invariant<object>(target);
        }

        [Pure]
        public static Contract<bool> Invariant(bool target)
        {
            return new Invariant<bool>(target);
        }

        [Pure]
        public static Contract<T> Invariant<T>(T target) where T : IComparable
        {
            return new Invariant<T>(target);
        }

        [Pure]
        public static Contract<IEnumerable<T>> Invariant<T>(IEnumerable<T> target)
        {
            return new Invariant<IEnumerable<T>>(target);
        }
    }
}
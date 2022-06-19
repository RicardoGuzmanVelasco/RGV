using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class GuardExtensions
    {
        public static IEnumerable<T> Contains<T>(this Precondition<IEnumerable<T>> precondition, params T[] others)
        {
            precondition.Evaluate<ArgumentException>
            (
                c => others.All(c.Contains)
            );

            return precondition.target;
        }
    }

    public static partial class Precondition
    {
        [Pure]
        public static Precondition<IEnumerable<T>> Require<T>(IEnumerable<T> target)
        {
            return new Precondition<IEnumerable<T>>(target);
        }

        [Pure]
        public static Precondition<IEnumerable<T>> Require<T, TExc>(IEnumerable<T> target)
            where TExc : Exception, new()
        {
            return new Precondition<IEnumerable<T>>(target, new TExc());
        }
    }
}
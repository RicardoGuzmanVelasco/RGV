using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [Pure]
        public static Contract<IEnumerable<T>> Require<T>(IEnumerable<T> target)
        {
            return new Contract<IEnumerable<T>>(target);
        }

        public static Contract<IEnumerable<T>> Contains<T>(this Contract<IEnumerable<T>> contract,
            params T[] others)
        {
            contract.Evaluate<ArgumentException>
            (
                c => others.All(c.Contains)
            );

            return contract;
        }
    }
}
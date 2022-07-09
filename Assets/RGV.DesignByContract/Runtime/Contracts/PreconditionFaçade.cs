﻿using System;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [Pure]
        public static Contract<object> Require(object target)
        {
            return PreconditionFor(target);
        }

        [Pure]
        public static Contract<bool> Require(bool target)
        {
            return new Contract<bool>(target);
        }

        [Pure]
        public static Contract<T> Require<T>(T target) where T : IComparable
        {
            return PreconditionFor(target);
        }
    }
}
using System;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [Pure]
        public static Precondition<Func<bool>> Require(Func<bool> target)
        {
            return new Precondition<Func<bool>>(target);
        }

        [Pure]
        public static Precondition<Func<bool>> Require<T>(Func<bool> target)
            where T : Exception, new()
        {
            return new Precondition<Func<bool>>(target, new T());
        }

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
    }
}
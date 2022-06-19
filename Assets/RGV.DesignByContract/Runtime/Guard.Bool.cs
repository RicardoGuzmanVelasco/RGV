using System;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Guard
    {
        [Pure]
        public static Precondition<Func<bool>> OnlyIf(Func<bool> target)
        {
            return new Precondition<Func<bool>>(target);
        }

        [Pure]
        public static Precondition<Func<bool>> OnlyIf<T>(Func<bool> target)
            where T : Exception, new()
        {
            return new Precondition<Func<bool>>(target, new T());
        }

        [Pure]
        public static Precondition<bool> OnlyIf(bool target)
        {
            return new Precondition<bool>(target);
        }

        [Pure]
        public static Precondition<bool> OnlyIf<T>(bool target)
            where T : Exception, new()
        {
            return new Precondition<bool>(target, new T());
        }
    }
}
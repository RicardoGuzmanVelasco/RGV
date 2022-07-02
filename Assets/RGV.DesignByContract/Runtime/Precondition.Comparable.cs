using System;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        [Pure]
        public static Precondition<T> Require<T>(T target) where T : IComparable
        {
            return new Precondition<T>(target);
        }

        [Pure]
        public static Precondition<int> Require<T>(int target)
            where T : Exception, new()
        {
            return new Precondition<int>(target, new T());
        }

        public static Precondition<T> GreaterThan<T>(this Precondition<T> precondition, T other)
            where T : IComparable<T>, IComparable
        {
            precondition.Evaluate<ArgumentOutOfRangeException>
            (
                c => c.CompareTo(other) > 0
            );

            return precondition;
        }

        public static Precondition<T> LesserThan<T>(this Precondition<T> precondition, T other)
            where T : IComparable<T>, IComparable
        {
            precondition.Evaluate<ArgumentOutOfRangeException>
            (
                c => c.CompareTo(other) < 0
            );

            return precondition;
        }

        public static Precondition<T> GreaterOrEqualThan<T>(this Precondition<T> precondition, T other)
            where T : IComparable<T>, IComparable
        {
            precondition.Not.LesserThan(other);

            return precondition;
        }

        public static Precondition<T> LesserOrEqualThan<T>(this Precondition<T> precondition, T other)
            where T : IComparable<T>, IComparable
        {
            precondition.Not.GreaterThan(other);

            return precondition;
        }

        public static Precondition<T> Between<T>(this Precondition<T> precondition, T min, T max)
            where T : IComparable<T>, IComparable
        {
            precondition.GreaterOrEqualThan(min);
            precondition.LesserOrEqualThan(max);

            return precondition;
        }

        public static Precondition<int> Negative(this Precondition<int> precondition)
        {
            precondition.Not.GreaterOrEqualThan(0);

            return precondition;
        }

        public static Precondition<float> Negative(this Precondition<float> precondition)
        {
            precondition.Not.GreaterOrEqualThan(0);

            return precondition;
        }

        public static Precondition<int> Positive(this Precondition<int> precondition)
        {
            precondition.Not.LesserOrEqualThan(0);

            return precondition;
        }

        public static Precondition<float> Positive(this Precondition<float> precondition)
        {
            precondition.Not.LesserOrEqualThan(0);

            return precondition;
        }
    }
}
using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        public static Contract<T> GreaterThan<T>(this Contract<T> contract, T other)
            where T : IComparable<T>, IComparable
        {
            contract.Evaluate<ArgumentOutOfRangeException>
            (
                c => c.CompareTo(other) > 0
            );

            return contract;
        }

        public static Contract<T> LesserThan<T>(this Contract<T> contract, T other)
            where T : IComparable<T>, IComparable
        {
            contract.Evaluate<ArgumentOutOfRangeException>
            (
                c => c.CompareTo(other) < 0
            );

            return contract;
        }

        public static Contract<T> GreaterOrEqualThan<T>(this Contract<T> contract, T other)
            where T : IComparable<T>, IComparable
        {
            contract.Not.LesserThan(other);

            return contract;
        }

        public static Contract<T> LesserOrEqualThan<T>(this Contract<T> contract, T other)
            where T : IComparable<T>, IComparable
        {
            contract.Not.GreaterThan(other);

            return contract;
        }

        public static Contract<T> Between<T>(this Contract<T> contract, T min, T max)
            where T : IComparable<T>, IComparable
        {
            contract.GreaterOrEqualThan(min);
            contract.LesserOrEqualThan(max);

            return contract;
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Zero(this Contract<int> contract)
        {
            contract.Evaluate<ArgumentException>(i => i == 0);
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void ApproxZero(this Contract<float> contract, float error = float.Epsilon)
        {
            contract.Evaluate<ArgumentException>(f => Math.Abs(f) <= error);
        }

        public static Contract<int> Negative(this Contract<int> contract)
        {
            contract.Not.GreaterOrEqualThan(0);

            return contract;
        }

        public static Contract<float> Negative(this Contract<float> contract)
        {
            contract.Not.GreaterOrEqualThan(0);

            return contract;
        }

        public static Contract<int> Positive(this Contract<int> contract)
        {
            contract.Not.LesserOrEqualThan(0);

            return contract;
        }

        public static Contract<float> Positive(this Contract<float> contract)
        {
            contract.Not.LesserOrEqualThan(0);

            return contract;
        }
    }
}
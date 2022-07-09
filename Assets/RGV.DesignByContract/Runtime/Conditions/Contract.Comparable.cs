using System;
using System.Diagnostics;
using JetBrains.Annotations;
using static System.Math;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        public static void GreaterThan<T>(this Contract<T> contract, T other)
            where T : IComparable<T>, IComparable
        {
            contract.Evaluate<ArgumentOutOfRangeException>
            (
                c => c.CompareTo(other) > 0
            );
        }

        public static void LesserThan<T>(this Contract<T> contract, T other)
            where T : IComparable<T>, IComparable
        {
            contract.Evaluate<ArgumentOutOfRangeException>
            (
                c => c.CompareTo(other) < 0
            );
        }

        public static void GreaterOrEqualThan<T>(this Contract<T> contract, T other)
            where T : IComparable<T>, IComparable
        {
            contract.Not.LesserThan(other);
        }

        public static void LesserOrEqualThan<T>(this Contract<T> contract, T other)
            where T : IComparable<T>, IComparable
        {
            contract.Not.GreaterThan(other);
        }

        public static void Between<T>(this Contract<T> contract, T min, T max)
            where T : IComparable<T>, IComparable
        {
            contract.GreaterOrEqualThan(min);
            contract.LesserOrEqualThan(max);
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Zero(this Contract<int> contract)
        {
            contract.Evaluate<ArgumentException>(i => i == 0);
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void ApproxZero(this Contract<float> contract, float error = float.Epsilon)
        {
            contract.Evaluate<ArgumentException>(f => Abs(f) <= error);
        }

        public static void Negative(this Contract<int> contract)
        {
            contract.Not.GreaterOrEqualThan(0);
        }

        public static void Negative(this Contract<float> contract)
        {
            contract.Not.GreaterOrEqualThan(0);
        }

        public static void Positive(this Contract<int> contract)
        {
            contract.Not.LesserOrEqualThan(0);
        }

        public static void Positive(this Contract<float> contract)
        {
            contract.Not.LesserOrEqualThan(0);
        }
    }
}
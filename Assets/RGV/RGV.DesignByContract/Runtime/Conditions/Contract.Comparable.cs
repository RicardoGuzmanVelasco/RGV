using System;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        public static void GreaterOrEqualThan<T>(this Contract<T> contract, T other, string reason = null)
            where T : IComparable<T>, IComparable
        {
            contract.Not.LesserThan(other);
        }

        public static void LesserOrEqualThan<T>(this Contract<T> contract, T other, string reason = null)
            where T : IComparable<T>, IComparable
        {
            contract.Not.GreaterThan(other);
        }

        public static void Between<T>(this Contract<T> contract, T min, T max, string reason = null)
            where T : IComparable<T>, IComparable
        {
            contract.GreaterOrEqualThan(min);
            contract.LesserOrEqualThan(max);
        }

        public static void GreaterThan<T>(this Contract<T> contract, T other, string reason = null)
            where T : IComparable<T>, IComparable
        {
            contract.Evaluate(c => c.CompareTo(other) > 0);
        }

        public static void LesserThan<T>(this Contract<T> contract, T other, string reason = null)
            where T : IComparable<T>, IComparable
        {
            contract.Evaluate(c => c.CompareTo(other) < 0);
        }
    }
}
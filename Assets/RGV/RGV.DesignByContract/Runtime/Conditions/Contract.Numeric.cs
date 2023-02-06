using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
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

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void Zero(this Contract<int> contract)
        {
            contract.Evaluate(i => i == 0);
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static void ApproxZero(this Contract<float> contract, float error = float.Epsilon)
        {
            contract.Evaluate(f => Math.Abs(f) <= error);
        }
    }
}
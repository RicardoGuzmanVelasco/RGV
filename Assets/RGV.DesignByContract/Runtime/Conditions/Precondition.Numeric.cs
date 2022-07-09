using System;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Precondition
    {
        public static Precondition<int> Zero(this Precondition<int> precondition)
        {
            precondition.Evaluate<ArgumentException>(i => i == 0);

            return precondition;
        }

        public static Precondition<float> AproxZero(this Precondition<float> precondition, float error = float.Epsilon)
        {
            precondition.Evaluate<ArgumentException>(f => Math.Abs(f) <= error);

            return precondition;
        }
    }
}
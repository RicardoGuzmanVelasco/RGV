using System;

namespace RGV.DesignByContract.Runtime
{
    public sealed class Precondition<T> : Contract<T>
    {
        internal Precondition(T evaluee) : base(evaluee) { }

        protected override Exception Throw { get; init; } = new ArgumentException("Precondition failed");
    }
}
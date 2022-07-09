using System;

namespace RGV.DesignByContract.Runtime
{
    class Invariant<T> : Contract<T>
    {
        public Invariant(T evaluee) : base(evaluee) { }

        protected override Exception Throw { get; init; } = new InvalidOperationException("Invariant failed");
    }
}
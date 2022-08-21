using System;
using System.Diagnostics;

namespace RGV.DesignByContract.Runtime
{
    internal class Invariant<T> : Contract<T>
    {
        public Invariant(T evaluee) : base(evaluee) { }

        protected override Exception Throw { get; init; } = new InvalidOperationException("Invariant failed");

        [DebuggerHidden]
        protected override Contract<T> CloneThisNegated()
        {
            return new Invariant<T>(evaluee)
            {
                negated = !negated,
                Throw = Throw
            };
        }
    }
}
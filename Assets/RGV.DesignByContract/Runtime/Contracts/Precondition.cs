using System;
using System.Diagnostics;

namespace RGV.DesignByContract.Runtime
{
    public sealed class Precondition<T> : Contract<T>
    {
        internal Precondition(T evaluee) : base(evaluee) { }

        protected override Exception Throw { get; init; } = new ArgumentException("Precondition failed");


        [DebuggerHidden]
        protected override Contract<T> CloneThisNegated()
        {
            return new Precondition<T>(evaluee)
            {
                negated = !negated,
                Throw = Throw
            };
        }
    }
}
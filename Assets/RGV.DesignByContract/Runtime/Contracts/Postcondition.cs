using System;
using System.Diagnostics;

namespace RGV.DesignByContract.Runtime
{
    internal class Postcondition<T> : Contract<T>
    {
        public Postcondition(T evaluee) : base(evaluee) { }

        protected override Exception Throw { get; init; } = new ApplicationException("Postcondition failed");

        [DebuggerHidden]
        protected override Contract<T> CloneThisNegated()
        {
            return new Postcondition<T>(evaluee)
            {
                negated = !negated,
                Throw = Throw
            };
        }
    }
}
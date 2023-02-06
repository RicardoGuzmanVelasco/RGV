using System;
using System.Diagnostics;

namespace RGV.DesignByContract.Runtime
{
    public sealed class Precondition<T> : Contract<T>
    {
        internal Precondition(T evaluee) : base(evaluee) { }

        protected override Exception ReportContractFailure(string msg)
        {
            return new ArgumentException(msg);
        }

        [DebuggerHidden]
        protected override Contract<T> CloneThisNegated()
        {
            return new Precondition<T>(evaluee)
            {
                IsNegated = !IsNegated
            };
        }
    }
}
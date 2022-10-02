using System;
using System.Diagnostics;

namespace RGV.DesignByContract.Runtime
{
    internal class Postcondition<T> : Contract<T>
    {
        public Postcondition(T evaluee) : base(evaluee) { }

        protected override Exception ReportContractFailure(string msg)
        {
            return new ApplicationException(msg);
        }

        [DebuggerHidden]
        protected override Contract<T> CloneThisNegated()
        {
            return new Postcondition<T>(evaluee)
            {
                IsNegated = !IsNegated
            };
        }
    }
}
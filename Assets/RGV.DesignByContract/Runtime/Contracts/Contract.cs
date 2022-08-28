using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    [DebuggerStepThrough]
    public abstract class Contract<T>
    {
        protected readonly T evaluee;
        protected bool negated;

        [DebuggerHidden]
        protected Contract(T evaluee)
        {
            this.evaluee = evaluee;
        }

        [DebuggerHidden] public Contract<T> Not => CloneThisNegated();

        [DebuggerHidden, Conditional("UNITY_ASSERTIONS")]
        public void Evaluate(Func<T, bool> predicate, string msg = null)
        {
            if(predicate is null)
                throw new ArgumentNullException(nameof(predicate), "Cannot evaluate a null predicate");

            if(!Satisfy(predicate))
                throw ReportContractFailure(FormatMsg(msg));
        }

        #region Support methods
        protected abstract Exception ReportContractFailure(string msg);

        string FormatMsg(string msg)
        {
            var prettyPrint = GetType().Name.Remove(GetType().Name.Length - 2);
            var header = $"{prettyPrint} failed.";

            var reason = msg is null
                ? string.Empty
                : $" {msg} (found {evaluee}).";

            return header + reason;
        }

        /// This is to avoid aliasing with negated state. 
        [DebuggerHidden]
        protected abstract Contract<T> CloneThisNegated();

        [DebuggerHidden]
        internal bool Satisfy([NotNull] Func<T, bool> predicate)
        {
            return predicate.Invoke(evaluee) ^ negated;
        }
        #endregion
    }
}
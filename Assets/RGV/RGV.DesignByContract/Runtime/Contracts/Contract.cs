using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    [DebuggerStepThrough]
    public abstract class Contract<T>
    {
        protected readonly T evaluee;

        [DebuggerHidden]
        protected Contract(T evaluee)
        {
            this.evaluee = evaluee;
        }

        internal bool IsNegated { get; init; }

        [DebuggerHidden] public Contract<T> Not => CloneThisNegated();

        [DebuggerHidden, Conditional("UNITY_ASSERTIONS")]
        public void Evaluate(Func<T, bool> predicate)
        {
            if(predicate is null)
                throw new ArgumentNullException(nameof(predicate), "Cannot evaluate a null predicate");

            if(!Satisfy(predicate))
                throw ReportContractFailure(FormatMsg());
        }

        #region Support methods
        protected abstract Exception ReportContractFailure(string msg);

        string FormatMsg()
        {
            return $"[{GetType().Name[..^2]} failed]";
        }

        public string MsgBody(string reason)
        {
            return $"(Reason: {(IsNegated ? " NOT" : string.Empty)} {reason}, found {evaluee})";
        }

        /// This is to avoid aliasing with negated state. 
        [DebuggerHidden]
        protected abstract Contract<T> CloneThisNegated();

        [DebuggerHidden]
        internal bool Satisfy([NotNull] Func<T, bool> predicate)
        {
            return predicate.Invoke(evaluee) ^ IsNegated;
        }
        #endregion
    }
}
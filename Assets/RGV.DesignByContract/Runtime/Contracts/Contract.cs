using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public class Contract<T>
    {
        readonly T evaluee;
        bool negated;

        [DebuggerHidden]
        protected Contract(T evaluee)
        {
            this.evaluee = evaluee;
        }

        [DebuggerHidden] public Contract<T> Not => CloneThisNegated();
        [DebuggerHidden] protected virtual Exception Throw { get; init; }

        [DebuggerHidden]
        public void Evaluate(Func<T, bool> predicate, Func<T, Exception> eFunc = null)
        {
            if(!Contract.ContractsEnabled)
                return;

            if(predicate is null)
                throw new ArgumentNullException(nameof(predicate), "Cannot evaluate a null predicate");

            if(!Satisfy(predicate))
                throw new AggregateException
                (
                    "Contract not satisfied",
                    Throw, eFunc?.Invoke(evaluee)
                );
        }

        #region Support methods
        /// This is to avoid aliasing with negated state. 
        [DebuggerHidden]
        Contract<T> CloneThisNegated()
        {
            return new Contract<T>(evaluee)
            {
                negated = !negated,
                Throw = Throw
            };
        }

        [DebuggerHidden]
        internal bool Satisfy([NotNull] Func<T, bool> predicate)
        {
            return predicate.Invoke(evaluee) ^ negated;
        }
        #endregion
    }
}
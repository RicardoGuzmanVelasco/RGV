using System;

namespace RGV.DesignByContract.Runtime
{
    public class Precondition<T> : Contract<T>
    {
        readonly T evaluee;
        bool negated;

        Exception Throw { get; }

        public Precondition<T> Not => CloneThisPreconditionNegated();

        public void Evaluate<TExc>(Func<T, bool> predicate)
            where TExc : Exception, new()
        {
            Evaluate(predicate, new TExc());
        }

        public void Evaluate(Func<T, bool> predicate, Exception e = null)
        {
            if(predicate is null)
                throw new ArgumentNullException(nameof(predicate), "Cannot evaluate a null predicate");

            if(!Satisfy(predicate))
                throw Throw ?? e ?? new Exception("Precondition not satisfied");
        }

        #region Ctors
        public Precondition(T evaluee) : this(evaluee, null) { }

        public Precondition(T evaluee, Exception exception)
            : base(evaluee)
        {
            this.evaluee = evaluee;
            Throw = exception;
        }
        #endregion

        #region Support methods
        /// <remarks>
        /// This is to avoid aliasing with negated state. 
        /// </remarks>
        Precondition<T> CloneThisPreconditionNegated()
        {
            return new Precondition<T>(evaluee, Throw)
            {
                negated = !negated
            };
        }

        internal bool Satisfy(Func<T, bool> predicate)
        {
            return predicate.Invoke(evaluee) ^ negated;
        }
        #endregion
    }
}
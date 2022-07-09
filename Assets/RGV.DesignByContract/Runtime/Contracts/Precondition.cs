using System;

namespace RGV.DesignByContract.Runtime
{
    public sealed class Precondition<T> : Contract<T>
    {
        internal Precondition(T evaluee, Exception exception = null) : base(evaluee)
        {
            if(exception is not null)
                Throw = exception;
        }

        protected override Exception Throw { get; init; } = new ArgumentException("Precondition failed");
    }
}
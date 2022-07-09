using System;

namespace RGV.DesignByContract.Runtime
{
    class Postcondition<T> : Contract<T>
    {
        public Postcondition(T evaluee) : base(evaluee) { }

        protected override Exception Throw { get; init; } = new ApplicationException("Postcondition failed");
    }
}
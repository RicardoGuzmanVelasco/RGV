using System;

namespace RGV.DesignByContract.Runtime
{
    public class Contract<T>
    {
        public Contract(T evaluee)
        {
            throw new NotImplementedException();
        }
    }

    class Postcondition<T> : Contract<T>
    {
        public Postcondition(T evaluee) : base(evaluee) { }
    }

    class Invariant<T> : Contract<T>
    {
        public Invariant(T evaluee) : base(evaluee) { }
    }
}
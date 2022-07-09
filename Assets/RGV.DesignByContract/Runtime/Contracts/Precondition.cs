using System;

namespace RGV.DesignByContract.Runtime
{
    public class Precondition<T> : Contract<T>
    {
        protected internal Precondition(T evaluee, Exception exception = null) : base(evaluee) { }
    }
}
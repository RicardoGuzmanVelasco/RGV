namespace RGV.DesignByContract.Runtime
{
    class Invariant<T> : Contract<T>
    {
        public Invariant(T evaluee) : base(evaluee) { }
    }
}
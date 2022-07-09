namespace RGV.DesignByContract.Runtime
{
    class Postcondition<T> : Contract<T>
    {
        public Postcondition(T evaluee) : base(evaluee) { }
    }
}
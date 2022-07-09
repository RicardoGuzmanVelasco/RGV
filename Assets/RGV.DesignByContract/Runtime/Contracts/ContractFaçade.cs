namespace RGV.DesignByContract.Runtime
{
    public class Contract
    {
        public static bool ContractsEnabled { get; set; } = true;

        internal static Contract<T> PreconditionFor<T>(T evaluee)
        {
            return new Precondition<T>(evaluee);
        }

        internal static Contract<T> InvariantFor<T>(T evaluee)
        {
            return new Invariant<T>(evaluee);
        }

        internal static Contract<T> PostconditionFor<T>(T evaluee)
        {
            return new Postcondition<T>(evaluee);
        }

        internal static Contract<T> For<T>(T evaluee)
        {
            return new Contract<T>(evaluee);
        }
    }
}
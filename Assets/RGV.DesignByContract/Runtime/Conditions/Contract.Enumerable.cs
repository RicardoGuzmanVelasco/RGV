using System.Collections.Generic;
using System.Linq;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        public static void Contains<T>(this Contract<IEnumerable<T>> contract,
            params T[] others)
        {
            contract.Evaluate(c => others.All(c.Contains));
        }
    }
}
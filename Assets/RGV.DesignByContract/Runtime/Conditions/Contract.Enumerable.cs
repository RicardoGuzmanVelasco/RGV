using System.Collections.Generic;
using System.Linq;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        public static void ContainsAll<T>(this Contract<IEnumerable<T>> contract, params T[] elements)
        {
            foreach(var e in elements)
                contract.Contains(e);
        }

        public static void Contains<T>(this Contract<IEnumerable<T>> contract, T element, string reason = null)
        {
            contract.Evaluate(c => c.Contains(element));
        }

        public static void Empty<T>(this Contract<IEnumerable<T>> contract, string reason = null)
        {
            contract.Evaluate(c => !c.Any());
        }
    }
}
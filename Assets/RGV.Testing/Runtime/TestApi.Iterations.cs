using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RGV.Testing.Runtime
{
    public static partial class TestApi
    {
        public record Iterations(int Value) : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                return Enumerable.Range(1, Value).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
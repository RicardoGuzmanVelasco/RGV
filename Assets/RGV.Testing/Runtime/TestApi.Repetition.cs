using System;
using System.Collections.Generic;
using System.Linq;

namespace RGV.Testing.Runtime
{
    public static partial class TestApi
    {
        public record ActionRepetition(Action Act)
        {
            public void For(Iterations iterations)
            {
                foreach(var _ in iterations)
                    Act.Invoke();
            }
        }

        public record FuncRepetition<T>(Func<T> Act)
        {
            public IEnumerable<T> For(Iterations iterations)
            {
                return iterations.Select(_ => Act.Invoke());
            }
        }
    }
}
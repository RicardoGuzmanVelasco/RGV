using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace RGV.Testing.Runtime
{
    public static partial class TestApi
    {
        /// He tenido que separar el nombre porque si no siempre pillaba el de Func<T> y no mutaba valores dentro.
        /// Se puede renombrar este al mismo nombre que el otro y ver cómo falla un test para entenderlo mejor. 
        [Pure, NotNull]
        public static ActionRepetition Repeat([NotNull] Action act)
        {
            return new ActionRepetition(act);
        }

        [Pure, NotNull]
        public static FuncRepetition<T> RepeatFunc<T>(Func<T> act)
        {
            return new FuncRepetition<T>(act);
        }

        public static Iterations Times(this int value)
        {
            Require(value).GreaterThan(1);
            return new Iterations(value);
        }

        #region Support structures
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
        #endregion
    }
}
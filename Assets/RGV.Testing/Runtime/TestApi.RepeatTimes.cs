using System;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace RGV.Testing.Runtime
{
    public static partial class TestApi
    {
        /// He tenido que separar el nombre porque si no siempre pillaba el de Func<T> y no mutaba valores dentro.
        /// Se puede renombrar este al mismo nombre que el otro y ver cómo falla un test para entenderlo mejor. 
        [Pure, NotNull]
        public static ActionRepetition RepeatAct([NotNull] Action act)
        {
            return new ActionRepetition(act);
        }

        [Pure, NotNull]
        public static FuncRepetition<T> Repeat<T>(Func<T> act)
        {
            return new FuncRepetition<T>(act);
        }

        public static Iterations Times(this int value)
        {
            Require(value).GreaterThan(1);
            return new Iterations(value);
        }
    }
}
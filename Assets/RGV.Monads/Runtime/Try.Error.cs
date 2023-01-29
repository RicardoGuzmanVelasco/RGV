using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using RGV.DesignByContract.Runtime;

namespace RGV.Monads.Runtime
{
    public partial class Try
    {
        public record Error
        {
            [UsedImplicitly] readonly string errorId;

            public Error OrIfOk(Task<Error> command)
            {
                return this == None
                    ? command.Result
                    : this;
            }

            public void IfOk(Action operation)
            {
                if(this == None)
                    operation.Invoke();
            }

            public override string ToString()
            {
                return this == None
                    ? "Error.None"
                    : errorId;
            }

            #region Ctors / Factory methods
            public Error(string errorId)
            {
                Contract.Require(errorId).Not.NullOrEmpty();
                this.errorId = errorId;
            }

            Error() { }

            internal static Error None { get; } = new();
            #endregion
        }
    }
}
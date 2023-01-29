using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RGV.Monads.Runtime
{
    public partial class Try
    {
        readonly Task<Error> command;

        Try(Task<Error> operation)
        {
            command = operation;
        }

        public static Try To(Func<Task<Error>> operation)
        {
            return new Try(operation.Invoke());
        }

        public static Try To(Task<Error> operation)
        {
            return new Try(operation);
        }

        public Try Then(Func<Task<Error>> operation)
        {
            var chained = command.ContinueWith
            (
                t => t.Result != Error.None
                    ? t.Result
                    : operation.Invoke().Result
            );

            return new Try(chained);
        }

        public Try Then(Task<Error> operation)
        {
            return new Try(command.ContinueWith(t => t.Result.OrIfOk(operation)));
        }

        public TaskAwaiter<Error> GetAwaiter()
        {
            return command.GetAwaiter();
        }

        #region Factory methods
        public static Task<Error> FromOk(Action action)
        {
            action?.Invoke();
            return FromOk();
        }

        public static Task<Error> FromOk() => Task.FromResult(Error.None);

        public static Task<Error> FromError(string id) => FromError(new Error(id));
        public static Task<Error> FromError(Error e) => Task.FromResult(e);
        #endregion

        #region Matching
        public Task IfOk(Action operation)
        {
            return command.ContinueWith(t => t.Result.IfOk(operation));
        }

        public Task IfError(Action<Error> operation)
        {
            return Resolve(null, operation);
        }

        public Task IfError(Action operation)
        {
            return Resolve(null, _ => operation.Invoke());
        }

        public Task Resolve(Action ok, Action<Error> error)
        {
            return command.ContinueWith(t =>
            {
                if(t.Result == Error.None)
                    ok?.Invoke();
                else
                    error?.Invoke(t.Result);
            });
        }
        #endregion
    }
}
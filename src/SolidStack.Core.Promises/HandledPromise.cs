using System;
using System.Threading.Tasks;

namespace SolidStack.Core.Promises
{
    public class HandledPromise<TSuccess>
    {
        public HandledPromise(Func<TSuccess> runner) :
            this(() => Task.FromResult(runner()))
        {
        }

        public HandledPromise(Func<Task<TSuccess>> runner) =>
            Runner = runner;

        private Func<Task<TSuccess>> Runner { get; }

        public Task<TSuccess> RunAsync() =>
            Runner();
    }
}
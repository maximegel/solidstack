using System.Threading.Tasks;

namespace SolidStack.Core.Promises
{
    public static class Promise
    {
        public static Promise<TError, TSuccess> Create<TError, TSuccess>(
            PromiseFunc<TError, TSuccess> factory) =>
            new LazyPromise<TError, TSuccess>(() =>
                Task.FromResult(
                    factory(error => (Promise<TError, TSuccess>) error,
                        success => (Promise<TError, TSuccess>) success)));

        public static Promise<TError, TSuccess> Create<TError, TSuccess>(
            PromiseAsyncFunc<TError, TSuccess> factory) =>
            new LazyPromise<TError, TSuccess>(() =>
                factory(error => (Promise<TError, TSuccess>) error, success => (Promise<TError, TSuccess>) success));
    }
}
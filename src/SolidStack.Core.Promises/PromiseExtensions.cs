using System;
using System.Threading.Tasks;

namespace SolidStack.Core.Promises
{
    public static class PromiseExtensions
    {
        public static Promise<TError, TSuccess> Catch<TError, TSuccess>(
            this Promise<TError, TSuccess> promise,
            Action action)
        {
            switch (promise)
            {
                case RejectedPromise<TError, TSuccess> _:
                case DefaultRejectedPromise _:
                    action();
                    return promise;
                default:
                    return promise;
            }
        }

        public static Promise<TError, TSuccess> Catch<TError, TSuccess>(
            this Promise<TError, TSuccess> promise,
            Action<TError> action) =>
            Catch(promise, action, _ => true);

        public static Promise<TError, TSuccess> Catch<TError, TSuccess>(
            this Promise<TError, TSuccess> promise,
            Action<TError> action,
            Func<TError, bool> predicate)
        {
            switch (promise)
            {
                case RejectedPromise<TError, TSuccess> error when predicate(error):
                    action(error);
                    return promise;
                default:
                    return promise;
            }
        }

        public static HandledPromise<TSuccess> Catch<TError, TSuccess>(
            this Promise<TError, TSuccess> promise,
            Func<TSuccess> handler)
        {
            switch (promise)
            {
                case LazyPromise<TError, TSuccess> lazyPromise:
                    return new HandledPromise<TSuccess>(async () => await (await lazyPromise.RunAsync()).Catch(handler).RunAsync());
                case ResolvedPromise<TError, TSuccess> success:
                    return new HandledPromise<TSuccess>(() => success);
                case RejectedPromise<TError, TSuccess> _:
                case DefaultRejectedPromise _:
                    return new HandledPromise<TSuccess>(handler);
                default:
                    throw new InvalidOperationException();
            }
        }

        public static Promise<TError, TSuccess> Catch<TError, TSuccess>(
            this Promise<TError, TSuccess> promise,
            Func<TError, TSuccess> handler) =>
            Catch(promise, handler, _ => true);

        public static Promise<TError, TSuccess> Catch<TError, TSuccess>(
            this Promise<TError, TSuccess> promise,
            Func<TError, TSuccess> handler,
            Func<TError, bool> predicate)
        {
            switch (promise)
            {
                case RejectedPromise<TError, TSuccess> error when predicate(error):
                    return handler(error);
                default:
                    return promise;
            }
        }

        public static Promise<TError, TNewSuccess> Then<TError, TSuccess, TNewSuccess>(
            this Promise<TError, TSuccess> promise,
            Func<TSuccess, TNewSuccess> function) =>
            Then(promise, success => (Promise<TError, TNewSuccess>)function(success));

        public static Promise<TError, TNewSuccess> Then<TError, TSuccess, TNewSuccess>(
            this Promise<TError, TSuccess> promise,
            Func<TSuccess, Promise<TError, TNewSuccess>> function)
        {
            switch (promise)
            {
                case LazyPromise<TError, TSuccess> lazyPromise:
                    return lazyPromise.Map(innerPromise => Task.FromResult(innerPromise.Then(function)));
                case ResolvedPromise<TError, TSuccess> success:
                    return function(success);
                case RejectedPromise<TError, TSuccess> error:
                    return (TError) error;
                case DefaultRejectedPromise error:
                    return error;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
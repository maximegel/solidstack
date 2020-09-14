using System;
using System.Threading.Tasks;

namespace SolidStack.Core.Promises
{
    public delegate Task<Promise<TError, TSuccess>> PromiseAsyncFunc<TError, TSuccess>(
        Func<TError, Promise<TError, TSuccess>> rejecter,
        Func<TSuccess, Promise<TError, TSuccess>> resolver);
}
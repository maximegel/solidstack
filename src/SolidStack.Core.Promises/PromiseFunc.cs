using System;

namespace SolidStack.Core.Promises
{
    public delegate Promise<TError, TSuccess> PromiseFunc<TError, TSuccess>(
        Func<TError, Promise<TError, TSuccess>> rejecter,
        Func<TSuccess, Promise<TError, TSuccess>> resolver);
}
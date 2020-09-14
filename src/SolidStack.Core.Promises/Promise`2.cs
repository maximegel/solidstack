namespace SolidStack.Core.Promises
{
    public abstract class Promise<TError, TSuccess>
    {
        public static implicit operator Promise<TError, TSuccess>(TSuccess success) =>
            new ResolvedPromise<TError, TSuccess>(success);

        public static implicit operator Promise<TError, TSuccess>(TError error) =>
            new RejectedPromise<TError, TSuccess>(error);

        public static implicit operator Promise<TError, TSuccess>(DefaultRejectedPromise error) =>
            new DefaultRejectedPromise<TError, TSuccess>();
    }
}
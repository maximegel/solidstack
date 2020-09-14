namespace SolidStack.Core.Promises
{
    public class ResolvedPromise<TError, TSuccess> : Promise<TError, TSuccess>
    {
        public ResolvedPromise(TSuccess content) =>
            Content = content;

        private TSuccess Content { get; }

        public static implicit operator TSuccess(ResolvedPromise<TError, TSuccess> value) =>
            value.Content;
    }
}
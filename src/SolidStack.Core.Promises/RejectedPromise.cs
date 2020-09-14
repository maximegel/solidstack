namespace SolidStack.Core.Promises
{
    public class RejectedPromise<TError, TSuccess> : Promise<TError, TSuccess>
    {
        public RejectedPromise(TError content) =>
            Content = content;

        private TError Content { get; }

        public static implicit operator TError(RejectedPromise<TError, TSuccess> value) =>
            value.Content;
    }
}
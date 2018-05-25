namespace SolidStack.Core.Flow.Internal.Result
{
    public interface IFilteredError<TError, out TSuccess> :
        IFilteredActionableContent<TError, IActionableResult<TError, TSuccess>>,
        IFilteredMappableError<TError, TSuccess>
    {
    }
}
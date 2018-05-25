namespace SolidStack.Core.Flow.Internal.Result
{
    public interface IFilteredSuccess<TError, out TSuccess> :
        IFilteredActionableContent<TSuccess, IActionableResult<TError, TSuccess>>,
        IFilteredMappableSuccess<TError, TSuccess>
    {
    }
}
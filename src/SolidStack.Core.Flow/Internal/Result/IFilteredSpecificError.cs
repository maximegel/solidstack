namespace SolidStack.Core.Flow.Internal.Result
{
    public interface IFilteredSpecificError<out TSpecificError, TError, out TSuccess> :
        IFilteredActionableContent<TSpecificError, IActionableResult<TError, TSuccess>>,
        IFilteredMappableSpecificError<TSpecificError, TError, TSuccess>
        where TSpecificError : TError
    {
    }
}
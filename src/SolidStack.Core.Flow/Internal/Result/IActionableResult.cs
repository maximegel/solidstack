namespace SolidStack.Core.Flow.Internal.Result
{
    public interface IActionableResult<TError, out TSuccess> :
        IActionable
    {
        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when the result is an error and
        ///     that error is assignable to the given specific error type.
        /// </summary>
        /// <typeparam name="TSpecificError">The type of the specific error matched against the actual error.</typeparam>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the result value.</returns>
        IFilteredActionableContent<TSpecificError, IActionableResult<TError, TSuccess>> WhenError<TSpecificError>()
            where TSpecificError : TError;

        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when the result is an error.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the result value.</returns>
        IFilteredActionableContent<TError, IActionableResult<TError, TSuccess>> WhenError();

        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when the result is a success.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the result value.</returns>
        IFilteredActionableContent<TSuccess, IActionableResult<TError, TSuccess>> WhenSuccess();
    }
}
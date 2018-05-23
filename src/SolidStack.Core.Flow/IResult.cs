using SolidStack.Core.Flow.Internal.Result;

namespace SolidStack.Core.Flow
{
    /// <summary>
    ///     Represents the result of an operation that may end in a success or an error and provides methods for building and
    ///     then executing an easy-to-read linear flow around these two situations.
    /// </summary>
    /// <typeparam name="TError">The type of error that can be returned.</typeparam>
    /// <typeparam name="TSuccess">The type of success that can be returned.</typeparam>
    public interface IResult<TError, out TSuccess>
    {
        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when the result is an error and
        ///     that error is assignable to the given specific error type.
        /// </summary>
        /// <typeparam name="TSpecificError">The type of the specific error matched against the actual error.</typeparam>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the result value.</returns>
        IFilteredSpecificError<TSpecificError, TError, TSuccess> WhenError<TSpecificError>()
            where TSpecificError : TError;

        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when the result is an error.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the result value.</returns>
        IFilteredError<TError, TSuccess> WhenError();

        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when the result is a success.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the result value.</returns>
        IFilteredSuccess<TError, TSuccess> WhenSuccess();
    }
}
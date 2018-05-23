namespace SolidStack.Core.Flow.Internal.Result
{
    public interface IMappableSpecificError<TError, out TSuccess, TMappingDestination>
    {
        /// <summary>
        ///     Filters the current flow to be able to define a mapping action for it that will be executed only when the result is
        ///     an error and that error is assignable to the given specific error type.
        /// </summary>
        /// <returns>The filtered flow containing the following possible mapping action to perform on the result value.</returns>
        IFilteredMappableContent<
                TSpecificError, TMappingDestination,
                IMappableSpecificError<TError, TSuccess, TMappingDestination>>
            WhenError<TSpecificError>()
            where TSpecificError : TError;

        /// <summary>
        ///     Filters the current flow to be able to define a mapping action for it that will be executed only when the result is
        ///     an error.
        /// </summary>
        /// <returns>The filtered flow containing the following possible mapping action to perform on the result value.</returns>
        IFilteredMappableContent<TError, TMappingDestination, IMappableError<TError, TSuccess, TMappingDestination>>
            WhenError();
    }
}
namespace SolidStack.Core.Flow.Internal.Result
{
    public interface IMappableError<out TError, out TSuccess, TMappingDestination>
    {
        /// <summary>
        ///     Filters the current flow to be able to define a mapping action for it that will be executed only when the result is an error.
        /// </summary>
        /// <returns>The filtered flow containing the following possible mapping action to perform on the result value.</returns>
        IFilteredMappableContent<TSuccess, TMappingDestination> WhenSuccess();
    }
}
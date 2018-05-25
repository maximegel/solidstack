namespace SolidStack.Core.Flow.Internal.Option
{
    public interface IMappableSome<out T, TMappingDestination>
    {
        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when there is no value.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the optional value.</returns>
        IFilteredMappableVoid<TMappingDestination> WhenNone();
    }
}
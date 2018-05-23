namespace SolidStack.Core.Flow.Internal
{
    public interface IMappable<out TDestination>
    {
        /// <summary>
        ///     Maps the initial object using the filtered flow corresponding to the initial object state.
        /// </summary>
        TDestination Map();
    }
}
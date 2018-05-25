using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal
{
    public interface IFilteredMappableContent<out TContent, TDestination>
    {
        /// <summary>
        ///     Adds a mapping action to the current filtered flow that will be executed further if the initial object state
        ///     correspond to the current filtered flow.
        /// </summary>
        /// <param name="mapping">The mapping action to use.</param>
        /// <returns>The updated flow.</returns>
        /// <exception cref="GuardClauseException">If the given mapping action is null.</exception>
        IMappable<TDestination> MapTo(Func<TContent, TDestination> mapping);
    }
}
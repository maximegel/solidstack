using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    public interface IFilteredMappableSome<out T>
    {
        /// <summary>
        ///     Adds a mapping action to the current filtered flow that will be executed further if the initial option contains a
        ///     value.
        /// </summary>
        /// <typeparam name="TMappingDestination">The destination type to create.</typeparam>
        /// <param name="mapping">The mapping action to use.</param>
        /// <returns>The updated flow.</returns>
        /// <exception cref="GuardClauseException">If the given predicate is null.</exception>
        IMappableSome<T, TMappingDestination> MapTo<TMappingDestination>(Func<T, TMappingDestination> mapping);
    }
}
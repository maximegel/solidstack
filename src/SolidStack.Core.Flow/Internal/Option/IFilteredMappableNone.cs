using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    public interface IFilteredMappableNone<out T>
    {
        /// <summary>
        ///     Adds a mapping action to the current filtered flow that will be executed further if the initial option contains no value.
        /// </summary>
        /// <typeparam name="TMappingDestination">The destination type to create.</typeparam>
        /// <param name="mapping">The mapping action to use.</param>
        /// <returns>The updated flow.</returns>
        /// <exception cref="GuardClauseException">If the given mapping action is null.</exception>
        IMappableNone<T, TMappingDestination> MapTo<TMappingDestination>(Func<TMappingDestination> mapping);
    }
}
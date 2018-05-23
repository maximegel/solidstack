using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Result
{
    public interface IFilteredMappableError<out TError, out TSuccess>
    {
        /// <summary>
        ///     Adds a mapping action to the current filtered flow that will be executed further if the initial result is an error.
        /// </summary>
        /// <typeparam name="TMappingDestination">The destination type to create.</typeparam>
        /// <param name="mapping">The mapping action to use.</param>
        /// <returns>The updated flow.</returns>
        /// <exception cref="GuardClauseException">If the given mapping action is null.</exception>
        IMappableError<TError, TSuccess, TMappingDestination> MapTo<TMappingDestination>(
            Func<TError, TMappingDestination> mapping);
    }
}
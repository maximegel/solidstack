using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Result
{
    public interface IFilteredMappableSpecificError<out TSpecificError, TError, out TSuccess>
        where TSpecificError : TError
    {
        /// <summary>
        ///     Adds a mapping action to the current filtered flow that will be executed further if the initial result is an error
        ///     and that error is assignable to the given specific error type.
        /// </summary>
        /// <typeparam name="TMappingDestination">The destination type to create.</typeparam>
        /// <param name="mapping">The mapping action to use.</param>
        /// <returns>The updated flow.</returns>
        /// <exception cref="GuardClauseException">If the given mapping action is null.</exception>
        IMappableSpecificError<TError, TSuccess, TMappingDestination> MapTo<TMappingDestination>(
            Func<TSpecificError, TMappingDestination> mapping);
    }
}
using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class ResolvedFilteredError<TError, TSuccess> :
        IFilteredError<TError, TSuccess>
    {
        public ResolvedFilteredError(TError error) =>
            Error = error;

        private TError Error { get; }

        public IActionableResult<TError, TSuccess> Do(Action<TError> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableError<TError, TSuccess>(Error, action);
        }

        public IMappableError<TError, TSuccess, TMappingDestination> MapTo<TMappingDestination>(
            Func<TError, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new ResolvedMappableError<TError, TSuccess, TMappingDestination>(Error, mapping);
        }
    }
}
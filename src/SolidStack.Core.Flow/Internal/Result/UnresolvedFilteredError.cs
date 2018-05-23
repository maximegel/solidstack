using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class UnresolvedFilteredError<TError, TSuccess> :
        IFilteredError<TError, TSuccess>
    {
        public UnresolvedFilteredError(TSuccess success) =>
            Success = success;

        private TSuccess Success { get; }

        public IActionableResult<TError, TSuccess> Do(Action<TError> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableSuccess<TError, TSuccess>(Success);
        }

        public IMappableError<TError, TSuccess, TMappingDestination> MapTo<TMappingDestination>(
            Func<TError, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new UnresolvedMappableError<TError, TSuccess, TMappingDestination>(Success);
        }
    }
}
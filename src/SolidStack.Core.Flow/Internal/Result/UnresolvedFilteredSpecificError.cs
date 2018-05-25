using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class UnresolvedFilteredSpecificError<TSpecificError, TError, TSuccess> :
        IFilteredSpecificError<TSpecificError, TError, TSuccess>
        where TSpecificError : TError
    {
        public UnresolvedFilteredSpecificError(TSuccess success) =>
            Success = success;

        private TSuccess Success { get; }

        public IActionableResult<TError, TSuccess> Do(Action<TSpecificError> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableSuccess<TError, TSuccess>(Success);
        }

        public IMappableSpecificError<TError, TSuccess, TMappingDestination> MapTo<TMappingDestination>(
            Func<TSpecificError, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new UnresolvedMappableSpecificError<TError, TSuccess, TMappingDestination>(Success);
        }
    }
}
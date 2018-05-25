using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class ResolvedFilteredSpecificError<TSpecificError, TError, TSuccess> :
        IFilteredSpecificError<TSpecificError, TError, TSuccess>
        where TSpecificError : TError
    {
        public ResolvedFilteredSpecificError(TError error) =>
            Error = error;

        private TError Error { get; }

        public IActionableResult<TError, TSuccess> Do(Action<TSpecificError> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return Error is TSpecificError
                ? new ResolvedActionableError<TError, TSuccess>(
                    Error, error => action((TSpecificError) error))
                : new ResolvedActionableError<TError, TSuccess>(Error);
        }

        public IMappableSpecificError<TError, TSuccess, TMappingDestination> MapTo<TMappingDestination>(
            Func<TSpecificError, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return Error is TSpecificError
                ? new ResolvedMappableSpecificError<TError, TSuccess, TMappingDestination>(
                    Error, error => mapping((TSpecificError) error))
                : new ResolvedMappableSpecificError<TError, TSuccess, TMappingDestination>(Error);
        }
    }
}
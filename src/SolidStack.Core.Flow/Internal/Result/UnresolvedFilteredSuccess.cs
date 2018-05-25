using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class UnresolvedFilteredSuccess<TError, TSuccess> :
        IFilteredSuccess<TError, TSuccess>
    {
        public UnresolvedFilteredSuccess(TError error) =>
            Error = error;

        private TError Error { get; }

        public IActionableResult<TError, TSuccess> Do(Action<TSuccess> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableError<TError, TSuccess>(Error);
        }

        public IMappableSuccess<TError, TSuccess, TMappingDestination> MapTo<TMappingDestination>(
            Func<TSuccess, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new UnresolvedMappableSuccess<TError, TSuccess, TMappingDestination>(Error);
        }
    }
}
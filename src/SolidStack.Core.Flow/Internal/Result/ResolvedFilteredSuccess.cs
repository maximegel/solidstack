using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class ResolvedFilteredSuccess<TError, TSuccess> :
        IFilteredSuccess<TError, TSuccess>
    {
        public ResolvedFilteredSuccess(TSuccess success) =>
            Success = success;

        private TSuccess Success { get; }

        public IActionableResult<TError, TSuccess> Do(Action<TSuccess> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableSuccess<TError, TSuccess>(Success, action);
        }

        public IMappableSuccess<TError, TSuccess, TMappingDestination> MapTo<TMappingDestination>(
            Func<TSuccess, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new ResolvedMappableSuccess<TError, TSuccess, TMappingDestination>(Success, mapping);
        }
    }
}
using System;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class UnresolvedMappableError<TError, TSuccess, TMappingDestination> : MappableContent<
            ResolvedMappableSuccess<TError, TSuccess, TMappingDestination>, TSuccess, TMappingDestination>,
        IMappableError<TError, TSuccess, TMappingDestination>
    {
        public UnresolvedMappableError(TSuccess content) :
            base(content)
        {
        }

        public UnresolvedMappableError(TSuccess content, Func<TSuccess, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableContent<TSuccess, TMappingDestination> WhenSuccess() =>
            TryUseLastMapping();
    }
}
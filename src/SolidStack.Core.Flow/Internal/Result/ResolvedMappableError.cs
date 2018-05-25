using System;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class ResolvedMappableError<TError, TSuccess, TMappingDestination> : MappableContent<
            ResolvedMappableError<TError, TSuccess, TMappingDestination>, TError, TMappingDestination>,
        IMappableError<TError, TSuccess, TMappingDestination>
    {
        public ResolvedMappableError(TError content) :
            base(content)
        {
        }

        public ResolvedMappableError(TError content, Func<TError, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableContent<TSuccess, TMappingDestination> WhenSuccess() =>
            SkipLastMapping<TSuccess>();
    }
}
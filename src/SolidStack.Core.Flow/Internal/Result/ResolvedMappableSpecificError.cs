using System;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class ResolvedMappableSpecificError<TError, TSuccess, TMappingDestination> : MappableContent<
            ResolvedMappableSpecificError<TError, TSuccess, TMappingDestination>, TError, TMappingDestination>,
        IMappableSpecificError<TError, TSuccess, TMappingDestination>,
        IMappableError<TError, TSuccess, TMappingDestination>
    {
        public ResolvedMappableSpecificError(TError content) :
            base(content)
        {
        }

        public ResolvedMappableSpecificError(TError content, Func<TError, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableContent<TSuccess, TMappingDestination> WhenSuccess() =>
            SkipLastMapping<TSuccess>();

        public IFilteredMappableContent<
                TSpecificError, TMappingDestination,
                IMappableSpecificError<TError, TSuccess, TMappingDestination>>
            WhenError<TSpecificError>()
            where TSpecificError : TError =>
            Content is TSpecificError
                ? TryUseNextMapping<TSpecificError>()
                : SkipNextMapping<TSpecificError>();

        public IFilteredMappableContent<
                TError, TMappingDestination,
                IMappableError<TError, TSuccess, TMappingDestination>>
            WhenError() =>
            TryUseNextMapping();
    }
}
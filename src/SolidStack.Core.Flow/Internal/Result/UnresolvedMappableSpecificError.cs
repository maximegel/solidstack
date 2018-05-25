using System;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class UnresolvedMappableSpecificError<TError, TSuccess, TMappingDestination> : MappableContent<
            UnresolvedMappableSpecificError<TError, TSuccess, TMappingDestination>, TSuccess, TMappingDestination>,
        IMappableSpecificError<TError, TSuccess, TMappingDestination>,
        IMappableError<TError, TSuccess, TMappingDestination>
    {
        public UnresolvedMappableSpecificError(TSuccess content) :
            base(content)
        {
        }

        public UnresolvedMappableSpecificError(TSuccess content, Func<TSuccess, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableContent<TSuccess, TMappingDestination> WhenSuccess() =>
            TryUseLastMapping();

        public IFilteredMappableContent<
                TSpecificError, TMappingDestination,
                IMappableSpecificError<TError, TSuccess, TMappingDestination>>
            WhenError<TSpecificError>()
            where TSpecificError : TError =>
            SkipNextMapping<TSpecificError>();

        public IFilteredMappableContent<TError, TMappingDestination,
            IMappableError<TError, TSuccess, TMappingDestination>> WhenError() =>
            SkipNextMapping<TError>();
    }
}
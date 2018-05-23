using System;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class ResolvedMappableSuccess<TError, TSuccess, TMappingDestination> : MappableContent<
            ResolvedMappableSuccess<TError, TSuccess, TMappingDestination>, TSuccess, TMappingDestination>,
        IMappableSuccess<TError, TSuccess, TMappingDestination>
    {
        public ResolvedMappableSuccess(TSuccess content) :
            base(content)
        {
        }

        public ResolvedMappableSuccess(TSuccess content, Func<TSuccess, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableContent<
                TSpecificError, TMappingDestination,
                IMappableSuccess<TError, TSuccess, TMappingDestination>>
            WhenError<TSpecificError>()
            where TSpecificError : TError =>
            SkipNextMapping<TSpecificError>();

        public IFilteredMappableContent<TError, TMappingDestination> WhenError() =>
            SkipLastMapping<TError>();
    }
}
using System;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class UnresolvedMappableSuccess<TError, TSuccess, TMappingDestination> : MappableContent<
            UnresolvedMappableSuccess<TError, TSuccess, TMappingDestination>, TError, TMappingDestination>,
        IMappableSuccess<TError, TSuccess, TMappingDestination>
    {
        public UnresolvedMappableSuccess(TError content) :
            base(content)
        {
        }

        public UnresolvedMappableSuccess(TError content, Func<TError, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableContent<
                TSpecificError, TMappingDestination,
                IMappableSuccess<TError, TSuccess, TMappingDestination>>
            WhenError<TSpecificError>()
            where TSpecificError : TError =>
            Content is TSpecificError
                ? TryUseNextMapping<TSpecificError>()
                : SkipNextMapping<TSpecificError>();

        public IFilteredMappableContent<TError, TMappingDestination> WhenError() =>
            TryUseLastMapping();
    }
}
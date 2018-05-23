using System;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class UnresolvedMappableNone<T, TMappingDestination> : MappableContent<
            UnresolvedMappableNone<T, TMappingDestination>, T, TMappingDestination>,
        IMappableNone<T, TMappingDestination>
    {
        public UnresolvedMappableNone(T content) :
            base(content)
        {
        }

        public UnresolvedMappableNone(T content, Func<T, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableContent<T, TMappingDestination, IMappableNone<T, TMappingDestination>> When(
            Func<T, bool> predicate) =>
            predicate(Content)
                ? TryUseNextMapping()
                : SkipNextMapping();

        public IFilteredMappableContent<T, TMappingDestination> WhenSome() =>
            TryUseLastMapping();
    }
}
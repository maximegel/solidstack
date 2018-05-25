using System;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class ResolvedMappablePredicatedSome<T, TMappingDestination> : MappableContent<
            ResolvedMappablePredicatedSome<T, TMappingDestination>, T, TMappingDestination>,
        IMappablePredicatedSome<T, TMappingDestination>,
        IMappableSome<T, TMappingDestination>
    {
        public ResolvedMappablePredicatedSome(T content) :
            base(content)
        {
        }

        public ResolvedMappablePredicatedSome(T content, Func<T, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableContent<T, TMappingDestination, IMappablePredicatedSome<T, TMappingDestination>> When(
            Func<T, bool> predicate) =>
            predicate(Content)
                ? TryUseNextMapping()
                : SkipNextMapping();

        public IFilteredMappableContent<T, TMappingDestination, IMappableSome<T, TMappingDestination>> WhenSome() =>
            TryUseNextMapping();

        public IFilteredMappableVoid<TMappingDestination> WhenNone() =>
            SkipLastVoidMapping();
    }
}
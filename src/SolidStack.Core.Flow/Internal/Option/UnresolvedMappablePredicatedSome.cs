using System;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class UnresolvedMappablePredicatedSome<T, TMappingDestination> : MappableVoid<
            UnresolvedMappablePredicatedSome<T, TMappingDestination>, TMappingDestination>,
        IMappablePredicatedSome<T, TMappingDestination>,
        IMappableSome<T, TMappingDestination>
    {
        public IFilteredMappableContent<T, TMappingDestination, IMappablePredicatedSome<T, TMappingDestination>> When(
            Func<T, bool> predicate) =>
            SkipNextContentMapping<T>();

        public IFilteredMappableContent<T, TMappingDestination, IMappableSome<T, TMappingDestination>> WhenSome() =>
            SkipNextContentMapping<T>();

        public IFilteredMappableVoid<TMappingDestination> WhenNone() =>
            TryUseLastMapping();
    }
}
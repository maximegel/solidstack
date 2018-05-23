using System;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class ResolvedMappableNone<T, TMappingDestination> : MappableVoid<
            ResolvedMappableNone<T, TMappingDestination>, TMappingDestination>,
        IMappableNone<T, TMappingDestination>
    {
        public ResolvedMappableNone()
        {
        }

        public ResolvedMappableNone(Func<TMappingDestination> firstMapping) :
            base(firstMapping)
        {
        }

        public IFilteredMappableContent<T, TMappingDestination, IMappableNone<T, TMappingDestination>> When(
            Func<T, bool> predicate) =>
            SkipNextContentMapping<T>();

        public IFilteredMappableContent<T, TMappingDestination> WhenSome() =>
            SkipLastContentMapping<T>();
    }
}
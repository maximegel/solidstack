using System;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class ResolvedMappableSome<T, TMappingDestination> : MappableContent<
            ResolvedMappableSome<T, TMappingDestination>, T, TMappingDestination>,
        IMappableSome<T, TMappingDestination>
    {
        public ResolvedMappableSome(T content) :
            base(content)
        {
        }

        public ResolvedMappableSome(T content, Func<T, TMappingDestination> firstMapping) :
            base(content, firstMapping)
        {
        }

        public IFilteredMappableVoid<TMappingDestination> WhenNone() =>
            SkipLastVoidMapping();
    }
}
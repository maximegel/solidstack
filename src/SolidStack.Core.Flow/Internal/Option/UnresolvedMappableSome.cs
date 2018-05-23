namespace SolidStack.Core.Flow.Internal.Option
{
    internal class UnresolvedMappableSome<T, TMappingDestination> : MappableVoid<
            UnresolvedMappableSome<T, TMappingDestination>, TMappingDestination>,
        IMappableSome<T, TMappingDestination>
    {
        public IFilteredMappableVoid<TMappingDestination> WhenNone() =>
            TryUseLastMapping();
    }
}
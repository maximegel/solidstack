using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal
{
    internal class FilteredMappableVoid<TDestination, TMappable> :
        IFilteredMappableVoid<TDestination>,
        IFilteredMappableVoid<TDestination, TMappable>
        where TMappable : IMappable<TDestination>
    {
        public FilteredMappableVoid(TMappable mappable, Action<Func<TDestination>> handleMapping)
        {
            Mappable = mappable;
            HandleMapping = handleMapping;
        }

        private Action<Func<TDestination>> HandleMapping { get; }

        private TMappable Mappable { get; }

        TMappable IFilteredMappableVoid<TDestination, TMappable>.MapTo(Func<TDestination> mapping) =>
            MapTo(mapping);

        IMappable<TDestination> IFilteredMappableVoid<TDestination>.MapTo(Func<TDestination> mapping) =>
            MapTo(mapping);

        private TMappable MapTo(Func<TDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            HandleMapping(mapping);
            return Mappable;
        }
    }
}
using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal
{
    internal class FilteredMappableContent<TContent, TDestination, TMappable> :
        IFilteredMappableContent<TContent, TDestination>,
        IFilteredMappableContent<TContent, TDestination, TMappable>
        where TMappable : IMappable<TDestination>
    {
        public FilteredMappableContent(TMappable mappable, Action<Func<TContent, TDestination>> handleMapping)
        {
            Mappable = mappable;
            HandleMapping = handleMapping;
        }

        private Action<Func<TContent, TDestination>> HandleMapping { get; }

        private TMappable Mappable { get; }

        TMappable IFilteredMappableContent<TContent, TDestination, TMappable>.MapTo(
            Func<TContent, TDestination> mapping) =>
            MapTo(mapping);

        IMappable<TDestination> IFilteredMappableContent<TContent, TDestination>.MapTo(
            Func<TContent, TDestination> mapping) =>
            MapTo(mapping);

        private TMappable MapTo(Func<TContent, TDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            HandleMapping(mapping);
            return Mappable;
        }
    }
}
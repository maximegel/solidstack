using System;

namespace SolidStack.Core.Flow.Internal
{
    internal abstract class MappableVoid<TSelf, TDestination> :
        IMappable<TDestination>
        where TSelf : MappableVoid<TSelf, TDestination>
    {
        protected MappableVoid()
        {
            Mapping = () => throw new InvalidOperationException();
            TryUseMapping = mapping =>
            {
                Mapping = mapping;
                TryUseMapping = _ => { };
            };
        }

        protected MappableVoid(Func<TDestination> mapping)
        {
            Mapping = mapping;
            TryUseMapping = _ => { };
        }

        private Func<TDestination> Mapping { get; set; }

        private Action<Func<TDestination>> TryUseMapping { get; set; }

        public TDestination Map() =>
            Mapping();

        protected IFilteredMappableContent<TContent, TDestination> SkipLastContentMapping<TContent>() =>
            new FilteredMappableContent<TContent, TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableVoid<TDestination> SkipLastMapping() =>
            new FilteredMappableVoid<TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableContent<TContent, TDestination, TSelf> SkipNextContentMapping<TContent>() =>
            new FilteredMappableContent<TContent, TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableVoid<TDestination, TSelf> SkipNextMapping() =>
            new FilteredMappableVoid<TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableVoid<TDestination> TryUseLastMapping() =>
            new FilteredMappableVoid<TDestination, TSelf>((TSelf) this, TryUseMapping);

        protected IFilteredMappableVoid<TDestination, TSelf> TryUseNextMapping() =>
            new FilteredMappableVoid<TDestination, TSelf>((TSelf) this, TryUseMapping);
    }
}
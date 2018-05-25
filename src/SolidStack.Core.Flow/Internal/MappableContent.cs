using System;

namespace SolidStack.Core.Flow.Internal
{
    internal class MappableContent<TSelf, TContent, TDestination> :
        IMappable<TDestination>
        where TSelf : MappableContent<TSelf, TContent, TDestination>
    {
        public MappableContent(TContent content)
        {
            Content = content;
            Mapping = _ => throw new InvalidOperationException();
            TryUseMapping = mapping =>
            {
                Mapping = mapping;
                TryUseMapping = _ => { };
            };
        }

        public MappableContent(TContent content, Func<TContent, TDestination> mapping)
        {
            Content = content;
            Mapping = mapping;
            TryUseMapping = _ => { };
        }

        protected TContent Content { get; }

        private Func<TContent, TDestination> Mapping { get; set; }

        private Action<Func<TContent, TDestination>> TryUseMapping { get; set; }

        public TDestination Map() =>
            Mapping(Content);

        protected IFilteredMappableContent<TContent, TDestination> SkipLastMapping() =>
            new FilteredMappableContent<TContent, TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableContent<TOtherContent, TDestination> SkipLastMapping<TOtherContent>() =>
            new FilteredMappableContent<TOtherContent, TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableVoid<TDestination> SkipLastVoidMapping() =>
            new FilteredMappableVoid<TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableContent<TContent, TDestination, TSelf> SkipNextMapping() =>
            new FilteredMappableContent<TContent, TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableContent<TOtherContent, TDestination, TSelf> SkipNextMapping<TOtherContent>() =>
            new FilteredMappableContent<TOtherContent, TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableVoid<TDestination, TSelf> SkipNextVoidMapping() =>
            new FilteredMappableVoid<TDestination, TSelf>((TSelf) this, _ => { });

        protected IFilteredMappableContent<TContent, TDestination> TryUseLastMapping() =>
            new FilteredMappableContent<TContent, TDestination, TSelf>((TSelf) this, TryUseMapping);

        protected IFilteredMappableContent<TSpecificContent, TDestination> TryUseLastMapping<TSpecificContent>()
            where TSpecificContent : TContent =>
            new FilteredMappableContent<TSpecificContent, TDestination, TSelf>(
                (TSelf) this,
                func => func((TSpecificContent) Content));

        protected IFilteredMappableContent<TContent, TDestination, TSelf> TryUseNextMapping() =>
            new FilteredMappableContent<TContent, TDestination, TSelf>((TSelf) this, TryUseMapping);

        protected IFilteredMappableContent<TSpecificContent, TDestination, TSelf> TryUseNextMapping<TSpecificContent>()
            where TSpecificContent : TContent =>
            new FilteredMappableContent<TSpecificContent, TDestination, TSelf>(
                (TSelf) this,
                func => func((TSpecificContent) Content));
    }
}
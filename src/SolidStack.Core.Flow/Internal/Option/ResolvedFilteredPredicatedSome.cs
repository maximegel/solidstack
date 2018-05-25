using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class ResolvedFilteredPredicatedSome<T> :
        IFilteredPredicatedSome<T>
    {
        public ResolvedFilteredPredicatedSome(T content, Func<T, bool> predicate)
        {
            Content = content;
            Predicate = predicate;
        }

        private T Content { get; }

        private Func<T, bool> Predicate { get; }

        public IActionableOption<T> Do(Action<T> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return Predicate(Content)
                ? new ResolvedActionableSome<T>(Content, action)
                : new ResolvedActionableSome<T>(Content);
        }

        public IMappablePredicatedSome<T, TMappingDestination> MapTo<TMappingDestination>(
            Func<T, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return Predicate(Content)
                ? new ResolvedMappablePredicatedSome<T, TMappingDestination>(Content, mapping)
                : new ResolvedMappablePredicatedSome<T, TMappingDestination>(Content);
        }
    }
}
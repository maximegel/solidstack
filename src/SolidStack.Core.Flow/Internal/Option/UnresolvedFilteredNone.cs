using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class UnresolvedFilteredNone<T> :
        IFilteredNone<T>
    {
        public UnresolvedFilteredNone(T content) =>
            Content = content;

        private T Content { get; }

        public IActionableOption<T> Do(Action action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableSome<T>(Content);
        }

        public IMappableNone<T, TMappingDestination> MapTo<TMappingDestination>(Func<TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new UnresolvedMappableNone<T, TMappingDestination>(Content);
        }
    }
}
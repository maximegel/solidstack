using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class ResolvedFilteredSome<T> :
        IFilteredSome<T>
    {
        public ResolvedFilteredSome(T content) =>
            Content = content;

        private T Content { get; }

        public IActionableOption<T> Do(Action<T> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableSome<T>(Content, action);
        }

        public IMappableSome<T, TMappingDestination> MapTo<TMappingDestination>(Func<T, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new ResolvedMappableSome<T, TMappingDestination>(Content, mapping);
        }
    }
}
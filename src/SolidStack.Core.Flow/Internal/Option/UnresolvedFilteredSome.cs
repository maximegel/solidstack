using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class UnresolvedFilteredSome<T> :
        IFilteredSome<T>
    {
        public IActionableOption<T> Do(Action<T> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableNone<T>();
        }

        public IMappableSome<T, TMappingDestination> MapTo<TMappingDestination>(Func<T, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new UnresolvedMappableSome<T, TMappingDestination>();
        }
    }
}
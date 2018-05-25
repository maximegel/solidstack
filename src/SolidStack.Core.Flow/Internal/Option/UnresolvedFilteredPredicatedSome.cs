using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class UnresolvedFilteredPredicatedSome<T> :
        IFilteredPredicatedSome<T>
    {
        public IActionableOption<T> Do(Action<T> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableNone<T>();
        }

        public IMappablePredicatedSome<T, TMappingDestination> MapTo<TMappingDestination>(
            Func<T, TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new UnresolvedMappablePredicatedSome<T, TMappingDestination>();
        }
    }
}
using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class ResolvedFilteredNone<T> :
        IFilteredNone<T>
    {
        public IActionableOption<T> Do(Action action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            return new ResolvedActionableNone<T>(action);
        }

        public IMappableNone<T, TMappingDestination> MapTo<TMappingDestination>(Func<TMappingDestination> mapping)
        {
            Guard.RequiresNonNull(mapping, nameof(mapping));

            return new ResolvedMappableNone<T, TMappingDestination>(mapping);
        }
    }
}
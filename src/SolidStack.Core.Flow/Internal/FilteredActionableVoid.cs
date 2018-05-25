using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal
{
    internal class FilteredActionableVoid<TActionable> :
        IFilteredActionableVoid<TActionable>
        where TActionable : IActionable
    {
        public FilteredActionableVoid(TActionable actionable, Action<Action> handleAction)
        {
            Actionable = actionable;
            HandleAction = handleAction;
        }

        private TActionable Actionable { get; }

        private Action<Action> HandleAction { get; }

        public TActionable Do(Action action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            HandleAction(action);
            return Actionable;
        }
    }
}
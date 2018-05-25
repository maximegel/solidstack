using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal
{
    internal class FilteredActionableContent<TContent, TActionable> :
        IFilteredActionableContent<TContent, TActionable>
        where TActionable : IActionable
    {
        public FilteredActionableContent(TActionable actionable, Action<Action<TContent>> handleAction)
        {
            Actionable = actionable;
            HandleAction = handleAction;
        }

        private TActionable Actionable { get; }

        private Action<Action<TContent>> HandleAction { get; }

        public TActionable Do(Action<TContent> action)
        {
            Guard.RequiresNonNull(action, nameof(action));

            HandleAction(action);
            return Actionable;
        }
    }
}
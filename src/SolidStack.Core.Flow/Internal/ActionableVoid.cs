using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidStack.Core.Flow.Internal
{
    internal abstract class ActionableVoid<TSelf> :
        IActionable
        where TSelf : ActionableVoid<TSelf>
    {
        protected ActionableVoid(params Action[] actions) :
            this(actions.AsEnumerable())
        {
        }

        protected ActionableVoid(IEnumerable<Action> actions) =>
            Actions = actions.ToList();

        private List<Action> Actions { get; }

        public void Execute() =>
            Actions.ForEach(action => action());

        protected IFilteredActionableVoid<TSelf> AppendNextAction() =>
            new FilteredActionableVoid<TSelf>((TSelf) this, Actions.Add);

        protected IFilteredActionableVoid<TSelf> SkipNextAction() =>
            new FilteredActionableVoid<TSelf>((TSelf) this, _ => { });

        protected IFilteredActionableContent<TContent, TSelf> SkipNextContentAction<TContent>() =>
            new FilteredActionableContent<TContent, TSelf>((TSelf) this, _ => { });
    }
}
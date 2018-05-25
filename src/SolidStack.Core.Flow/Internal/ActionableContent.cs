using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidStack.Core.Flow.Internal
{
    internal abstract class ActionableContent<TSelf, TContent> :
        IActionable
        where TSelf : ActionableContent<TSelf, TContent>
    {
        protected ActionableContent(TContent content, params Action<TContent>[] actions) :
            this(content, actions.AsEnumerable())
        {
        }

        protected ActionableContent(TContent content, IEnumerable<Action<TContent>> actions)
        {
            Content = content;
            Actions = actions.ToList();
        }

        protected TContent Content { get; }

        private List<Action<TContent>> Actions { get; }

        public void Execute() =>
            Actions.ForEach(action => action(Content));

        protected IFilteredActionableContent<TContent, TSelf> AppendNextAction() =>
            new FilteredActionableContent<TContent, TSelf>((TSelf) this, Actions.Add);

        protected IFilteredActionableContent<TSpecificContent, TSelf> AppendNextAction<TSpecificContent>()
            where TSpecificContent : TContent =>
            new FilteredActionableContent<TSpecificContent, TSelf>(
                (TSelf) this,
                action => action((TSpecificContent) Content));

        protected IFilteredActionableContent<TContent, TSelf> SkipNextAction() =>
            new FilteredActionableContent<TContent, TSelf>((TSelf) this, _ => { });

        protected IFilteredActionableContent<TOtherContent, TSelf> SkipNextAction<TOtherContent>() =>
            new FilteredActionableContent<TOtherContent, TSelf>((TSelf) this, _ => { });

        protected IFilteredActionableVoid<TSelf> SkipNextVoidAction() =>
            new FilteredActionableVoid<TSelf>((TSelf) this, _ => { });
    }
}
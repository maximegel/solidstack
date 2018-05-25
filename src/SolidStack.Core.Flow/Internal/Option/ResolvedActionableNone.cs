using System;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class ResolvedActionableNone<T> : ActionableVoid<ResolvedActionableNone<T>>,
        IActionableOption<T>
    {
        public ResolvedActionableNone()
        {
        }

        public ResolvedActionableNone(Action firstAction) :
            base(firstAction)
        {
        }

        public IFilteredActionableContent<T, IActionableOption<T>> When(Func<T, bool> predicate) =>
            SkipNextContentAction<T>();

        public IFilteredActionableVoid<IActionableOption<T>> WhenNone() =>
            AppendNextAction();

        public IFilteredActionableContent<T, IActionableOption<T>> WhenSome() =>
            SkipNextContentAction<T>();
    }
}
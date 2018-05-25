using System;

namespace SolidStack.Core.Flow.Internal.Option
{
    internal class ResolvedActionableSome<T> : ActionableContent<ResolvedActionableSome<T>, T>,
        IActionableOption<T>
    {
        public ResolvedActionableSome(T content) :
            base(content)
        {
        }

        public ResolvedActionableSome(T content, Action<T> firstAction) :
            base(content, firstAction)
        {
        }

        public IFilteredActionableContent<T, IActionableOption<T>> When(Func<T, bool> predicate) =>
            predicate(Content)
                ? AppendNextAction()
                : SkipNextAction();

        public IFilteredActionableVoid<IActionableOption<T>> WhenNone() =>
            SkipNextVoidAction();

        public IFilteredActionableContent<T, IActionableOption<T>> WhenSome() =>
            AppendNextAction();
    }
}
using System;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class ResolvedActionableSuccess<TError, TSuccess> :
        ActionableContent<ResolvedActionableSuccess<TError, TSuccess>, TSuccess>,
        IActionableResult<TError, TSuccess>
    {
        public ResolvedActionableSuccess(TSuccess content) :
            base(content)
        {
        }

        public ResolvedActionableSuccess(TSuccess content, Action<TSuccess> firstAction) :
            base(content, firstAction)
        {
        }

        public IFilteredActionableContent<TSpecificError, IActionableResult<TError, TSuccess>> WhenError<
            TSpecificError>() where TSpecificError : TError =>
            SkipNextAction<TSpecificError>();

        public IFilteredActionableContent<TError, IActionableResult<TError, TSuccess>> WhenError() =>
            SkipNextAction<TError>();

        public IFilteredActionableContent<TSuccess, IActionableResult<TError, TSuccess>> WhenSuccess() =>
            AppendNextAction();
    }
}
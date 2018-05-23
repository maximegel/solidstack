using System;

namespace SolidStack.Core.Flow.Internal.Result
{
    internal class ResolvedActionableError<TError, TSuccess> :
        ActionableContent<ResolvedActionableError<TError, TSuccess>, TError>,
        IActionableResult<TError, TSuccess>
    {
        public ResolvedActionableError(TError content) :
            base(content)
        {
        }

        public ResolvedActionableError(TError content, Action<TError> firstAction) :
            base(content, firstAction)
        {
        }

        public IFilteredActionableContent<TSpecificError, IActionableResult<TError, TSuccess>> WhenError<
            TSpecificError>() where TSpecificError : TError =>
            Content is TSpecificError
                ? AppendNextAction<TSpecificError>()
                : SkipNextAction<TSpecificError>();

        public IFilteredActionableContent<TError, IActionableResult<TError, TSuccess>> WhenError() =>
            AppendNextAction();

        public IFilteredActionableContent<TSuccess, IActionableResult<TError, TSuccess>> WhenSuccess() =>
            SkipNextAction<TSuccess>();
    }
}
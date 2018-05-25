using System.Collections.Generic;
using System.Linq;
using SolidStack.Core.Flow.Internal.Result;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow
{
    public class Result<TError, TSuccess> :
        IResult<TError, TSuccess>
    {
        private Result(IEnumerable<TError> errorContent, IEnumerable<TSuccess> successContent)
        {
            ErrorContent = errorContent;
            SuccessContent = successContent;
        }

        private IEnumerable<TError> ErrorContent { get; }

        private IEnumerable<TSuccess> SuccessContent { get; }

        public IFilteredSpecificError<TSpecificError, TError, TSuccess> WhenError<TSpecificError>()
            where TSpecificError : TError =>
            ErrorContent
                .Select<TError, IFilteredSpecificError<TSpecificError, TError, TSuccess>>(
                    item => new ResolvedFilteredSpecificError<TSpecificError, TError, TSuccess>(item))
                .DefaultIfEmpty(
                    new UnresolvedFilteredSpecificError<TSpecificError, TError, TSuccess>(
                        SuccessContent.SingleOrDefault()))
                .Single();

        public IFilteredError<TError, TSuccess> WhenError() =>
            ErrorContent
                .Select<TError, IFilteredError<TError, TSuccess>>(
                    item => new ResolvedFilteredError<TError, TSuccess>(item))
                .DefaultIfEmpty(new UnresolvedFilteredError<TError, TSuccess>(SuccessContent.SingleOrDefault()))
                .Single();

        public IFilteredSuccess<TError, TSuccess> WhenSuccess() =>
            SuccessContent
                .Select<TSuccess, IFilteredSuccess<TError, TSuccess>>(
                    item => new ResolvedFilteredSuccess<TError, TSuccess>(item))
                .DefaultIfEmpty(new UnresolvedFilteredSuccess<TError, TSuccess>(ErrorContent.SingleOrDefault()))
                .Single();

        public static IResult<TError, TSuccess> Error(TError error)
        {
            Guard.RequiresNonNull(error, nameof(error));

            return new Result<TError, TSuccess>(new[] {error}, new TSuccess[0]);
        }

        public static IResult<TError, TSuccess> Success(TSuccess success)
        {
            Guard.RequiresNonNull(success, nameof(success));

            return new Result<TError, TSuccess>(new TError[0], new[] {success});
        }
    }
}
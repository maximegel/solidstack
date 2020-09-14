using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidStack.Core.Errors
{
    public class Try<TError, TResult> :
        IPartiallyHandledTry<TError, TResult>,
        IHandledTry<TError, TResult>
    {
        public Try(Func<TResult> function, Func<Exception, TError> errorMapper)
        {
            Function = function;
            ErrorMapper = errorMapper;
            Handlers = new Dictionary<Type, Func<TError, TResult>>();
        }

        private Try(
            Func<TResult> function,
            Func<Exception, TError> errorMapper,
            IDictionary<Type, Func<TError, TResult>> handlers)
        {
            Function = function;
            ErrorMapper = errorMapper;
            Handlers = handlers;
        }

        private Func<Exception, TError> ErrorMapper { get; }

        private Func<TResult> Function { get; }

        private IDictionary<Type, Func<TError, TResult>> Handlers { get; }

        TResult IHandledTry<TError, TResult>.Invoke() =>
            Invoke();

        public IHandledTry<TError, TDestinationResult> Map<TDestinationResult>(
            Func<TResult, TDestinationResult> mapper) =>
            new Try<TError, TDestinationResult>(
                () => mapper(Function()),
                ErrorMapper,
                Handlers
                    .AsEnumerable()
                    .ToDictionary(
                        pair => pair.Key,
                        pair => new Func<TError, TDestinationResult>(error => mapper(pair.Value(error)))));

        Try<TError, TResult> IHandledTry<TError, TResult>.Then(
            Func<TResult, Try<TError, TResult>> continuationTryFactory) =>
            Then(continuationTryFactory);

        public IHandledTry<TError, TResult> Catch(Func<TError, TResult> handler) =>
            new Try<TError, TResult>(
                Function,
                ErrorMapper,
                new Dictionary<Type, Func<TError, TResult>>(Handlers)
                {
                    {typeof(Exception), handler}
                });

        public IPartiallyHandledTry<TError, TResult> Catch<TSpecificError>(
            Func<TSpecificError, TResult> handler)
            where TSpecificError : TError =>
            new Try<TError, TResult>(
                Function,
                ErrorMapper,
                new Dictionary<Type, Func<TError, TResult>>(
                    Handlers.Where(pair => pair.Key != typeof(TSpecificError)))
                {
                    {typeof(TSpecificError), error => handler((TSpecificError) error)}
                });

        private TResult Invoke()
        {
            try
            {
                return Function();
            }
            catch (Exception exception)
            {
                var mappedException = ErrorMapper(exception);
                return Handlers[mappedException.GetType()](mappedException);
            }
        }

        private Try<TError, TResult> Then(Func<TResult, Try<TError, TResult>> continuationTryFactory) =>
            new Try<TError, TResult>(
                () => continuationTryFactory(Invoke()).Invoke(),
                ErrorMapper,
                new Dictionary<Type, Func<TError, TResult>>());
    }

    public interface IHandledTry<TError, TResult>
    {
        TResult Invoke();

        IHandledTry<TError, TDestinationResult> Map<TDestinationResult>(Func<TResult, TDestinationResult> mapper);

        Try<TError, TResult> Then(Func<TResult, Try<TError, TResult>> continuationTryFactory);
    }

    public interface IPartiallyHandledTry<TError, TResult>
    {
        IHandledTry<TError, TResult> Catch(Func<TError, TResult> handler);

        IPartiallyHandledTry<TError, TResult> Catch<TSpecificError>(Func<TSpecificError, TResult> handler)
            where TSpecificError : TError;
    }
}
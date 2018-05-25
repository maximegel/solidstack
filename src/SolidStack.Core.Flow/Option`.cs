using System;
using System.Collections.Generic;
using System.Linq;
using SolidStack.Core.Flow.Internal.Option;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow
{
    /// <inheritdoc />
    public class Option<T> :
        IOption<T>
    {
        private Option(IEnumerable<T> content) =>
            Content = content;

        private IEnumerable<T> Content { get; }

        /// <inheritdoc />
        public IFilteredPredicatedSome<T> When(Func<T, bool> predicate)
        {
            Guard.RequiresNonNull(predicate, nameof(predicate));

            return
                Content
                    .Select<T, IFilteredPredicatedSome<T>>(item =>
                        new ResolvedFilteredPredicatedSome<T>(item, predicate))
                    .DefaultIfEmpty(new UnresolvedFilteredPredicatedSome<T>())
                    .Single();
        }

        /// <inheritdoc />
        public IFilteredNone<T> WhenNone() =>
            Content
                .Select<T, IFilteredNone<T>>(item => new UnresolvedFilteredNone<T>(item))
                .DefaultIfEmpty(new ResolvedFilteredNone<T>())
                .Single();

        /// <inheritdoc />
        public IFilteredSome<T> WhenSome() =>
            Content
                .Select<T, IFilteredSome<T>>(item => new ResolvedFilteredSome<T>(item))
                .DefaultIfEmpty(new UnresolvedFilteredSome<T>())
                .Single();

        /// <summary>
        ///     Creates an option using an existing value; if the value is null, the option is created with no value.
        /// </summary>
        /// <param name="optionnal"></param>
        /// <returns>The created option.</returns>
        public static IOption<T> From(T optionnal) =>
            optionnal != null
                ? Some(optionnal)
                : None();

        /// <summary>
        ///     Creates an option that doesn't contain a value.
        /// </summary>
        /// <returns>The created option.</returns>
        public static IOption<T> None() =>
            new Option<T>(new T[0]);

        /// <summary>
        ///     Creates an option that contain a value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The created option.</returns>
        /// <exception cref="GuardClauseException">If the given value is null.</exception>
        public static IOption<T> Some(T value)
        {
            Guard.RequiresNonNull(value, nameof(value));

            return new Option<T>(new[] {value});
        }
    }
}
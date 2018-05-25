using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow
{
    public static class OptionAdapters
    {
        /// <summary>
        ///     Returns a empty sequence if there is no value or a sequence of one item if there is a value.
        /// </summary>
        /// <remarks>
        ///     The returned sequence will never contain more than one element.
        /// </remarks>
        /// <returns>The sequence of zero or one item.</returns>
        /// <exception cref="GuardClauseException">If the given option is null.</exception>
        public static IEnumerable<T> AsEnumerable<T>(this IOption<T> option)
        {
            Guard.RequiresNonNull(option, nameof(option));

            return option
                .WhenSome()
                .MapTo(item => new[] {item})
                .WhenNone()
                .MapTo(() => new T[0])
                .Map();
        }

        /// <summary>
        ///     Returns an option containing the first element of a sequence, or an empty option if the sequence contains no
        ///     elements.
        /// </summary>
        /// <typeparam name="T">The type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence to search in.</param>
        /// <returns>An option containing the first element or an empty option.</returns>
        /// <exception cref="GuardClauseException">If the given sequence is null.</exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IOption<T> TryFirst<T>(this IEnumerable<T> sequence)
        {
            Guard.RequiresNonNull(sequence, nameof(sequence));

            if (sequence is IList<T> list)
            {
                if (list.Count > 0)
                    return Option.Some(list[0]);
            }
            else
            {
                using (var enumerator = sequence.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                        return Option.Some(enumerator.Current);
                }
            }

            return Option.None<T>();
        }

        /// <summary>
        ///     Returns an option containing the first element of the sequence that satisfies a condition or a an empty option if
        ///     no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence to search in.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An option containing the first element found or an empty option.</returns>
        /// <exception cref="GuardClauseException">If the given sequence is null.</exception>
        /// <exception cref="GuardClauseException">If the given predicate is null.</exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IOption<T> TryFirst<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            Guard.RequiresNonNull(sequence, nameof(sequence));
            Guard.RequiresNonNull(predicate, nameof(predicate));

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (var element in sequence)
                if (predicate(element))
                    return Option.Some(element);

            return Option.None<T>();
        }

        /// <summary>
        ///     Gets the value associated with the specified key or an empty option if the value can't be found.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to search in.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        ///     An option with some value if the dictionary contains an element with the specified key; otherwise, an empty
        ///     option.
        /// </returns>
        /// <exception cref="GuardClauseException">If the given dictionary in null.</exception>
        /// <exception cref="GuardClauseException">If the given key in null.</exception>
        public static IOption<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            Guard.RequiresNonNull(dictionary, nameof(dictionary));
            Guard.RequiresNonNull(key, nameof(key));

            return dictionary.TryGetValue(key, out var value)
                ? Option<TValue>.Some(value)
                : Option<TValue>.None();
        }

        /// <summary>
        ///     Returns an option containing the only element of a sequence, or an empty option if the sequence contains no
        ///     elements.
        /// </summary>
        /// <typeparam name="T">The type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence to search in.</param>
        /// <returns>An option containing the first element or an empty option.</returns>
        /// <exception cref="GuardClauseException">If the given sequence is null.</exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IOption<T> TrySingle<T>(this IEnumerable<T> sequence)
        {
            Guard.RequiresNonNull(sequence, nameof(sequence));

            if (sequence is IList<T> list)
                switch (list.Count)
                {
                    case 0:
                        return Option.None<T>();
                    case 1:
                        return Option.Some(list[0]);
                    default:
                        return Option.None<T>();
                }

            using (var enumerator = sequence.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return Option.None<T>();

                var result = enumerator.Current;

                if (!enumerator.MoveNext())
                    return Option.Some(result);
            }

            return Option.None<T>();
        }

        /// <summary>
        ///     Returns an option containing the only element of a sequence, or an empty option if the sequence contains no
        ///     elements.
        /// </summary>
        /// <typeparam name="T">The type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence to search in.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An option containing the first element or an empty option.</returns>
        /// <exception cref="GuardClauseException">If the given sequence is null.</exception>
        /// <exception cref="GuardClauseException">If the given predicate is null.</exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IOption<T> TrySingle<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            Guard.RequiresNonNull(sequence, nameof(sequence));
            Guard.RequiresNonNull(predicate, nameof(predicate));

            var result = Option.None<T>();
            long count = 0;

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (var element in sequence)
                if (predicate(element))
                {
                    result = Option.Some(element);
                    checked
                    {
                        count++;
                    }
                }

            switch (count)
            {
                case 0:
                    return Option.None<T>();
                case 1:
                    return result;
                default:
                    return Option.None<T>();
            }
        }

        /// <summary>
        ///     Keeps only the options that contain a value.
        /// </summary>
        /// <typeparam name="T">The type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence of options to filter.</param>
        /// <returns>A new sequence with no more optional elements.</returns>
        /// <exception cref="GuardClauseException">If the given sequence is null.</exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<T> WhereSome<T>(this IEnumerable<IOption<T>> sequence)
        {
            Guard.RequiresNonNull(sequence, nameof(sequence));

            return sequence.SelectMany(option => option.AsEnumerable()).ToList();
        }

        /// <summary>
        ///     Projects each element of a sequence into a optional form and then keeps only the options that contain a value.
        /// </summary>
        /// <typeparam name="T">The type of the sequence elements.</typeparam>
        /// <typeparam name="TResult">The destination type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence of values to invoke a transform function on.</param>
        /// <param name="predicate">The transform function to apply to each element.</param>
        /// <returns>A sequence containing only the elements that have returned an option with some value after being transformed.</returns>
        /// <exception cref="GuardClauseException">If the given sequence is null.</exception>
        /// <exception cref="GuardClauseException">If the given predicate is null.</exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<TResult> WhereSome<T, TResult>
            (this IEnumerable<T> sequence, Func<T, IOption<TResult>> predicate)
        {
            Guard.RequiresNonNull(sequence, nameof(sequence));
            Guard.RequiresNonNull(predicate, nameof(predicate));

            return sequence.Select(predicate).WhereSome();
        }
    }
}
using System;
using System.Collections.Generic;

namespace SolidStack.Core.Options
{
    public static class OptionAdapters
    {
        /// <summary>
        ///     Returns an option containing the first element of the sequence that satisfies a condition or a an empty option if
        ///     no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence to search in.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An option containing the first element found or an empty option.</returns>
        public static Option<T> TryFirst<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (var element in sequence)
                if (predicate(element))
                    return element;

            return Option.None;
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
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.TryGetValue(key, out var value)
                ? (Option<TValue>) value
                : Option.None;

        /// <summary>
        ///     Returns an option containing the only element of a sequence, or an empty option if the sequence contains no
        ///     elements.
        /// </summary>
        /// <typeparam name="T">The type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence to search in.</param>
        /// <returns>An option containing the first element or an empty option.</returns>
        public static Option<T> TrySingle<T>(this IEnumerable<T> sequence)
        {
            if (sequence is IList<T> list)
                switch (list.Count)
                {
                    case 0:
                        return Option.None;
                    case 1:
                        return list[0];
                    default:
                        return Option.None;
                }

            using (var enumerator = sequence.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return Option.None;

                var result = enumerator.Current;

                if (!enumerator.MoveNext())
                    return Option.Some(result);
            }

            return Option.None;
        }

        public static Option<T> When<T>(this T value, Func<T, bool> predicate) =>
            predicate(value) ? (Option<T>) value : Option.None;
    }
}
using System.Collections.Generic;
using JetBrains.Annotations;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Collections
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> source,
            [NotNull] TKey key,
            [NotNull] TValue value)
        {
            Guard.RequiresNonNull(source, nameof(source));
            Guard.RequiresNonNull(key, nameof(key));
            Guard.RequiresNonNull(value, nameof(value));

            AddOrUpdate(source, new KeyValuePair<TKey, TValue>(key, value));
        }

        public static void AddOrUpdate<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> source,
            KeyValuePair<TKey, TValue> pair)
        {
            Guard.RequiresNonNull(source, nameof(source));

            if (source.ContainsKey(pair.Key))
            {
                source[pair.Key] = pair.Value;
                return;
            }

            source.Add(pair);
        }

        public static void AddOrUpdateRange<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> source,
            [NotNull] IDictionary<TKey, TValue> second)
        {
            Guard.RequiresNonNull(second, nameof(second));
            Guard.RequiresNonNull(source, nameof(source));

            second.ForEach(source.AddOrUpdate);
        }

        public static void RemoveRange<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> source,
            [NotNull] IEnumerable<TKey> keys)
        {
            Guard.RequiresNonNull(source, nameof(source));
            Guard.RequiresNonNull(keys, nameof(keys));

            keys.ForEach(key => source.Remove(key));
        }

        public static void RemoveRange<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> source,
            [NotNull] IDictionary<TKey, TValue> second)
        {
            Guard.RequiresNonNull(source, nameof(source));
            Guard.RequiresNonNull(second, nameof(second));

            second.ForEach(pair => source.Remove(pair));
        }
    }
}
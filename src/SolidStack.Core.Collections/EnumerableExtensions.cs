using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Collections
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>([NotNull] this IEnumerable<T> source, [NotNull] Action<T> action)
        {
            Guard.RequiresNonNull(source, nameof(source));
            Guard.RequiresNonNull(action, nameof(action));

            foreach (var element in source)
                action(element);
        }

        public static void ForEach<T>([NotNull] this IEnumerable<T> source, [NotNull] Action<T, int> action)
        {
            Guard.RequiresNonNull(source, nameof(source));
            Guard.RequiresNonNull(action, nameof(action));

            var index = 0;
            foreach (var element in source)
                action(element, index++);
        }
    }
}
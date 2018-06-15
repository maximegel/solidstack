using System.Collections;
using System.Linq;

namespace SolidStack.Core.Equality.Internal
{
    internal static class EnumerableExtensions
    {
        private const int HashCodeSeed = 17;

        private const int HashCodeMultiplier = 89;

        public static int GetSequenceHashCode(this IEnumerable sequence)
        {
            if (sequence is null)
                return 0;

            unchecked
            {
                return sequence.Cast<object>().Aggregate(HashCodeSeed,
                    (current, o) => (current * HashCodeMultiplier) ^ (o?.GetHashCode() ?? 0));
            }
        }

        public static bool SequenceEqual(this IEnumerable sequence, IEnumerable second) =>
            Enumerable.SequenceEqual(sequence.Cast<object>(), second.Cast<object>());
    }
}
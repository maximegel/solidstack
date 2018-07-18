using System.Collections;
using System.Linq;

namespace SolidStack.Core.Equality.Internal
{
    internal static class EnumerableExtensions
    {
        private const int HashCodeSeed = 17;

        private const int HashCodeMultiplier = 89;

        public static int GetSequenceHashCode(this IEnumerable @this)
        {
            if (@this is null)
                return 0;

            unchecked
            {
                return @this.Cast<object>().Aggregate(HashCodeSeed,
                    (current, o) => (current * HashCodeMultiplier) ^ (o?.GetHashCode() ?? 0));
            }
        }

        public static bool SequenceEqual(this IEnumerable @this, IEnumerable other) =>
            // True if both references are equal.
            ReferenceEquals(@this, other) ||
            // False if only one sequence is null.
            !(@this is null) && !(other is null) &&
            // True if all members of both sequences are equal.
            Enumerable.SequenceEqual(@this.Cast<object>(), other.Cast<object>());
    }
}
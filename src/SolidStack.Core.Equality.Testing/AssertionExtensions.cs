using System.Collections.Generic;

namespace SolidStack.Core.Equality.Testing
{
    public static class AssertionExtensions
    {
        public static EqualityComparerAssertions<T> Should<T>(this IEqualityComparer<T> equalityComparer) =>
            new EqualityComparerAssertions<T>(equalityComparer);
    }
}
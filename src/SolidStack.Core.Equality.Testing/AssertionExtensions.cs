using System;
using System.Collections.Generic;

namespace SolidStack.Core.Equality.Testing
{
    public static class AssertionExtensions
    {
        public static EqualityComparerAssertions<T> Should<T>(this IEqualityComparer<T> equalityComparer) 
            where T : class =>
            new EqualityComparerAssertions<T>(equalityComparer);

        public static EquatableAssertions<T> Should<T>(this IEquatable<T> equatable) =>
            new EquatableAssertions<T>(equatable);
    }
}
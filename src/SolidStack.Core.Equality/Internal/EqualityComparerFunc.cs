using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SolidStack.Core.Equality.Internal
{
    internal class EqualityComparerFunc<T> : IEqualityComparer<T>
        where T : class
    {
        public EqualityComparerFunc(Func<T, T, bool> equalsFunc, Func<T, int> getHashCodeFunc)
        {
            EqualsFunc = equalsFunc;
            GetHashCodeFunc = getHashCodeFunc;
        }

        private Func<T, T, bool> EqualsFunc { get; }

        private Func<T, int> GetHashCodeFunc { get; }

        public bool Equals(T x, T y) =>
            // True if both references are equal.
            ReferenceEquals(x, y) ||
            // False if only one object is null.
            !(x is null) && !(y is null) &&
            // True if the delegate function validate the equality.
            EqualsFunc(x, y);

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public int GetHashCode(T obj) =>
            obj is null
                ? 0
                : GetHashCodeFunc(obj);
    }
}
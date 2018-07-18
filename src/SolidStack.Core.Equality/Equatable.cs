using System;
using System.Collections.Generic;

namespace SolidStack.Core.Equality
{
    /// <inheritdoc />
    /// <summary>
    ///     Provides the boilerplate required to alter the equality of an object by overriding the equality methods, the
    ///     equality operators and the <see cref="object.GetHashCode" /> method.
    /// </summary>
    /// <typeparam name="TSelf">
    ///     The concrete type that should be equatable.
    ///     This is almost always the type that derives from <see cref="Equatable{TSelf}" />,
    ///     e.g. <c>class Foo : Equatable&lt;Foo&gt;</c>
    /// </typeparam>
    public abstract class Equatable<TSelf> :
        IEquatable<TSelf>
        where TSelf : Equatable<TSelf>
    {
        private static IEqualityComparer<TSelf> CachedEqualityComparer { get; set; }

        private IEqualityComparer<TSelf> EqualityComparer =>
            CachedEqualityComparer ?? (CachedEqualityComparer = GetEqualityComparer());

        /// <inheritdoc />
        public bool Equals(TSelf other) =>
            EqualityComparer.Equals(this as TSelf, other);

        public static bool operator ==(Equatable<TSelf> x, Equatable<TSelf> y) =>
            // True if both references are equal.
            ReferenceEquals(x, y) ||
            // True if both objects are equal.
            x?.EqualityComparer.Equals(x as TSelf, y as TSelf) == true;

        public static bool operator !=(Equatable<TSelf> x, Equatable<TSelf> y) =>
            !(x == y);

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            Equals(obj as TSelf);

        /// <inheritdoc />
        public override int GetHashCode() =>
            EqualityComparer.GetHashCode((TSelf) this);

        /// <summary>
        ///     Returns the equality comparer used to define how the equality of the object should be done.
        /// </summary>
        /// <remarks>
        ///     The given equality comparer is cached to increase performance.
        /// </remarks>
        /// <returns>The equality comparer.</returns>
        protected abstract IEqualityComparer<TSelf> GetEqualityComparer();
    }
}
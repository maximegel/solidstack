using System;
using SolidStack.Core.Flow.Internal.Option;

namespace SolidStack.Core.Flow
{
    /// <summary>
    ///     Represents a value that may or may not be present and provides methods for building and then executing an
    ///     easy-to-read linear flow around these two situations.
    /// </summary>
    /// <typeparam name="T">The type of the value that may or may not be present.</typeparam>
    public interface IOption<out T>
    {
        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when there is a value and that
        ///     value matches the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match against the value.</param>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the optional value.</returns>
        IFilteredPredicatedSome<T> When(Func<T, bool> predicate);

        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when there is no value.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the optional value.</returns>
        IFilteredNone<T> WhenNone();

        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when there is a value.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the optional value.</returns>
        IFilteredSome<T> WhenSome();
    }
}
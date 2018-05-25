using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    public interface IActionableOption<out T> :
        IActionable
    {
        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when there is a value and that
        ///     value matches the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match against the value.</param>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the optional value.</returns>
        /// <exception cref="GuardClauseException">If the given predicate is null.</exception>
        IFilteredActionableContent<T, IActionableOption<T>> When(Func<T, bool> predicate);

        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when there is no value.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the optional value.</returns>
        IFilteredActionableVoid<IActionableOption<T>> WhenNone();

        /// <summary>
        ///     Filters the current flow to be able to create actions that will be executed only when there is a value.
        /// </summary>
        /// <returns>The filtered flow containing the following possible actions that can be performed on the optional value.</returns>
        IFilteredActionableContent<T, IActionableOption<T>> WhenSome();
    }
}
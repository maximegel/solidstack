using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal.Option
{
    public interface IMappablePredicatedSome<out T, TMappingDestination>
    {
        /// <summary>
        ///     Filters the current flow to be able to define a mapping action for it that will be executed only when there is a value and that
        ///     value matches the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match against the value.</param>
        /// <returns>The filtered flow containing the following possible mapping action to perform on the optional value.</returns>
        /// <exception cref="GuardClauseException">If the given predicate is null.</exception>

        IFilteredMappableContent<T, TMappingDestination, IMappablePredicatedSome<T, TMappingDestination>> When(
            Func<T, bool> predicate);

        /// <summary>
        ///     Filters the current flow to be able to define a mapping action for it that will be executed only when there is a value.
        /// </summary>
        /// <returns>The filtered flow containing the following possible mapping action to perform on the optional value.</returns>
        IFilteredMappableContent<T, TMappingDestination, IMappableSome<T, TMappingDestination>> WhenSome();
    }
}
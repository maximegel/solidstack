using System;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow.Internal
{
    public interface IFilteredActionableVoid<out TActionable>
        where TActionable : IActionable
    {
        /// <summary>
        ///     Adds an action to the current filtered flow that will be executed further if the execution flow match the current
        ///     filtered flow.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <returns>The updated flow.</returns>
        /// <exception cref="GuardClauseException">If the given action is null.</exception>
        TActionable Do(Action action);
    }
}
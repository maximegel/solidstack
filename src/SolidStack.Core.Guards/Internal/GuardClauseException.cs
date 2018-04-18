using System;

namespace SolidStack.Core.Guards.Internal
{
    /// <inheritdoc />
    /// <summary>
    ///     The exception that is thrown when the guard clause (i.e. pre-condition / post-condition) of a method isn't respected.
    /// </summary>
    /// <remarks>
    ///     That kind of exception should never be catch.
    /// </remarks>
    public class GuardClauseException : Exception
    {
        public GuardClauseException(string message) :
            base(message)
        {
        }
    }
}
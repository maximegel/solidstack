using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SolidStack.Core.Guards
{
    /// <summary>
    ///     <para>
    ///         Provides guard clauses that allow you to write pre-conditions and post-conditions for your methods in a
    ///         readable way.
    ///     </para>
    ///     <para>
    ///         <see
    ///             href="https://github.com/idmobiles/solidstack/wiki/SolidStack.Core.Guards#protecting-your-methods-with-the-guard-class" />
    ///     </para>
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Checks for a method post-condition; if the condition is false, throws an exception that should never be catch.
        /// </summary>
        /// <remarks>
        ///     Run in "debug" mode only to not affect performance.
        /// </remarks>
        /// <param name="predicate">The condition to validate.</param>
        /// <param name="message">The error message to display.</param>
        /// <exception cref="GuardClauseException">If the specified condition is not satisfied.</exception>
        [Conditional("DEBUG")]
        public static void Ensures(Func<bool> predicate, string message) =>
            Assert(predicate, message);

        /// <summary>
        ///     Checks for a method post-condition against all items in a sequence; if any condition is false, throws an exception that should never be catch.
        /// </summary>
        /// <remarks>
        ///     Run in "debug" mode only to not affect performance.
        /// </remarks>
        /// <typeparam name="T">The type of the sequence items.</typeparam>
        /// <param name="sequence">The sequence to check.</param>
        /// <param name="predicate">The condition to validate.</param>
        /// <param name="message">The error message to display.</param>
        /// <exception cref="GuardClauseException">If one or more items of the sequence doesn't satisfy the specified condition.</exception>
        [Conditional("DEBUG")]
        public static void EnsuresAll<T>(
            IEnumerable<T> sequence,
            Func<T, bool> predicate, string message) =>
            Assert(() => sequence.All(predicate), message);

        /// <summary>
        ///     Checks for a method post-condition against all items in a sequence; if all conditions are false, throws an exception that should never be catch.
        /// </summary>
        /// <remarks>
        ///     Run in "debug" mode only to not affect performance.
        /// </remarks>
        /// <typeparam name="T">The type of the sequence items.</typeparam>
        /// <param name="sequence">The sequence to check.</param>
        /// <param name="predicate">The condition to validate.</param>
        /// <param name="message">The error message to display.</param>
        /// <exception cref="GuardClauseException">If all items in the sequence doesn't satisfy the specified condition.</exception>
        [Conditional("DEBUG")]
        public static void EnsuresAny<T>(
            IEnumerable<T> sequence,
            Func<T, bool> predicate, string message) =>
            Ensures(() => sequence.Any(predicate), message);

        /// <summary>
        ///     Checks if a value is null as a method post-condition; if the value is null, throws an exception that should never be catch.
        /// </summary>
        /// <remarks>
        ///     Run in "debug" mode only to not affect performance.
        /// </remarks>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="message">The error message to display.</param>
        /// <exception cref="GuardClauseException">If the given value is null.</exception>
        [Conditional("DEBUG")]
        public static void EnsuresNonNull<T>(T value, string message) =>
            Ensures(() => value != null, message);

        /// <summary>
        ///     Checks if a sequence contains null items as a method post-condition; if any item is null, throws an exception that should never be catch.
        /// </summary>
        /// <remarks>
        ///     Run in "debug" mode only to not affect performance.
        /// </remarks>
        /// <typeparam name="T">The type of the sequence items.</typeparam>
        /// <param name="sequence">The sequence to check.</param>
        /// <param name="message">The error message to display.</param>
        /// <exception cref="GuardClauseException">If any items the sequence is null.</exception>
        [Conditional("DEBUG")]
        public static void EnsuresNoNullIn<T>(IEnumerable<T> sequence, string message) =>
            EnsuresAll(sequence, item => item != null, message);

        /// <summary>
        ///     Checks for a method pre-condition; if the condition is false, throws an exception that should never be catch.
        /// </summary>
        /// <param name="predicate">The condition to validate.</param>
        /// <param name="message">The error message to display.</param>
        /// <exception cref="GuardClauseException">If the specified condition is not satisfied.</exception>
        public static void Requires(Func<bool> predicate, string message) =>
            Assert(predicate, message);

        /// <summary>
        ///     Checks for a method pre-condition against all items in a sequence; if any condition is false, throws an exception that should never be catch.
        /// </summary>
        /// <remarks>
        ///     Run in "debug" mode only to not affect performance.
        /// </remarks>
        /// <typeparam name="T">The type of the sequence items.</typeparam>
        /// <param name="sequence">The sequence to check.</param>
        /// <param name="predicate">The condition to validate.</param>
        /// <param name="message">The error message to display.</param>
        /// <exception cref="GuardClauseException">If one or more items of the sequence doesn't satisfy the specified condition.</exception>
        [Conditional("DEBUG")]
        public static void RequiresAll<T>(
            IEnumerable<T> sequence,
            Func<T, bool> predicate, string message) =>
            Assert(() => sequence.All(predicate), message);

        /// <summary>
        ///     Checks for a method pre-condition against all items in a sequence; if all conditions are false, throws an exception that should never be catch.
        /// </summary>
        /// <remarks>
        ///     Run in "debug" mode only to not affect performance.
        /// </remarks>
        /// <typeparam name="T">The type of the sequence items.</typeparam>
        /// <param name="sequence">The sequence to check.</param>
        /// <param name="predicate">The condition to validate.</param>
        /// <param name="message">The error message to display.</param>
        /// <exception cref="GuardClauseException">If all items in the sequence doesn't satisfy the specified condition.</exception>
        [Conditional("DEBUG")]
        public static void RequiresAny<T>(
            IEnumerable<T> sequence,
            Func<T, bool> predicate, string message) =>
            Requires(() => sequence.Any(predicate), message);

        /// <summary>
        ///     Checks if a value is null as a method post-condition; if the value is null, throws an exception that should never be catch.
        /// </summary>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="variableName">The variable name of the value to check.</param>
        /// <exception cref="GuardClauseException">If the given value is null.</exception>
        public static void RequiresNonNull<T>(T value, string variableName) =>
            Requires(() => value != null, $"Receiving null {variableName}.");

        /// <summary>
        ///     Checks if a sequence contains null items as a method pre-condition; if any item is null, throws an exception that should never be catch.
        /// </summary>
        /// <remarks>
        ///     Run in "debug" mode only to not affect performance.
        /// </remarks>
        /// <typeparam name="T">The type of the sequence items.</typeparam>
        /// <param name="sequence">The sequence to check.</param>
        /// <param name="variableName">The variable name of the sequence to check.</param>
        /// <exception cref="GuardClauseException">If any items the sequence is null.</exception>
        [Conditional("DEBUG")]
        public static void RequiresNoNullIn<T>(IEnumerable<T> sequence, string variableName) =>
            RequiresAll(sequence, item => item != null, $"Receiving {variableName} containing one or more null items.");

        private static void Assert(Func<bool> predicate, string message)
        {
            if (predicate())
                return;

            throw new GuardClauseException(message);
        }
    }
}
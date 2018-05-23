using SolidStack.Core.Guards;

namespace SolidStack.Core.Flow
{
    public static class Option
    {
        /// <summary>
        ///     Creates an option using an existing value; if the value is null, the option is created with no value.
        /// </summary>
        /// <param name="optionnal"></param>
        /// <returns>The created option.</returns>
        public static IOption<T> From<T>(T optionnal) =>
            Option<T>.From(optionnal);

        /// <summary>
        ///     Creates an option that doesn't contain a value.
        /// </summary>
        /// <returns>The created option.</returns>
        public static IOption<T> None<T>() =>
            Option<T>.None();

        /// <summary>
        ///     Creates an option that contain a value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The created option.</returns>
        /// <exception cref="GuardClauseException">If the given value is null.</exception>
        public static IOption<T> Some<T>(T value) =>
            Option<T>.Some(value);
    }
}
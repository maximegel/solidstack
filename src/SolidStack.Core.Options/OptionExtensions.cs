using System;

namespace SolidStack.Core.Options
{
    public static class OptionExtensions
    {
        public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> map) =>
            option is Some<T> some ? (Option<TResult>) map(some) : Option.None;

        public static T Reduce<T>(this Option<T> option, T whenNone) =>
            option is Some<T> some ? (T) some : whenNone;

        public static T Reduce<T>(this Option<T> option, Func<T> whenNone) =>
            option is Some<T> some ? (T) some : whenNone();

        public static Option<T> When<T>(this Option<T> option, Func<T, bool> predicate) =>
            option is Some<T> some && predicate(some) ? (Option<T>) some : Option.None;
    }
}
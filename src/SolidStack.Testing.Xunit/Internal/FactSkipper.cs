using Xunit;

namespace SolidStack.Testing.Xunit.Internal
{
    internal static class FactSkipper
    {
        public static void SkipInRelease(FactAttribute fact) =>
            SkipInRelease(fact, "Only running in debug mode.");

        public static void SkipInRelease(FactAttribute fact, string message)
        {
            #if !DEBUG
            fact.Skip = message;
            #endif
        }
    }
}

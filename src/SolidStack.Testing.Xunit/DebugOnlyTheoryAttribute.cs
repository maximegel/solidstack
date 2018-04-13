using SolidStack.Testing.Xunit.Internal;
using Xunit;

namespace SolidStack.Testing.Xunit
{
    public class DebugOnlyTheoryAttribute : TheoryAttribute
    {
        public DebugOnlyTheoryAttribute() =>
            FactSkipper.SkipInRelease(this);
    }
}

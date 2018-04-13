using SolidStack.Testing.Xunit.Internal;
using Xunit;

namespace SolidStack.Testing.Xunit
{
    public class DebugOnlyFactAttribute : FactAttribute
    {
        public DebugOnlyFactAttribute() =>
            FactSkipper.SkipInRelease(this);
    }
}

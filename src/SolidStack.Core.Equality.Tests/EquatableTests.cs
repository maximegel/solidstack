using System.Collections.Generic;
using Moq;
using SolidStack.Core.Equality.Testing;
using SolidStack.Core.Equality.Tests.Doubles;
using Xunit;
namespace SolidStack.Core.Equality.Tests
{
    public class EquatableTests
    {

        [Fact]
        public void Equatable_OverridesEquality()
        {
            var comparer = new Mock<IEqualityComparer<EquatableStub>>();
            comparer.Setup(mock => mock.Equals(It.IsAny<EquatableStub>(), It.IsAny<EquatableStub>())).Returns(true);
            comparer.Setup(mock => mock.GetHashCode()).Returns(42);
            var equatable = new EquatableStub(() => comparer.Object);

            equatable.Should()
                .BeTypeSealed()
                .And.OverrideEquality();
        }

    }
}
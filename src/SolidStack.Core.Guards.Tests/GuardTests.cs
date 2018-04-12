using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using SolidStack.Core.Guards;
using SolidStack.Core.Guards.Internal;
using Xunit;

namespace SolidStack.Core.Tests.Guards
{
    public class GuardTests
    {
        [Fact]
        public void Requires_WithMatchedPredicate_DoesntThrows()
        {
            Action act = () => Guard.Requires(() => true, "message");

            act.Should().NotThrow<GuardClauseException>();
        }

        [Fact]
        public void Requires_WithUnmatchedPredicate_Throws()
        {
            Action act = () => Guard.Requires(() => false, "message");

            act.Should().Throw<GuardClauseException>();
        }

        [Fact]
        public void RequiresNonNull_WithNonNull_DoesntThrows()
        {
            const string nonNull = "";
            Action act = () => Guard.RequiresNonNull(nonNull, "message");

            act.Should().NotThrow<GuardClauseException>();
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void RequiresNonNull_WithNull_Throws()
        {
            string @null = null;
            Action act = () => Guard.RequiresNonNull(@null, "message");

            act.Should().Throw<GuardClauseException>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using SolidStack.Testing.Xunit;
using Xunit;

namespace SolidStack.Core.Guards.Tests
{
    public class GuardTests
    {
        [Fact]
        public void Ensures_WithMatchedPredicate_DoesntThrows()
        {
            Action act = () => Guard.Ensures(() => true, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [DebugOnlyFact]
        public void Ensures_WithUnmatchedPredicateInDebug_Throws()
        {
            Action act = () => Guard.Ensures(() => false, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Theory]
        [InlineData(new[] { 0, 4, 6, 10 })]
        public void EnsuresAll_WithMatchedPredicates_DoesntThrows(IEnumerable<int> sequence)
        {
            Action act = () => Guard.EnsuresAll(sequence, n => n >= 0, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [DebugOnlyTheory]
        [InlineData(new[] { -4, 0, 6, 10 })]
        [InlineData(new[] { -2, -3, -5, -3 })]
        public void EnsuresAll_WithUnmatchedPredicatesInDebug_Throws(IEnumerable<int> sequence)
        {
            Action act = () => Guard.EnsuresAll(sequence, n => n >= 0, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Theory]
        [InlineData(new[] { 0, 4, 6, 10 })]
        [InlineData(new[] { -2, -3, 5, -3 })]
        [InlineData(new[] { -2, 0, -7, -5 })]
        public void EnsuresAny_WithMatchedPredicates_DoesntThrows(IEnumerable<int> sequence)
        {
            Action act = () => Guard.EnsuresAny(sequence, n => n >= 0, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [DebugOnlyTheory]
        [InlineData(new[] { -2, -3, -5, -3 })]
        public void EnsuresAny_WithUnmatchedPredicatesInDebug_Throws(IEnumerable<int> sequence)
        {
            Action act = () => Guard.EnsuresAny(sequence, n => n >= 0, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Fact]
        public void EnsuresNonNull_WithNonNull_DoesntThrows()
        {
            const string nonNull = "";
            Action act = () => Guard.EnsuresNonNull(nonNull, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [DebugOnlyFact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void EnsuresNonNull_WithNullInDebug_Throws()
        {
            string @null = null;
            Action act = () => Guard.EnsuresNonNull(@null, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Fact]
        public void EnsuresNoNullIn_WithNoNull_DoesntThrows()
        {
            var sequence = new[] { "foo", "bar", "baz" };
            Action act = () => Guard.EnsuresNoNullIn(sequence, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [DebugOnlyFact]
        public void EnsuresNoNullIn_WithNullsInDebug_Throws()
        {
            var sequence = new[] { "foo", null, "baz" };
            Action act = () => Guard.EnsuresNoNullIn(sequence, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Fact]
        public void Requires_WithMatchedPredicate_DoesntThrows()
        {
            Action act = () => Guard.Requires(() => true, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [Fact]
        public void Requires_WithUnmatchedPredicate_Throws()
        {
            Action act = () => Guard.Requires(() => false, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Theory]
        [InlineData(new[] { 0, 4, 6, 10 })]
        public void RequiresAll_WithMatchedPredicates_DoesntThrows(IEnumerable<int> sequence)
        {
            Action act = () => Guard.RequiresAll(sequence, n => n >= 0, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [DebugOnlyTheory]
        [InlineData(new[] { -4, 0, 6, 10 })]
        [InlineData(new[] { -2, -3, -5, -3 })]
        public void RequiresAll_WithUnmatchedPredicates_Throws(IEnumerable<int> sequence)
        {
            Action act = () => Guard.RequiresAll(sequence, n => n >= 0, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Theory]
        [InlineData(new[] { 0, 4, 6, 10 })]
        [InlineData(new[] { -2, -3, 5, -3 })]
        [InlineData(new[] { -2, 0, -7, -5 })]
        public void RequiresAny_WithMatchedPredicates_DoesntThrows(IEnumerable<int> sequence)
        {
            Action act = () => Guard.RequiresAny(sequence, n => n >= 0, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [DebugOnlyTheory]
        [InlineData(new[] { -2, -3, -5, -3 })]
        public void RequiresAny_WithUnmatchedPredicates_Throws(IEnumerable<int> sequence)
        {
            Action act = () => Guard.RequiresAny(sequence, n => n >= 0, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Fact]
        public void RequiresNonNull_WithNonNull_DoesntThrows()
        {
            const string nonNull = "";
            Action act = () => Guard.RequiresNonNull(nonNull, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void RequiresNonNull_WithNull_Throws()
        {
            string @null = null;
            Action act = () => Guard.RequiresNonNull(@null, "error");

            act.Should().Throw<GuardClauseException>();
        }

        [Fact]
        public void RequiresNoNullIn_WithNoNull_DoesntThrows()
        {
            var sequence = new[] { "foo", "bar", "baz" };
            Action act = () => Guard.RequiresNoNullIn(sequence, "error");

            act.Should().NotThrow<GuardClauseException>();
        }

        [DebugOnlyFact]
        public void RequiresNoNullIn_WithNulls_Throws()
        {
            var sequence = new[] { "foo", null, "baz" };
            Action act = () => Guard.RequiresNoNullIn(sequence, "error");

            act.Should().Throw<GuardClauseException>();
        }
    }
}
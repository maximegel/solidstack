using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace SolidStack.Core.Flow.Tests
{
    public class OptionAdaptersTests
    {
        [Fact]
        public void AsEnumerable_WhenNone_ReturnsAnEmptySequence()
        {
            var option = Option.None<int>();
            var sequence = option.AsEnumerable();

            sequence.Should().BeEmpty();
        }

        [Fact]
        public void AsEnumerable_WhenSome_ReturnsASequenceOfOneElement()
        {
            var option = Option.Some(5);
            var sequence = option.AsEnumerable();

            sequence.Should()
                .ContainSingle(x => x == 5)
                .And.HaveCount(1);
        }

        [Fact]
        public void TryFirst_WithEmptySequence_ReturnsNone()
        {
            var sequence = Enumerable.Empty<int>();
            var option = sequence.TryFirst();

            option.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void TryFirst_WithMatchingPredicate_ReturnsSome()
        {
            var sequence = new[] {4, 5};
            var option = sequence.TryFirst(x => x < 5);

            option.AsEnumerable().Should().ContainSingle(x => x == 4);
        }

        [Fact]
        public void TryFirst_WithSequenceOfOneOrMoreElements_ReturnsSome()
        {
            var sequence = new[] {4, 5};
            var option = sequence.TryFirst();

            option.AsEnumerable().Should().ContainSingle(x => x == 4);
        }

        [Fact]
        public void TryFirst_WithUnmatchingPredicate_ReturnsNone()
        {
            var sequence = Enumerable.Empty<int>();
            var option = sequence.TryFirst(x => x >= 5);

            option.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void TryGetValue_WithExistingKey_ReturnsSome()
        {
            var dictionary = new Dictionary<string, int> {{"foo", 7}, {"bar", 3}};
            var option = dictionary.TryGetValue("foo");

            option.AsEnumerable().Should().ContainSingle(x => x == 7);
        }

        [Fact]
        public void TryGetValue_WithUnexistingKey_ReturnsNone()
        {
            var dictionary = new Dictionary<string, int> {{"foo", 7}, {"bar", 3}};
            var option = dictionary.TryGetValue("baz");

            option.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void TrySingle_WhenEmptySequence_ReturnsNone()
        {
            var sequence = Enumerable.Empty<int>();
            var option = sequence.TrySingle();

            option.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void TrySingle_WithPredicateThatMatchManyElements_ReturnsNone()
        {
            var sequence = new[] {-2, 4, 5};
            var option = sequence.TrySingle(x => x < 5);

            option.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void TrySingle_WithPredicateThatMatchOneElement_ReturnsSome()
        {
            var sequence = new[] {4, 5};
            var option = sequence.TrySingle(x => x < 5);

            option.AsEnumerable().Should().ContainSingle(x => x == 4);
        }

        [Fact]
        public void TrySingle_WithSequenceOfMoreThenOneElement_ReturnsNone()
        {
            var sequence = new[] {4, 5};
            var option = sequence.TrySingle();

            option.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void TrySingle_WithUnmatchingPredicate_ReturnsNone()
        {
            var sequence = Enumerable.Empty<int>();
            var option = sequence.TrySingle(x => x >= 5);

            option.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void WhereSome_FiltersTheEmptyOptions()
        {
            var options = new[] {Option.Some(4), Option.None<int>(), Option.Some(5), Option.None<int>()};
            var values = options.WhereSome();

            values.Should().HaveCount(2).And.ContainInOrder(4, 5);
        }

        [Fact]
        public void WhereSome_WithPredicate_FiltersTheEmptyOptions()
        {
            var sequence = new[] {4, -1, 5, -3};
            var values = sequence.WhereSome(x => x >= 0 ? Option.Some(x) : Option.None<int>());

            values.Should().HaveCount(2).And.ContainInOrder(4, 5);
        }
    }
}
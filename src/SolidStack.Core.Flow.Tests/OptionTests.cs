using FluentAssertions;
using Xunit;

namespace SolidStack.Core.Flow.Tests
{
    public class OptionTests
    {
        [Fact]
        public void Do_OnMatchedWhereClause_FallThrough()
        {
            var count = 0;
            var option = Option.Some(5);

            option
                .When(x => x > 3)
                .Do(x => count++)
                .WhenSome()
                .Do(x => count++)
                .Execute();

            count.Should().Be(2);
        }

        [Fact]
        public void Do_OnNone_FallThrough()
        {
            var count = 0;
            var option = Option.None<int>();

            option
                .WhenNone()
                .Do(() => count++)
                .WhenNone()
                .Do(() => count++)
                .Execute();

            count.Should().Be(2);
        }

        [Fact]
        public void Do_OnSome_FallThrough()
        {
            var count = 0;
            var option = Option.Some(5);

            option
                .WhenSome()
                .Do(x => count++)
                .WhenSome()
                .Do(x => count++)
                .Execute();

            count.Should().Be(2);
        }

        [Fact]
        public void Do_OnUnmatchedWhereClause_FallThrough()
        {
            var count = 0;
            var option = Option.Some(5);

            option
                .When(x => x < 3)
                .Do(x => count++)
                .WhenSome()
                .Do(x => count++)
                .Execute();

            count.Should().Be(1);
        }

        [Fact]
        public void Do_WhenMatchesAndServesInputValueToLambda_ReturnsOptionValue()
        {
            var touched = false;
            var value = 0;
            var option = Option.Some(5);

            option
                .When(x => x > 6)
                .Do(x => touched = true)
                .When(x => x > 3)
                .Do(x => value = x)
                .Execute();

            touched.Should().BeFalse();
            value.Should().Be(5);
        }

        [Fact]
        public void Do_WhenSomeMatchesAndServesInputValueToLambda_ReturnsOptionValue()
        {
            var touched = false;
            var value = 0;
            var option = Option.Some(5);

            option
                .When(x => x > 6)
                .Do(x => touched = true)
                .WhenSome()
                .Do(x => value = x)
                .Execute();

            touched.Should().BeFalse();
            value.Should().Be(5);
        }

        [Fact]
        public void Execute_WhenDoesntMatchesSome_DoesntExecuteDoAction()
        {
            var touched = false;
            var option = Option.Some(5);

            option
                .When(x => x > 6)
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeFalse();
        }

        [Fact]
        public void Execute_WhenMatchesSome_ExecuteDoAction()
        {
            var touched = false;
            var option = Option.Some(5);

            option
                .When(x => x > 3)
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeTrue();
        }

        [Fact]
        public void Execute_WhenNoneMatchesNone_DoesntExecuteDoAction()
        {
            var touchedAfterWhen = false;
            var touchedAfterWhenNone = false;
            var option = Option.None<int>();

            option
                .When(x => true)
                .Do(x => touchedAfterWhen = true)
                .WhenNone()
                .Do(() => touchedAfterWhenNone = true)
                .Execute();

            touchedAfterWhen.Should().BeFalse();
            touchedAfterWhenNone.Should().BeTrue();
        }

        [Fact]
        public void Execute_WhenNoneOfWhenMatchesSome_DoesntExecuteDoActions()
        {
            var touchedAfterFirstWhen = false;
            var touchedAfterSecondWhen = false;
            var option = Option.Some(5);

            option
                .When(x => x > 6)
                .Do(x => touchedAfterFirstWhen = true)
                .When(x => x > 7)
                .Do(x => touchedAfterSecondWhen = true)
                .Execute();

            touchedAfterFirstWhen.Should().BeFalse();
            touchedAfterSecondWhen.Should().BeFalse();
        }

        [Fact]
        public void Execute_WhenNotMatchingNone_DoesntExecuteDoAction()
        {
            var touched = false;
            var option = Option.None<int>();

            option
                .When(x => true)
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeFalse();
        }

        [Fact]
        public void Execute_WhenSomeMatched_ExecutesAllSomeAndWhereAction()
        {
            var touchedAfterWhen = false;
            var touchedAfterWhenSome = false;
            var option = Option.Some(5);

            option
                .WhenSome()
                .Do(x => touchedAfterWhenSome = true)
                .When(x => x > 3)
                .Do(x => touchedAfterWhen = true)
                .Execute();

            touchedAfterWhenSome.Should().BeTrue();
            touchedAfterWhen.Should().BeTrue();
        }

        [Fact]
        public void MapTo_OnMatchedWhereClause_DoesntFallThrough()
        {
            var option = Option.Some(5);

            var result =
                option
                    .When(x => x > 3)
                    .MapTo(x => $"{x} > 3")
                    .When(x => x > 2)
                    .MapTo(x => "x > 2")
                    .WhenSome()
                    .MapTo(x => "some")
                    .WhenNone()
                    .MapTo(() => "none")
                    .Map();

            result.Should().Be("5 > 3");
        }

        [Fact]
        public void MapTo_OnNone_ReturnsMappedResult()
        {
            var option = Option.None<int>();

            var result =
                option
                    .When(x => x > 3)
                    .MapTo(x => "x > 3")
                    .WhenSome()
                    .MapTo(x => "some")
                    .WhenNone()
                    .MapTo(() => "none")
                    .Map();

            result.Should().Be("none");
        }

        [Fact]
        public void MapTo_OnSome_ReturnsMappedResult()
        {
            var option = Option.Some(5);

            var result =
                option
                    .WhenSome()
                    .MapTo(x => "some")
                    .WhenNone()
                    .MapTo(() => "none")
                    .Map();

            result.Should().Be("some");
        }

        [Fact]
        public void MapTo_OnUnmatchedWhereClause_DoesntFallThrough()
        {
            var option = Option.Some(5);

            var result =
                option
                    .When(x => x != 5)
                    .MapTo(x => $"x != 5")
                    .When(x => x > 2)
                    .MapTo(x => "x > 2")
                    .WhenSome()
                    .MapTo(x => "some")
                    .WhenNone()
                    .MapTo(() => "none")
                    .Map();

            result.Should().Be("x > 2");
        }
    }
}
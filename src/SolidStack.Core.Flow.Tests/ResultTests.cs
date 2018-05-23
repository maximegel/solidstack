using FluentAssertions;
using Xunit;

namespace SolidStack.Core.Flow.Tests
{
    public class ResultTests
    {
        [Fact]
        public void Do_OnError_FallThrough()
        {
            var count = 0;
            var result = Result<string, string>.Error("error");

            result
                .WhenError()
                .Do(x => count++)
                .WhenError()
                .Do(x => count++)
                .Execute();

            count.Should().Be(2);
        }

        [Fact]
        public void Do_OnSuccess_FallThrough()
        {
            var count = 0;
            var result = Result<string, string>.Success("success");

            result
                .WhenSuccess()
                .Do(x => count++)
                .WhenSuccess()
                .Do(x => count++)
                .Execute();

            count.Should().Be(2);
        }

        [Fact]
        public void Do_WhenErrorMatchesAndServesInputValueToLambda_ReturnsErrorValue()
        {
            var value = "";
            var result = Result<string, string>.Error("error");

            result
                .WhenError()
                .Do(x => value = x)
                .Execute();

            value.Should().Be("error");
        }

        [Fact]
        public void Do_WhenSpecificErrorMatchesAndServesInputValueToLambda_ReturnsSpecificErrorValue()
        {
            var value = "";
            var result = Result<object, object>.Error("error");

            result
                .WhenError<string>()
                .Do(x => value = x)
                .Execute();

            value.Should().Be("error");
        }

        [Fact]
        public void Do_WhenSuccessMatchesAndServesInputValueToLambda_ReturnsSuccessValue()
        {
            var value = "";
            var result = Result<string, string>.Success("success");

            result
                .WhenSuccess()
                .Do(x => value = x)
                .Execute();

            value.Should().Be("success");
        }

        [Fact]
        public void Execute_WhenErrorDoesntMatches_DoesntExecuteDoAction()
        {
            var touched = false;
            var result = Result<string, string>.Success("success");

            result
                .WhenError()
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeFalse();
        }

        [Fact]
        public void Execute_WhenErrorMatches_ExecuteDoAction()
        {
            var touched = false;
            var result = Result<string, string>.Error("error");

            result
                .WhenError()
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeTrue();
        }

        [Fact]
        public void Execute_WhenSpecificErrorDoesntMatches_DoesntExecuteDoAction()
        {
            var touched = false;
            var result = Result<object, object>.Error("error");

            result
                .WhenError<int>()
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeFalse();
        }

        [Fact]
        public void Execute_WhenSpecificErrorMatches_ExecuteDoAction()
        {
            var touched = false;
            var result = Result<object, object>.Error("error");

            result
                .WhenError<string>()
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeTrue();
        }

        [Fact]
        public void Execute_WhenSuccessDoesntMatches_DoesntExecuteDoAction()
        {
            var touched = false;
            var result = Result<string, string>.Error("error");

            result
                .WhenSuccess()
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeFalse();
        }

        [Fact]
        public void Execute_WhenSuccessMatches_ExecuteDoAction()
        {
            var touched = false;
            var result = Result<string, string>.Success("success");

            result
                .WhenSuccess()
                .Do(x => touched = true)
                .Execute();

            touched.Should().BeTrue();
        }

        [Fact]
        public void MapTo_OnError_ReturnsMappedResult()
        {
            var result = Result<object, object>.Error("error");

            var mappedResult =
                result
                    .WhenError<int>()
                    .MapTo(x => 2)
                    .WhenError()
                    .MapTo(x => 1)
                    .WhenSuccess()
                    .MapTo(x => 0)
                    .Map();

            mappedResult.Should().Be(1);
        }

        [Fact]
        public void MapTo_OnMatchedSpecificError_DoesntFallThrough()
        {
            var result = Result<object, object>.Error("error");

            var mappedResult =
                result
                    .WhenError<string>()
                    .MapTo(x => "string error")
                    .WhenError()
                    .MapTo(x => "error")
                    .WhenSuccess()
                    .MapTo(x => "success")
                    .Map();

            mappedResult.Should().Be("string error");
        }

        [Fact]
        public void MapTo_OnSpecificError_ReturnsMappedResult()
        {
            var result = Result<object, object>.Error("error");

            var mappedResult =
                result
                    .WhenError<string>()
                    .MapTo(x => 2)
                    .WhenError()
                    .MapTo(x => 1)
                    .WhenSuccess()
                    .MapTo(x => 0)
                    .Map();

            mappedResult.Should().Be(2);
        }

        [Fact]
        public void MapTo_OnSuccess_ReturnsMappedResult()
        {
            var result = Result<string, string>.Success("success");

            var mappedResult =
                result
                    .WhenSuccess()
                    .MapTo(x => 0)
                    .WhenError()
                    .MapTo(x => 1)
                    .Map();

            mappedResult.Should().Be(0);
        }

        [Fact]
        public void MapTo_UnmatchedSpecificError_DoesntFallThrough()
        {
            var result = Result<object, object>.Error("error");

            var mappedResult =
                result
                    .WhenError<int>()
                    .MapTo(x => "int error")
                    .WhenError()
                    .MapTo(x => "error")
                    .WhenSuccess()
                    .MapTo(x => "success")
                    .Map();

            mappedResult.Should().Be("error");
        }
    }
}
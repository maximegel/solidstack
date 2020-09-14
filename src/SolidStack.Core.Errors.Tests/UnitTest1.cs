using System;
using Xunit;

namespace SolidStack.Core.Errors.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
           var s = new Try<Exception, string>(() => "1) success;", e => e)
                .Catch<InvalidOperationException>(e => "1) error: invalid operation;")
                .Catch(e => "1) error: unknown;")
                .Then(result => new Try<Exception, string>(() => throw new InvalidOperationException(), e => e))
                .Catch<InvalidOperationException>(e => "2) error: invalid operation;")
                .Catch(e => "2) error: unknown;")
                .Invoke();
        }
    }
}

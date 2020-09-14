using System;
using FluentAssertions;
using Xunit;

namespace SolidStack.Core.Promises.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var ok = true;
            //Promise.Create<Exception, int>(
            //        (reject, resolve) => ok ? resolve(0) : reject(new ArgumentException()),
            //        exception => exception)
            //    .Then<Exception, int>(count => count + 1)
            //    .Handle<ArgumentException>(e => -3, e => e.ParamName == "x")
            //    .Handle<ArgumentException>(e => -2)
            //    .Then(count => Promise.FromResult<Exception, int>(count + 1))
            //    .Handle(e => -1)
            //    .ExecuteAsync()
            //    .Should().Be(2);
        }
    }
}
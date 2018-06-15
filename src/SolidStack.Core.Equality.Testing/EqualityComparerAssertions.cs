using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;

namespace SolidStack.Core.Equality.Testing
{
    public class EqualityComparerAssertions<T>
        where T : class
    {
        public EqualityComparerAssertions(IEqualityComparer<T> equalityComparer) =>
            Subject = equalityComparer;

        public IEqualityComparer<T> Subject { get; protected set; }

        public AndConstraint<EqualityComparerAssertions<T>> HandleBasicEqualitiesAndInequalites(
            string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => new Mock<T>().Object)
                .ForCondition(dummy => Subject.Equals(dummy, dummy))
                .FailWith(
                    "Expected {context:comparer} to evaluate the equality of the same object as {0}{reason}, but found {1}.",
                    true, false)
                .Then
                .ForCondition(dummy => !Subject.Equals(null, dummy) && !Subject.Equals(dummy, null))
                .FailWith(
                    "Expected {context:comparer} to evaluate the equality of null and a non-null object as {0}{reason}, but found {1}.",
                    false, true)
                .Then
                .Given(_ =>
                {
                    var xStub = new Mock<T>();
                    var yStub = new Mock<T>();

                    xStub.Setup(obj => obj.GetType()).Returns(typeof(object));

                    return new[] {xStub.Object, yStub.Object};
                })
                .ForCondition(stubs => Subject.Equals(stubs[0], stubs[1]))
                .FailWith("Expected {context:comparer} to evaluate the equality of objects with a different type as {0}{reason}, but found {1}.",
                    true, false);

            return new AndConstraint<EqualityComparerAssertions<T>>(this);
        }

        public AndConstraint<EqualityComparerAssertions<T>> InvalidateEqualityOf(
            T x, T y, string because = "", params object[] becauseArgs)
        {
            var xCopy = x;
            var yCopy = y;

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(!Subject.Equals(x, y))
                .FailWith(
                    "Expected {context:comparer} to evaluate the equality of {0} and {1} as {2}{reason}, but found {3}.",
                    x, y, false, true)
                .Then
                .Given(() => new[] {Subject.GetHashCode(x), Subject.GetHashCode(y)})
                .ForCondition(hashCodes => hashCodes[0] != hashCodes[1])
                .FailWith(
                    "Expected {context:comparer} to return the same hash code for {0} and {1}{reason}, but found {2} and {3}.",
                    _ => xCopy, _ => yCopy, hashCodes => hashCodes[0], hashCodes => hashCodes[1]);

            return new AndConstraint<EqualityComparerAssertions<T>>(this);
        }

        public AndConstraint<EqualityComparerAssertions<T>> ValidateEqualityOf(
            T x, T y, string because = "", params object[] becauseArgs)
        {
            var xCopy = x;
            var yCopy = y;

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Equals(x, y))
                .FailWith(
                    "Expected {context:comparer} to evaluate the equality of {0} and {1} as {2}{reason}, but found {3}.",
                    x, y, true, false)
                .Then
                .Given(() => new[] {Subject.GetHashCode(x), Subject.GetHashCode(y)})
                .ForCondition(hashCodes => hashCodes[0] == hashCodes[1])
                .FailWith(
                    "Expected {context:comparer} to return the same has code for {0} and {1}{reason}, but found {2} and {3}.",
                    _ => xCopy, _ => yCopy, hashCodes => hashCodes[0], hashCodes => hashCodes[1]);

            return new AndConstraint<EqualityComparerAssertions<T>>(this);
        }
    }
}
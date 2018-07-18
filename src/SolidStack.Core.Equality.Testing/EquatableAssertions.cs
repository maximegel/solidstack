using System;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;

namespace SolidStack.Core.Equality.Testing
{
    public class EquatableAssertions<T>
    {
        public EquatableAssertions(IEquatable<T> equatable) =>
            Subject = equatable;

        public IEquatable<T> Subject { get; protected set; }

        private TypeInfo SubjectType =>
            Subject.GetType().GetTypeInfo();

        public AndConstraint<EquatableAssertions<T>> BeTypeSealed(
            string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.GetType().IsSealed)
                .FailWith($"Expected {{context:{Subject.GetType().Name}}} to be type sealed{{reason}}.");

            return new AndConstraint<EquatableAssertions<T>>(this);
        }

        public AndConstraint<EquatableAssertions<T>> OverrideEquality(
            string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(OverridesMethod("Equals", new []{typeof(object)}))
                .FailWith($"Expected {{context:{Subject.GetType().Name}}} to override Equals(object){{reason}}.", Subject.GetType())
                .Then
                .ForCondition(OverridesMethod("GetHashCode", Type.EmptyTypes))
                .FailWith($"Expected {{context:{Subject.GetType().Name}}} to override GetHashCode(){{reason}}.", Subject.GetType())
                .Then
                .ForCondition(OverridesOperator("op_Equality"))
                .FailWith($"Expected {{context:{Subject.GetType().Name}}} to override equality operator{{reason}}.", Subject.GetType())
                .Then
                .ForCondition(OverridesOperator("op_Inequality"))
                .FailWith($"Expected {{context:{Subject.GetType().Name}}} to override inequality operator{{reason}}.", Subject.GetType());

            return new AndConstraint<EquatableAssertions<T>>(this);
        }

        private bool OverridesMethod(string methodName, Type[] types) =>
            SubjectType
                .GetMethod(methodName, types)
                ?.DeclaringType != typeof(object);

        private bool OverridesOperator(string operatorName) =>
            SubjectType
                .GetMethod(
                    operatorName,
                    BindingFlags.Instance |
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.FlattenHierarchy) != null;
    }
}
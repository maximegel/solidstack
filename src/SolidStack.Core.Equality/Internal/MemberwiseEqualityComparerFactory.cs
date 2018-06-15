using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolidStack.Core.Equality.Internal
{
    internal static class MemberwiseEqualityComparerFactory
    {
        private const int HashCodeSeed = 29;

        private const int HashCodeMultiplier = 103;

        private static BindingFlags AllInstanceMembersBindingFlags =>
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private static MethodInfo ObjectEqualsMethod { get; } =
            new Func<object, object, bool>(Equals).GetMethodInfo();

        public static IEqualityComparer<T> Create<T>(
            Func<IEnumerable<MemberInfo>, IEnumerable<MemberInfo>> memberSelector)
            where T : class =>
            Create<T>(memberSelector(GetMembers<T>()));

        private static IEqualityComparer<T> Create<T>(IEnumerable<MemberInfo> members)
            where T : class
        {
            var memberList = members.ToList();

            return new EqualityComparerFunc<T>(
                MakeEqualsMethod<T>(memberList),
                MakeGetHashCodeMethod<T>(memberList));
        }

        private static IEnumerable<MemberInfo> GetMembers<T>()
        {
            var type = typeof(T).GetTypeInfo();

            return type
                .GetFields(AllInstanceMembersBindingFlags)
                .Cast<MemberInfo>()
                .Concat(type.GetProperties(AllInstanceMembersBindingFlags))
                .Where(member => !member.Name.EndsWith(">k__BackingField"));
        }

        private static bool IsSequenceType(Type type) =>
            typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type) && type != typeof(string);

        private static Expression LinkHashCodeExpression(Expression x, Expression y)
        {
            var xMultipliedExpr = Expression.Multiply(x, Expression.Constant(HashCodeMultiplier));
            return Expression.ExclusiveOr(xMultipliedExpr, y);
        }

        private static Expression MakeEqualsExpression(MemberInfo member, Expression x, Expression y)
        {
            var xMemberExpr = Expression.MakeMemberAccess(x, member);
            var yMemberExpr = Expression.MakeMemberAccess(y, member);

            var memberType = xMemberExpr.Type;

            // We create an expression that invoke Enumerable.SequenceEqual if obj is a sequence or object.Equals if not.
            if (!xMemberExpr.Type.GetTypeInfo().IsValueType)
                return IsSequenceType(memberType)
                    ? MakeSequenceTypeEqualExpression(xMemberExpr, yMemberExpr, memberType)
                    : MakeReferenceTypeEqualExpression(xMemberExpr, yMemberExpr);

            var xMemberAsObjExpr = Expression.Convert(xMemberExpr, typeof(object));
            var yMemberAsObjExpr = Expression.Convert(yMemberExpr, typeof(object));

            return MakeReferenceTypeEqualExpression(xMemberAsObjExpr, yMemberAsObjExpr);
        }

        private static Func<T, T, bool> MakeEqualsMethod<T>(IEnumerable<MemberInfo> members)
        {
            // We create the method parameters.
            var xParamAsObjExpr = Expression.Parameter(typeof(object), "x");
            var yParamAsObjExpr = Expression.Parameter(typeof(object), "y");

            // We cast the parameters to the concrete type.
            var xParamExpr = Expression.Convert(xParamAsObjExpr, typeof(T));
            var yParamExpr = Expression.Convert(xParamAsObjExpr, typeof(T));

            // We create the AND expression using short-circuit evaluation.
            var equalsExprs = members.Select(member => MakeEqualsExpression(member, xParamExpr, yParamExpr));
            var andChainExpr = equalsExprs.Aggregate((Expression) Expression.Constant(true), Expression.AndAlso);

            // We use Object.Equals if second parameter doesn't match type.
            var objectEqualsExpr = Expression.Equal(xParamAsObjExpr, yParamAsObjExpr);
            var typedEqualsOrUntypedEqualsExpr = Expression.Condition(
                Expression.TypeIs(yParamAsObjExpr, typeof(T)),
                andChainExpr,
                objectEqualsExpr);

            // We compile the lambda expression into a function.
            return Expression.Lambda<Func<T, T, bool>>(
                    typedEqualsOrUntypedEqualsExpr, xParamAsObjExpr, yParamAsObjExpr)
                .Compile();
        }

        private static Expression MakeGetHashCodeExpression(MemberInfo member, Expression obj)
        {
            var memberExpr = Expression.MakeMemberAccess(obj, member);
            var memberAsObjExpr = Expression.Convert(memberExpr, typeof(object));

            var memberType = memberExpr.Type;

            // We create an expression that invoke EnumerableExtensions.GetSequenceHashCode if obj is a sequence or obj.GetHashCode if not.
            var getHashCodeExpr = IsSequenceType(memberType)
                ? MakeSequenceTypeGetHashCodeExpression(memberExpr)
                : MakeReferenceTypeGetHashCodeExpression(memberAsObjExpr);

            return Expression.Condition(
                // If the argument is null, \
                Expression.ReferenceEqual(Expression.Constant(null), memberAsObjExpr),
                // we return 0 \
                Expression.Constant(0),
                // otherwise we invoke obj.GetHashCode or we GetSequenceHashCode if obj is a sequence.
                getHashCodeExpr);
        }

        private static Func<T, int> MakeGetHashCodeMethod<T>(IEnumerable<MemberInfo> members)
        {
            // We create the methhod parameter.
            var objParamAsObjExpr = Expression.Parameter(typeof(object), "obj");

            // We cast the parameter to the concrete type.
            var objParamExpr = Expression.Convert(objParamAsObjExpr, typeof(T));

            // We create the XOR expression.
            var getHashCodeExprs = members.Select(member => MakeGetHashCodeExpression(member, objParamExpr));
            var xorChainExpr =
                getHashCodeExprs.Aggregate((Expression) Expression.Constant(HashCodeSeed), LinkHashCodeExpression);

            // We compile the expression into a function.
            return Expression.Lambda<Func<T, int>>(xorChainExpr, objParamAsObjExpr).Compile();
        }

        private static Expression MakeReferenceTypeEqualExpression(Expression x, Expression y) =>
            Expression.Call(ObjectEqualsMethod, x, y);

        private static Expression MakeReferenceTypeGetHashCodeExpression(Expression obj) =>
            Expression.Call(obj, "GetHashCode", Type.EmptyTypes);

        private static Expression MakeSequenceTypeEqualExpression(Expression x, Expression y, Type enumerableType) =>
            Expression.Call(typeof(Enumerable), "SequenceEqual", new[] {enumerableType}, x, y);

        private static Expression MakeSequenceTypeGetHashCodeExpression(Expression obj) =>
            Expression.Call(typeof(EnumerableExtensions), "GetSequenceHashCode", Type.EmptyTypes, obj);
    }
}
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

            // If the member is a reference type, \
            if (!memberType.GetTypeInfo().IsValueType)
                // we create an expression that invoke Enumerable.SequenceEqual if obj is a sequence or obj.Equals otherwise.
                return IsSequenceType(memberType)
                    ? MakeSequenceTypeEqualExpression(xMemberExpr, yMemberExpr)
                    : MakeReferenceTypeEqualExpression(xMemberExpr, yMemberExpr);

            var xMemberAsObjExpr = Expression.Convert(xMemberExpr, typeof(object));
            var yMemberAsObjExpr = Expression.Convert(yMemberExpr, typeof(object));

            return MakeReferenceTypeEqualExpression(xMemberAsObjExpr, yMemberAsObjExpr);
        }

        private static Func<T, T, bool> MakeEqualsMethod<T>(IEnumerable<MemberInfo> members)
        {
            // We create the method parameters.
            var xParamExpr = Expression.Parameter(typeof(T), "x");
            var yParamExpr = Expression.Parameter(typeof(T), "y");

            // We create the AND chain expression using short-circuit evaluation.
            var equalsExprs = members.Select(member => MakeEqualsExpression(member, xParamExpr, yParamExpr));
            var andChainExpr = equalsExprs.Aggregate((Expression) Expression.Constant(true), Expression.AndAlso);

            // We compile the AND chain expression into a function.
            var andChainFunc = Expression.Lambda<Func<T, T, bool>>(andChainExpr, xParamExpr, yParamExpr).Compile();

            // We returns false if the types are different or we evaluate the AND chain otherwise.
            return (x, y) => x.GetType() == y.GetType() && andChainFunc(x, y);
        }

        private static Expression MakeGetHashCodeExpression(MemberInfo member, Expression obj)
        {
            var memberExpr = Expression.MakeMemberAccess(obj, member);
            var memberAsObjExpr = Expression.Convert(memberExpr, typeof(object));

            var memberType = memberExpr.Type;

            // We create an expression that invoke EnumerableExtensions.GetSequenceHashCode if obj is a sequence or obj.GetHashCode otherwise.
            var getHashCodeExpr = IsSequenceType(memberType)
                ? MakeSequenceTypeGetHashCodeExpression(memberExpr)
                : MakeReferenceTypeGetHashCodeExpression(memberAsObjExpr);

            return Expression.Condition(
                // If the argument is null, \
                Expression.ReferenceEqual(Expression.Constant(null), memberAsObjExpr),
                // we return 0 \
                Expression.Constant(0),
                // otherwise we invoke obj.GetHashCode or GetSequenceHashCode if obj is a sequence.
                getHashCodeExpr);
        }

        private static Func<T, int> MakeGetHashCodeMethod<T>(IEnumerable<MemberInfo> members)
        {
            // We create the methhod parameter.
            var objParamExpr = Expression.Parameter(typeof(T), "obj");

            // We create the XOR chain expression.
            var getHashCodeExprs = members.Select(member => MakeGetHashCodeExpression(member, objParamExpr));
            var xorChainExpr =
                getHashCodeExprs.Aggregate((Expression) Expression.Constant(HashCodeSeed), LinkHashCodeExpression);

            // We compile the XOR chain expression into a function.
            var xorChainFunc = Expression.Lambda<Func<T, int>>(xorChainExpr, objParamExpr).Compile();

            // We apply the XOR operator to the hash code of the given object type and the XOR chain.
            return obj => obj.GetType().GetHashCode() ^ xorChainFunc(obj);
        }

        private static Expression MakeReferenceTypeEqualExpression(Expression x, Expression y) =>
            Expression.Call(typeof(object), "Equals", Type.EmptyTypes, x, y);

        private static Expression MakeReferenceTypeGetHashCodeExpression(Expression obj) =>
            Expression.Call(obj, "GetHashCode", Type.EmptyTypes);

        private static Expression MakeSequenceTypeEqualExpression(Expression x, Expression y) => 
            Expression.Call(typeof(EnumerableExtensions), "SequenceEqual", Type.EmptyTypes, x, y);

        private static Expression MakeSequenceTypeGetHashCodeExpression(Expression obj) =>
            Expression.Call(typeof(EnumerableExtensions), "GetSequenceHashCode", Type.EmptyTypes, obj);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using SolidStack.Core.Equality.Internal;
using SolidStack.Core.Guards;

namespace SolidStack.Core.Equality
{
    public static class EqualityComparer
    {
        public static IEqualityComparer<T> ByElements<T>()
            where T : class, IEnumerable =>
            For<T>(
                (x, y) => x.SequenceEqual(y),
                obj => obj.GetSequenceHashCode());

        public static IEqualityComparer<T> ByFields<T>()
            where T : class =>
            ByFields<T>(fields => fields);

        public static IEqualityComparer<T> ByFields<T>(Func<FieldInfo, bool> predicate)
            where T : class
        {
            Guard.RequiresNonNull(predicate, nameof(predicate));

            return ByFields<T>(fields => fields.Where(predicate));
        }

        public static IEqualityComparer<T> ByFields<T>(
            Func<IEnumerable<FieldInfo>, IEnumerable<FieldInfo>> selector)
            where T : class
        {
            Guard.RequiresNonNull(selector, nameof(selector));

            return ByMembers<T>(members => selector(members.OfType<FieldInfo>()));
        }

        public static IEqualityComparer<T> ByKey<T>(Func<T, object> keyPath)
            where T : class
        {
            Guard.RequiresNonNull(keyPath, nameof(keyPath));

            return For<T>(
                (x, y) => keyPath(x).Equals(keyPath(y)),
                obj => keyPath(obj).GetHashCode());
        }

        public static IEqualityComparer<T> ByMembers<T>()
            where T : class =>
            ByMembers<T>(members => members);

        public static IEqualityComparer<T> ByMembers<T>(Func<MemberInfo, bool> predicate)
            where T : class
        {
            Guard.RequiresNonNull(predicate, nameof(predicate));

            return ByMembers<T>(members => members.Where(predicate));
        }

        public static IEqualityComparer<T> ByMembers<T>(
            Func<IEnumerable<MemberInfo>, IEnumerable<MemberInfo>> selector)
            where T : class
        {
            Guard.RequiresNonNull(selector, nameof(selector));

            return MemberwiseEqualityComparerFactory.Create<T>(selector);
        }

        public static IEqualityComparer<T> ByProperties<T>()
            where T : class =>
            ByProperties<T>(fields => fields);

        public static IEqualityComparer<T> ByProperties<T>(Func<PropertyInfo, bool> predicate)
            where T : class
        {
            Guard.RequiresNonNull(predicate, nameof(predicate));

            return ByProperties<T>(fields => fields.Where(predicate));
        }

        public static IEqualityComparer<T> ByProperties<T>(
            Func<IEnumerable<PropertyInfo>, IEnumerable<PropertyInfo>> selector)
            where T : class
        {
            Guard.RequiresNonNull(selector, nameof(selector));

            return ByMembers<T>(members => selector(members.OfType<PropertyInfo>()));
        }

        public static IEqualityComparer<T> ByTaggedFields<T>(params Type[] attributeTypes)
            where T : class
        {
            InnerGuard.RequiresValidAttributeTypes(attributeTypes);

            return ByFields<T>(member => member.HasAnyAttribute(attributeTypes));
        }

        public static IEqualityComparer<T> ByTaggedMembers<T>(params Type[] attributeTypes)
            where T : class
        {
            InnerGuard.RequiresValidAttributeTypes(attributeTypes);

            return ByMembers<T>(member => member.HasAnyAttribute(attributeTypes));
        }

        public static IEqualityComparer<T> ByTaggedProperties<T>(params Type[] attributeTypes)
            where T : class
        {
            InnerGuard.RequiresValidAttributeTypes(attributeTypes);

            return ByProperties<T>(member => member.HasAnyAttribute(attributeTypes));
        }

        public static IEqualityComparer<T> For<T>(Func<T, T, bool> equals, Func<T, int> getHashCode)
            where T : class
        {
            Guard.RequiresNonNull(equals, nameof(equals));
            Guard.RequiresNonNull(getHashCode, nameof(getHashCode));

            return new EqualityComparerFunc<T>(equals, getHashCode);
        }

        private static bool HasAnyAttribute(this MemberInfo member, IEnumerable<Type> attributeTypes) =>
            attributeTypes.Any(attributeType => Attribute.IsDefined(member, attributeType));

        private static class InnerGuard
        {
            [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
            public static void RequiresValidAttributeTypes(IEnumerable<Type> attributeTypes)
            {
                Guard.RequiresNoNullIn(attributeTypes, nameof(attributeTypes));
                Guard.RequiresAll(attributeTypes, type => type.IsSubclassOf(typeof(Attribute)),
                    $"Receiving {nameof(attributeTypes)} containing one or more types that are not a subclass of Attribute.");
            }
        }
    }
}
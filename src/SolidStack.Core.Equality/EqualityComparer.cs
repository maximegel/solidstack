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
        /// <summary>
        ///     Creates an equality comparer that compare the equality of two sequences based on the equality of each element.
        /// </summary>
        /// <typeparam name="T">The type of the sequences to compare.</typeparam>
        /// <returns>The elementwise equality comparer.</returns>
        public static IEqualityComparer<T> ByElements<T>()
            where T : class, IEnumerable =>
            For<T>(
                (x, y) => x.SequenceEqual(y),
                obj => obj.GetSequenceHashCode());

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each field.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <returns>The fieldwise equality comparer.</returns>
        public static IEqualityComparer<T> ByFields<T>()
            where T : class =>
            ByFields<T>(fields => fields);

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each selected field.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="predicate">The filter function used to select the fields to compare.</param>
        /// <returns>The fieldwise equality comparer.</returns>
        public static IEqualityComparer<T> ByFields<T>(Func<FieldInfo, bool> predicate)
            where T : class
        {
            Guard.RequiresNonNull(predicate, nameof(predicate));

            return ByFields<T>(fields => fields.Where(predicate));
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each selected field.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="selector">The filter function used to select the fields to compare.</param>
        /// <returns>The fieldwise equality comparer.</returns>
        public static IEqualityComparer<T> ByFields<T>(
            Func<IEnumerable<FieldInfo>, IEnumerable<FieldInfo>> selector)
            where T : class
        {
            Guard.RequiresNonNull(selector, nameof(selector));

            return ByMembers<T>(members => selector(members.OfType<FieldInfo>()));
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of a field or a
        ///     property that act as a key.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="keyPath">The function used to select the key.</param>
        /// <returns>The keywise equality comparer.</returns>
        public static IEqualityComparer<T> ByKey<T>(Func<T, object> keyPath)
            where T : class
        {
            Guard.RequiresNonNull(keyPath, nameof(keyPath));

            return For<T>(
                (x, y) => keyPath(x).Equals(keyPath(y)),
                obj => obj.GetType().GetHashCode() ^ keyPath(obj).GetHashCode());
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each field or
        ///     property.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <returns>The memberwise equality comparer.</returns>
        public static IEqualityComparer<T> ByMembers<T>()
            where T : class =>
            ByMembers<T>(members => members);

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each selected field
        ///     or property.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="predicate">The filter function used to select the members to compare.</param>
        /// <returns></returns>
        public static IEqualityComparer<T> ByMembers<T>(Func<MemberInfo, bool> predicate)
            where T : class
        {
            Guard.RequiresNonNull(predicate, nameof(predicate));

            return ByMembers<T>(members => members.Where(predicate));
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each selected field
        ///     or property.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="selector">The filter function used to select the members to compare.</param>
        /// <returns>The memberwise equality comparer.</returns>
        public static IEqualityComparer<T> ByMembers<T>(
            Func<IEnumerable<MemberInfo>, IEnumerable<MemberInfo>> selector)
            where T : class
        {
            Guard.RequiresNonNull(selector, nameof(selector));

            return MemberwiseEqualityComparerFactory.Create<T>(selector);
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each property.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <returns>The propertywise equality comparer.</returns>
        public static IEqualityComparer<T> ByProperties<T>()
            where T : class =>
            ByProperties<T>(properties => properties);

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each selected
        ///     property.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="predicate">The filter function used to select the properties to compare.</param>
        /// <returns>The propertywise equality comparer.</returns>
        public static IEqualityComparer<T> ByProperties<T>(Func<PropertyInfo, bool> predicate)
            where T : class
        {
            Guard.RequiresNonNull(predicate, nameof(predicate));

            return ByProperties<T>(properties => properties.Where(predicate));
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each selected
        ///     property.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="selector">The filter function used to select the properties to compare</param>
        /// <returns>The propertywise equality comparer.</returns>
        public static IEqualityComparer<T> ByProperties<T>(
            Func<IEnumerable<PropertyInfo>, IEnumerable<PropertyInfo>> selector)
            where T : class
        {
            Guard.RequiresNonNull(selector, nameof(selector));

            return ByMembers<T>(members => selector(members.OfType<PropertyInfo>()));
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each field tagged by
        ///     <see cref="EqualityMemberAttribute" />.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <returns>The fieldwise equality comparer.</returns>
        public static IEqualityComparer<T> ByTaggedFields<T>()
            where T : class =>
            ByTaggedFields<T>(typeof(EqualityMemberAttribute));

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each field tagged by
        ///     the specified attributes.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="attributeTypes"></param>
        /// <returns>The fieldwise equality comparer.</returns>
        public static IEqualityComparer<T> ByTaggedFields<T>(params Type[] attributeTypes)
            where T : class
        {
            InnerGuard.RequiresValidAttributeTypes(attributeTypes);

            return ByFields<T>(field => field.HasAnyAttribute(attributeTypes));
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each field or
        ///     property tagged by <see cref="EqualityMemberAttribute" />.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <returns>The memberwise equality comparer.</returns>
        public static IEqualityComparer<T> ByTaggedMembers<T>()
            where T : class =>
            ByTaggedMembers<T>(typeof(EqualityMemberAttribute));

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each field or
        ///     property tagged by the specified attributes.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="attributeTypes"></param>
        /// <returns>The memberwise equality comparer.</returns>
        public static IEqualityComparer<T> ByTaggedMembers<T>(params Type[] attributeTypes)
            where T : class
        {
            InnerGuard.RequiresValidAttributeTypes(attributeTypes);

            return ByMembers<T>(member => member.HasAnyAttribute(attributeTypes));
        }

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each property tagged
        ///     by <see cref="EqualityMemberAttribute" />.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <returns>The propertywise equality comparer.</returns>
        public static IEqualityComparer<T> ByTaggedProperties<T>()
            where T : class =>
            ByTaggedProperties<T>(typeof(EqualityMemberAttribute));

        /// <summary>
        ///     Creates an equality comparer that compare the equality of two objects based on the equality of each property tagged
        ///     by the specified attributes.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="attributeTypes"></param>
        /// <returns>The propertywise equality comparer.</returns>
        public static IEqualityComparer<T> ByTaggedProperties<T>(params Type[] attributeTypes)
            where T : class
        {
            InnerGuard.RequiresValidAttributeTypes(attributeTypes);

            return ByProperties<T>(property => property.HasAnyAttribute(attributeTypes));
        }

        /// <summary>
        ///     Creates a custom equality comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="equals">The function used to validate the equality of the object.</param>
        /// <param name="getHashCode">The function used to generate the hash code of an object.</param>
        /// <returns>The custom equality comparer.</returns>
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
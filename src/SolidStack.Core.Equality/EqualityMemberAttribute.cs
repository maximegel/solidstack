using System;
using System.Collections.Generic;

namespace SolidStack.Core.Equality
{
    /// <inheritdoc />
    /// <summary>
    ///     Tags a field or a property that should be used to evaluate the equality of the object.
    /// </summary>
    /// <remarks>
    ///     To construct a <see cref="IEqualityComparer{T}" /> that compare equality of objects based on all members of these
    ///     objects tagged by this attribute use the <see cref="EqualityComparer.ByTaggedMembers{T}" /> method.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EqualityMemberAttribute : Attribute
    {
    }
}
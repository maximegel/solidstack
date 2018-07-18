using System;
using System.Collections.Generic;

namespace SolidStack.Core.Equality.Tests.Doubles
{
    public class DummyByMembersEqualityComparableChild : DummyByMembersEqualityComparable
    {
        protected string FieldC;

        public DummyByMembersEqualityComparableChild(
            int fieldA, IEnumerable<char> fieldB, string fieldC,
            DateTime propertyA, IEnumerable<bool> propertyB, bool propertyC) :
            base(fieldA, fieldB, propertyA, propertyB)
        {
            FieldC = fieldC;
            PropertyC = propertyC;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor used to instantiate the class via reflection.
        /// </summary>
        public DummyByMembersEqualityComparableChild()
        {
        }

        public bool PropertyC { get; set; }
    }
}
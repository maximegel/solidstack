using System;
using System.Collections.Generic;

namespace SolidStack.Core.Equality.Tests.Doubles
{
    public class DummyByMembersEqualityComparable
    {
        public int FieldA;

        protected IEnumerable<char> FieldB;

        public DummyByMembersEqualityComparable(
            int fieldA, IEnumerable<char> fieldB,
            DateTime propertyA, IEnumerable<bool> propertyB)
        {
            FieldA = fieldA;
            FieldB = fieldB;
            PropertyA = propertyA;
            PropertyB = propertyB;
        }

        /// <summary>
        ///     Constructor used to instantiate the class via reflection.
        /// </summary>
        public DummyByMembersEqualityComparable()
        {
        }

        public DateTime PropertyA { get; }

        protected IEnumerable<bool> PropertyB { get; set; }
    }
}
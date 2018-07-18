namespace SolidStack.Core.Equality.Tests.Doubles
{
    public class DummyByTaggedMembersEqualityComparable
    {
        [EqualityMember]
        public int FieldA;

        protected string FieldB;

        public DummyByTaggedMembersEqualityComparable(int fieldA, string fieldB, bool propertyA, char propertyB)
        {
            FieldA = fieldA;
            FieldB = fieldB;
            PropertyA = propertyA;
            PropertyB = propertyB;
        }

        /// <summary>
        ///     Constructor used to instantiate the class via reflection.
        /// </summary>
        public DummyByTaggedMembersEqualityComparable()
        {
        }

        public bool PropertyA { get; }

        [EqualityMember]
        protected char PropertyB { get; set; }
    }
}
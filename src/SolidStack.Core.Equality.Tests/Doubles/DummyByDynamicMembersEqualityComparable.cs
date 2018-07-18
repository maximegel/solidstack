namespace SolidStack.Core.Equality.Tests.Doubles
{
    public class DummyByDynamicMembersEqualityComparable
    {
        protected dynamic FieldA;

        public DummyByDynamicMembersEqualityComparable(dynamic fieldA, dynamic propertyA)
        {
            FieldA = fieldA;
            PropertyA = propertyA;
        }

        /// <summary>
        ///     Constructor used to instantiate the class via reflection.
        /// </summary>
        public DummyByDynamicMembersEqualityComparable()
        {
        }

        public dynamic PropertyA { get; }
    }
}
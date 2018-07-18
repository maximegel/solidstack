namespace SolidStack.Core.Equality.Tests.Doubles
{
    public class DummyByKeyEqualityComparableChild : DummyByKeyEqualityComparable
    {
        public DummyByKeyEqualityComparableChild(string id) :
            base(id)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor used to instantiate the class via reflection.
        /// </summary>
        public DummyByKeyEqualityComparableChild()
        {
        }
    }
}
namespace SolidStack.Core.Equality.Tests.Doubles
{
    public class DummyByKeyEqualityComparable
    {
        public DummyByKeyEqualityComparable(string id) => 
            Id = id;

        /// <summary>
        ///     Constructor used to instantiate the class via reflection.
        /// </summary>
        public DummyByKeyEqualityComparable()
        {
        }

        public string Id { get; }
    }
}
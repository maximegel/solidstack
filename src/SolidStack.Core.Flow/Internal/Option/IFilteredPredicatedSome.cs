namespace SolidStack.Core.Flow.Internal.Option
{
    public interface IFilteredPredicatedSome<out T> :
        IFilteredActionableContent<T, IActionableOption<T>>,
        IFilteredMappablePredicatedSome<T>
    {
    }
}
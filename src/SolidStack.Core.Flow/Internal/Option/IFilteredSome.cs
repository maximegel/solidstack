namespace SolidStack.Core.Flow.Internal.Option
{
    public interface IFilteredSome<out T> :
        IFilteredActionableContent<T, IActionableOption<T>>,
        IFilteredMappableSome<T>
    {
    }
}
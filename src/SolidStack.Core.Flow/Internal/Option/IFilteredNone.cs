namespace SolidStack.Core.Flow.Internal.Option
{
    public interface IFilteredNone<out T> :
        IFilteredActionableVoid<IActionableOption<T>>,
        IFilteredMappableNone<T>
    {
    }
}
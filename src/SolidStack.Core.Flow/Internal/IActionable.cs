namespace SolidStack.Core.Flow.Internal
{
    public interface IActionable
    {
        /// <summary>
        ///     Executes all actions added to the filtered flow corresponding to the initial object state.
        /// </summary>
        void Execute();
    }
}
using JetBrains.Annotations;

namespace SolidStack.Persistence
{
    public class Repository<TId, TPersistable, TStorage> : IRepository<TId, TPersistable>
    {
        // TODO(maximeggelinas): Get the storage.
        [NotNull]
        protected TStorage Storage => default;
    }
}
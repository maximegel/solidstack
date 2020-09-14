using System.Linq;

namespace SolidStack.Persistence
{
    public interface IQueryableRepository<in TId, TPersistable> :
        IReadableRepository<TId, TPersistable>,
        IQueryable<TPersistable>
    {
    }
}
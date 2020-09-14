using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SolidStack.Core.Guards;

namespace SolidStack.Persistence
{
    public class ReadableRepository<TId, TPersistable, TStorage> : Repository<TId, TPersistable, TStorage>,
        IReadableRepository<TId, TPersistable>
        where TStorage : IReadableStorage<TId, TPersistable>
    {
        public async Task<IEnumerable<TPersistable>> FindRangeAsync(IEnumerable<TId> ids, CancellationToken cancellationToken)
        {
            var idList = ids as IList<TId> ?? ids.ToList();

            Guard.RequiresNonNull(idList, nameof(ids));
            Guard.RequiresNoNullIn(idList, nameof(ids));

            var result = (await Storage.FindRangeAsync(idList, cancellationToken)).ToList();

            Guard.EnsuresNonNull(result,
                $"{nameof(IReadableStorage<TId, TPersistable>)} declared by {Storage.GetType()} returned a null sequence.");
            Guard.EnsuresNoNullIn(result,
                $"{nameof(IReadableStorage<TId, TPersistable>)} declared by {Storage.GetType()} returned a sequence containing one or more null elements.");

            return result;
        }
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SolidStack.Persistence
{
    public interface IWritableStorage<in TId, TPersistable>
    {
        Task AddOrUpdateRangeAsync(
            [NotNull] [ItemNotNull] IEnumerable<TPersistable> persistables,
            CancellationToken cancellationToken);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<TPersistable>> FindRangeAsync(
            [NotNull] [ItemNotNull] IEnumerable<TId> ids,
            CancellationToken cancellationToken);

        Task RemoveRangeAsync(
            [NotNull] [ItemNotNull] IEnumerable<TPersistable> persistables,
            CancellationToken cancellationToken);
    }
}
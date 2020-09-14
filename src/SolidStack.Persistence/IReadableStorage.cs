using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SolidStack.Persistence
{
    public interface IReadableStorage<in TId, TPersistable>
    {
        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<TPersistable>> FindRangeAsync(
            [NotNull] [ItemNotNull] IEnumerable<TId> ids,
            CancellationToken cancellationToken);
    }
}
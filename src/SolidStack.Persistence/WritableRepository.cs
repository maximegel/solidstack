using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SolidStack.Core.Collections;
using SolidStack.Core.Guards;

namespace SolidStack.Persistence
{
    public class WritableRepository<TId, TPersistable, TStorage> : Repository<TId, TPersistable, TStorage>,
        IWritableRepository<TId, TPersistable>
        where TId : struct, IEquatable<TId>
        where TPersistable : IPersistable<TId>, IChangeTracking
        where TStorage : IWritableStorage<TId, TPersistable>, IObservableStorage
    {
        public WritableRepository()
        {
            Storage.SavingChanges += Storage_SavingChanges;
            Storage.ChangesSaved += Storage_ChangesSaved;
            Storage.ChangesRollbacked += Storage_ChangesRollbacked;
        }

        private IChangeTracker<TPersistable> ChangeTracker { get; } =
            new DelegatingChangeTracker<TPersistable>();

        private IDictionary<TId, TPersistable> MemoryCache { get; } =
            new ConcurrentDictionary<TId, TPersistable>();

        public void AddOrUpdateRange(IEnumerable<TPersistable> persistables)
        {
            var persistableList = persistables as IList<TPersistable> ?? persistables.ToList();

            Guard.RequiresNonNull(persistableList, nameof(persistables));
            Guard.RequiresNoNullIn(persistableList, nameof(persistables));

            MemoryCache.AddOrUpdateRange(persistableList.ToDictionary(persistable => persistable.Id,
                persistable => persistable));

            Storage.AddOrUpdateRange(persistableList);
        }

        public IEnumerable<TPersistable> FindRange(IEnumerable<TId> ids)
        {
            var idList = ids as IList<TId> ?? ids.ToList();

            Guard.RequiresNonNull(idList, nameof(ids));
            Guard.RequiresNoNullIn(idList, nameof(ids));

            var cacheLookup = idList.ToLookup(MemoryCache.ContainsKey);

            return FindRangeFromMemoryCache(cacheLookup[true])
                .Concat(FindRangeFromStorage(cacheLookup[false]))
                .OrderBy(persistable => idList.IndexOf(persistable.Id))
                .ToList();
        }

        public void RemoveRange(IEnumerable<TPersistable> persistables)
        {
            var persistableList = persistables as IList<TPersistable> ?? persistables.ToList();

            Guard.RequiresNonNull(persistableList, nameof(persistables));
            Guard.RequiresNoNullIn(persistableList, nameof(persistables));

            MemoryCache.RemoveRange(persistableList.Select(persistable => persistable.Id));

            Storage.RemoveRange(persistableList);
        }

        private IEnumerable<TPersistable> FindRangeFromMemoryCache(IEnumerable<TId> ids) =>
            ids.Select(id => MemoryCache[id]);

        private IEnumerable<TPersistable> FindRangeFromStorage(IEnumerable<TId> ids)
        {
            var result = Storage.FindRange(ids).ToList();

            Guard.EnsuresNonNull(result,
                $"{nameof(IReadableStorage<TId, TPersistable>)} declared by {Storage.GetType()} returned a null sequence.");
            Guard.EnsuresNoNullIn(result,
                $"{nameof(IReadableStorage<TId, TPersistable>)} declared by {Storage.GetType()} returned a sequence containing one or more null elements.");

            MemoryCache.AddOrUpdateRange(result.ToDictionary(persistable => persistable.Id,
                persistable => persistable));

            return result;
        }

        private void Storage_ChangesRollbacked()
        {
            MemoryCache.Clear();
        }

        private void Storage_ChangesSaved()
        {
            ChangeTracker.AcceptAllChanges();
            MemoryCache.Clear();
        }

        private void Storage_SavingChanges()
        {
            Storage.AddOrUpdateRange(ChangeTracker.DetectChanges());
        }
    }
}
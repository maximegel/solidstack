//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using SolidStack.Core.Flow;
//using SolidStack.Core.Guards;

//namespace SolidStack.Persistence
//{
//    //TODO(maximegelinas): Add ReSharper annotations.
//    public static class ReadableRepositoryExtensions
//    {
//        [NotNull]
//        [ItemNotNull]
//        public static IEnumerable<TPersistable> FindRange<TId, TPersistable>(
//            [NotNull] this IReadableRepository<TId, TPersistable> repository,
//            [NotNull] [ItemNotNull] params TId[] ids)
//        {
//            Guard.RequiresNonNull(repository, nameof(repository));
//            Guard.RequiresNonNull(ids, nameof(ids));

//            return repository.FindRange(ids.AsEnumerable());
//        }

//        public static Task<IEnumerable<TPersistable>> FindRangeAsync<TId, TPersistable>(
//            this IReadableRepository<TId, TPersistable> repository,
//            params TId[] ids) =>
//            FindRangeAsync(repository, ids.AsEnumerable());

//        public static Task<IEnumerable<TPersistable>> FindRangeAsync<TId, TPersistable>(
//            this IReadableRepository<TId, TPersistable> repository,
//            IEnumerable<TId> ids,
//            CancellationToken cancellationToken = default)
//        {
//            Guard.RequiresNonNull(repository, nameof(repository));

//            return Task.Run(() => repository.FindRange(ids), cancellationToken);
//        }

//        public static IOption<TPersistable> TryFind<TId, TPersistable>(
//            this IReadableRepository<TId, TPersistable> repository,
//            TId id)
//        {
//            Guard.RequiresNonNull(repository, nameof(repository));

//            var entities = repository.FindRange(id).ToList();

//            Guard.EnsuresNonNull(entities,
//                $"{nameof(IReadableRepository<TId, TPersistable>.FindRange)} declared by {repository.GetType()} returned a null sequence.");

//            return entities
//                .Select(Option.Some)
//                .DefaultIfEmpty(Option.None<TPersistable>())
//                .Single();
//        }

//        public static Task<IOption<TPersistable>> TryFindAsync<TId, TPersistable>(
//            this IReadableRepository<TId, TPersistable> repository,
//            TId id) =>
//            Task.Run(() => TryFind(repository, id));
//    }
//}
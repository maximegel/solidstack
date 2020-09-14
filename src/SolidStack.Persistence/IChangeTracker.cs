using System.Collections.Generic;
using JetBrains.Annotations;

namespace SolidStack.Persistence
{
    public interface IChangeTracker<T>
    {
        void AcceptAllChanges();

        void Clear();

        [NotNull]
        [ItemNotNull]
        IEnumerable<T> DetectChanges();

        void TrackRange([NotNull] [ItemNotNull] IEnumerable<T> instances);
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SolidStack.Core.Guards;

namespace SolidStack.Persistence
{
    public class DelegatingChangeTracker<T> :
        IChangeTracker<T>
        where T : IChangeTracking
    {
        private List<T> TrackedInstances { get; } =
            new List<T>();

        public void AcceptAllChanges() =>
            TrackedInstances.ForEach(instance => instance.AcceptChanges());

        public void Clear() =>
            TrackedInstances.Clear();

        public IEnumerable<T> DetectChanges() =>
            TrackedInstances.Where(instance => instance.IsChanged);

        public void TrackRange(IEnumerable<T> instances)
        {
            var instanceList = instances as IList<T> ?? instances.ToList();

            Guard.RequiresNonNull(instanceList, nameof(instances));
            Guard.RequiresNoNullIn(instanceList, nameof(instances));

            TrackedInstances.AddRange(instanceList);
        }
    }
}
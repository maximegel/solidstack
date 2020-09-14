using JetBrains.Annotations;
using SolidStack.Core.Guards;

namespace SolidStack.Persistence
{
    public static class ChangeTrackerExtensions
    {
        public static void Track<T>(
            [NotNull] IChangeTracker<T> changeTracker,
            [NotNull] T instance)
        {
            Guard.RequiresNonNull(changeTracker, nameof(changeTracker));
            Guard.RequiresNonNull(instance, nameof(instance));

            changeTracker.TrackRange(new[] {instance});
        }

        public static void TrackRange<T>(
            [NotNull] IChangeTracker<T> changeTracker,
            [NotNull] [ItemNotNull] params T[] instances)
        {
            Guard.RequiresNonNull(changeTracker, nameof(changeTracker));
            Guard.RequiresNonNull(instances, nameof(instances));
            Guard.RequiresNoNullIn(instances, nameof(instances));

            changeTracker.TrackRange(instances);
        }
    }
}
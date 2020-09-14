using System;

namespace SolidStack.Persistence
{
    public interface IPersistable<out TId>
        where TId : struct, IEquatable<TId>
    {
        TId Id { get; }
    }
}
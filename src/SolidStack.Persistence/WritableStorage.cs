using System;
using System.Collections.Generic;

namespace SolidStack.Persistence
{
    public abstract class WritableStorage<TId, TPersistable> :
        IWritableStorage<TId, TPersistable>
    {
        public void AddOrUpdateRange(IEnumerable<TPersistable> persistables)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TPersistable> FindRange(IEnumerable<TId> ids)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<TPersistable> persistables)
        {
            throw new NotImplementedException();
        }
    }
}
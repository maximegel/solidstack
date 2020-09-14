using System;

namespace SolidStack.Persistence
{
    public interface IObservableStorage
    {
        event Action ChangesRollbacked;

        event Action ChangesSaved;

        event Action SavingChanges;
    }
}
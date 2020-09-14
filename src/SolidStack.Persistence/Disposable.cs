using System;

namespace SolidStack.Persistence
{
    public abstract class Disposable : IDisposable
    {
        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            DisposeUnmanagedResources();
            if (disposing)
                DisposeManagedResources();

            IsDisposed = true;
        }

        protected abstract void DisposeManagedResources();

        protected virtual void DisposeUnmanagedResources()
        {
        }

        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        ~Disposable() =>
            Dispose(false);
    }
}
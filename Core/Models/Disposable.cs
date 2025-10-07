using System;

namespace Ion.Core;

/// <summary>
/// A <see cref="Model"/> that implements <see cref="IDisposable"/>.
/// </summary>
public abstract record class Disposable() : Model(), IDisposable
{
    bool disposed;

    ~Disposable()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
            OnManagedDisposed();

        OnUnmanagedDisposed();
        disposed = true;
    }

    /// <summary>
    /// Occurs when managed resources need disposed.
    /// </summary>
    protected virtual void OnManagedDisposed() { }

    /// <summary>
    /// Occurs when unmanaged resources need disposed.
    /// </summary>
    protected virtual void OnUnmanagedDisposed() { }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
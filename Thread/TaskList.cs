using Ion.Collect;
using Ion.Input;
using System;

namespace Ion.Threading;

public class TaskList<T>() : ListObservable<T>() where T : class, ITask
{
    public event EventHandle<T> Completed;

    public event EventHandler<EventArgs> Scanned;

    public event EventHandle<T> Started;

    public T Current { get; private set; }

    private async void Assign(T task)
    {
        Current = task;
        OnStarted(task);
        
        await Current.Start();

        _ = Remove(Current);
        Current = null;

        OnCompleted(task);
        Scan();
    }

    private void Scan()
    {
        Current.IfNull(() => Count > 0, () => Assign(this[0]));
        OnScanned(EventArgs.Empty);
    }

    protected override void OnAdded(ListAddedEventArgs e) { base.OnAdded(e); Scan(); }

    protected override void OnClearing(ListClearingEventArgs e)
    {
        base.OnClearing(e);
        this.ForEach(i => i.Cancel());
    }

    protected virtual void OnCompleted(T task) => Completed?.Invoke(this, new(task));

    protected virtual void OnScanned(EventArgs e) => Scanned?.Invoke(this, e);

    protected virtual void OnStarted(T task) => Started?.Invoke(this, new(task));
}
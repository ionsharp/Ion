using Ion.Input;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ion.Threading;

public class TaskQueue()
{
    private readonly object _Lock = new();

    private Task _Previous = Task.FromResult(true);

    private CancellationTokenSource _Token = new();

    ///

    /// <summary>
    /// Occurs when all tasks have cancelled.
    /// </summary>
    public event EventHandler<EventArgs> Cancelled;

    /// <summary>
    /// Occurs when all tasks have completed.
    /// </summary>
    public event EventHandler<EventArgs> Completed;

    /// <summary>
    /// Occurs when a task has completed.
    /// </summary>
    public event EventHandle<object> TaskCompleted;

    ///

    public int Count { get; private set; }

    public bool IsCancellationRequested { get; private set; }

    ///

    /// <inheritdoc cref="Cancelled"/>
    protected virtual void OnCancelled()
    {
        Count = 0;
        _Previous = Task.FromResult(true);
        _Token = new CancellationTokenSource();

        Cancelled?.Invoke(this, new EventArgs());
    }

    /// <inheritdoc cref="Completed"/>
    protected virtual void OnCompleted()
    {
        Completed?.Invoke(this, new EventArgs());
    }

    /// <inheritdoc cref="TaskCompleted"/>
    protected virtual void OnTaskCompleted<T>(T input)
    {
        Count--;
        TaskCompleted?.Invoke(this, new EventArgs<object>(input));
        if (Count == 0)
            OnCompleted();
    }

    /// <exception cref="ArgumentNullException"/>
    public Task Add(Action action)
    {
        Throw.IfNull(action, nameof(action));
        lock (_Lock)
        {
            Count++;
            _Previous = _Previous.ContinueWith(i =>
            {
                action();
                OnTaskCompleted(default(object));
            },
            _Token.Token, TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
            return _Previous;
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public Task Add(Action<CancellationToken> action)
    {
        Throw.IfNull(action, nameof(action));
        lock (_Lock)
        {
            Count++;
            _Previous = _Previous.ContinueWith(i =>
            {
                action(_Token.Token);
                OnTaskCompleted(default(object));
            },
            _Token.Token, TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
            return _Previous;
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public Task Add<T>(Func<T> action)
    {
        Throw.IfNull(action, nameof(action));
        lock (_Lock)
        {
            _Previous = _Previous.ContinueWith(i =>
            {
                var result = action();
                OnTaskCompleted(result);
            },
            _Token.Token, TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
            return _Previous;
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public Task Add<T>(Func<CancellationToken, T> action)
    {
        Throw.IfNull(action, nameof(action));
        lock (_Lock)
        {
            Count++;
            _Previous = _Previous.ContinueWith(i =>
            {
                var result = action(_Token.Token);
                OnTaskCompleted(result);
            },
            _Token.Token, TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
            return _Previous;
        }
    }

    public void CancelAll()
    {
        if (!IsCancellationRequested)
        {
            IsCancellationRequested = true;
            _Token?.Cancel();

            try
            {
                _Previous?.Wait();
            }
            catch { }
            finally
            {
                OnCancelled();
            }
            IsCancellationRequested = false;
        }
    }
}
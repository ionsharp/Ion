using Ion.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Ion.Threading;

/// <summary>
/// An <see cref="ITask"/> that can be cancelled and paused.
/// </summary>
public record class Taskable : Model, ITask
{
    private sealed class Request(TaskType type)
    {
        public readonly TaskType Type = type;
    }

    /// <see cref="Region.Event"/>

    public event TaskActiveEventHandler Active;

    public event TaskCancelledEventHandler Cancelled;

    public event TaskCompletedEventHandler Completed;

    public event TaskPausedEventHandler Paused;

    public event TaskProgressedEventHandler Progressed;

    /// <see cref="Region.Field"/>
    #region

    private readonly TaskManaged ManagedTask;

    private readonly TaskUnmanaged UnmanagedTask;

    private readonly List<Request> requests = [];

    protected CancellationTokenSource token;

    #endregion

    /// <see cref="Region.Property"/>
    #region

    public TimeSpan Duration { get => Get(TimeSpan.Zero); set => Set(value); }

    public bool IsCancelled => token?.IsCancellationRequested ?? false;

    public bool IsPaused { get => Get(false, false); protected set => Set(value, false); }

    public bool IsStarted { get => Get(false, false); protected set => Set(value, false); }

    public DateTime? LastActive { get => Get<DateTime?>(); set => Set(value); }

    public DateTime? LastCancelled { get => Get<DateTime?>(); set => Set(value); }

    public DateTime? LastCompleted { get => Get<DateTime?>(); set => Set(value); }

    public DateTime? LastPaused { get => Get<DateTime?>(); set => Set(value); }

    public double Progress { get => Get(.0); set => Set(value); }

    public TaskStrategy Strategy { get => Get(TaskStrategy.Ignore); set => Set(value); }

    #endregion

    /// <see cref="Region.Constructor"/>
    #region

    private Taskable() : base() { }

    protected Taskable(TaskStrategy strategy) : this()
        => Strategy = strategy;

    public Taskable(TaskUnmanaged unmanaged, TaskStrategy strategy = TaskStrategy.Ignore) : this(strategy)
        => UnmanagedTask = unmanaged;

    public Taskable(TaskManaged managed, TaskStrategy strategy = TaskStrategy.Ignore) : this(strategy)
        => ManagedTask = managed;

    #endregion

    /// <see cref="Region.Method.Protected"/>

    public override void OnSetProperty(PropertySetEventArgs e)
    {
        base.OnSetProperty(e);
        if (e.PropertyName == nameof(Progress))
            OnProgressed(new(DateTime.Now, ((double)e.OldValue, (double)e.NewValue)));
    }

    protected virtual void OnActive(TaskEventArgs e) => Active?.Invoke(this, e);

    protected virtual void OnCancelled(TaskEventArgs e) => Cancelled?.Invoke(this, e);

    protected virtual void OnCompleted(TaskEventArgs e) => Completed?.Invoke(this, e);

    protected virtual void OnPaused(TaskEventArgs e) => Paused?.Invoke(this, e);

    protected virtual void OnProgressed(TaskProgressedEventArgs e) => Progressed?.Invoke(this, e);

    /// <see cref="Region.Method.Public"/>
    #region

    [NotTested]
    public void Cancel()
    {
        token?.Cancel();
        Reset(() => IsCancelled);

        OnActive(new TaskEventArgs(DateTime.Now));
        OnCancelled(new TaskEventArgs(DateTime.Now));
    }

    [NotComplete]
    public void Pause()
    {
        OnActive(new TaskEventArgs(DateTime.Now));
        OnPaused(new TaskEventArgs(DateTime.Now));
    }

    private readonly object _lock = new();

    public virtual async Task Start()
    {
        var type = ManagedTask is not null ? TaskType.Managed : TaskType.Unmanaged;

        Monitor.Enter(_lock);
        try
        {
            if (IsStarted)
            {
                var request = Strategy switch
                {
                    TaskStrategy.CancelAndRestart | TaskStrategy.FinishAndRestart => new Request(type),
                    TaskStrategy.Ignore => null,
                    _ => null
                };

                if (request is not null)
                    requests.Add(request);

                switch (Strategy)
                {
                    case TaskStrategy.CancelAndRestart:
                        Cancel();
                        return;

                    case TaskStrategy.FinishAndRestart:
                    case TaskStrategy.Ignore:
                        return;
                }
            }
        }
        finally { Monitor.Exit(_lock); }

        Duration
            = TimeSpan.Zero;
        LastActive
            = DateTime.Now;
        Progress
            = 0;

        token = new CancellationTokenSource();
        Reset(() => IsCancelled);

        var watch = new Stopwatch();
        watch.Start();

        switch (type)
        {
            case TaskType.Managed:
                await Task.Run(() => ManagedTask.Invoke(token.Token), token.Token);
                break;

            case TaskType.Unmanaged:
                await UnmanagedTask.Invoke(token.Token);
                break;
        }

        watch.Stop();

        token = null;
        Reset(() => IsCancelled);

        Duration
            = watch.Elapsed;
        LastActive
            = DateTime.Now;
        LastCompleted
            = DateTime.Now;
        Progress
            = 0;

        Monitor.Enter(_lock);

        try
        { IsStarted = false; }
        finally
        { Monitor.Exit(_lock); }

        OnCompleted(new(DateTime.Now));
        if (requests.Count != 0)
        {
            var request = requests.First();
            requests.RemoveAt(0);

            _ = Start();
        }
    }

    #endregion
}

/// <inheritdoc/>
public record class Taskable<T>(TaskManaged<T> managed, TaskUnmanaged<T> unmanaged, TaskStrategy strategy = TaskStrategy.Ignore) : Taskable(strategy)
{
    /// <see cref="Request"/>
    #region

    private sealed class Request(T i, TaskType e)
    {
        public readonly TaskType Type = e;

        public readonly T Parameter = i;
    }

    #endregion

    /// <see cref="Region.Field"/>
    #region

    [field: NonSerialized]
    private readonly TaskManaged<T> ManagedTask = managed;

    [field: NonSerialized]
    private readonly TaskUnmanaged<T> UnmanagedTask = unmanaged;

    [field: NonSerialized]
    private readonly List<Request> requests = [];

    #endregion

    /// <see cref="Region.Method"/>
    #region

    public override async Task Start() => await Start(default);

    public async Task Start(T parameter, TaskType type = TaskType.Unmanaged)
    {
        if (IsStarted)
        {
            var request = Strategy switch
            {
                TaskStrategy.CancelAndRestart | TaskStrategy.FinishAndRestart => new Request(parameter, type),
                TaskStrategy.Ignore => null,
                _ => null
            };

            if (request is not null)
                requests.Add(request);

            switch (Strategy)
            {
                case TaskStrategy.CancelAndRestart:
                    Cancel();
                    return;

                case TaskStrategy.FinishAndRestart:
                case TaskStrategy.Ignore:
                    return;
            }
        }

        IsStarted = true;

        Duration = TimeSpan.Zero;
        Progress = 0;

        token = new CancellationTokenSource();
        Reset(() => IsCancelled);

        var watch = new Stopwatch();
        watch.Start();

        switch (type)
        {
            case TaskType.Managed:
                await Task.Run(() => ManagedTask.Invoke(parameter, token.Token), token.Token);
                break;

            case TaskType.Unmanaged:
                await UnmanagedTask.Invoke(parameter, token.Token);
                break;
        }

        watch.Stop();

        token = null;
        Reset(() => IsCancelled);

        Duration = watch.Elapsed;
        Progress = 0;

        IsStarted = false;
        OnCompleted(new(DateTime.Now));

        if (requests.Count != 0)
        {
            var request = requests.First<Request>();
            requests.RemoveAt(0);

            _ = Start(request.Parameter, request.Type);
        }
    }

    #endregion
}
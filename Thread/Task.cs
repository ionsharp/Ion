using System;
using System.Threading.Tasks;

namespace Ion.Threading;

public interface ITask
{
    public event TaskActiveEventHandler Active;

    public event TaskCancelledEventHandler Cancelled;

    public event TaskCompletedEventHandler Completed;

    public event TaskPausedEventHandler Paused;

    public event TaskProgressedEventHandler Progressed;

    public void Cancel();

    public void Pause();

    public Task Start();

    public TimeSpan Duration { get; }

    public bool IsCancelled { get; }

    public bool IsPaused { get; }

    public bool IsStarted { get; }

    public DateTime? LastActive { get; }

    public DateTime? LastCancelled { get; }

    public DateTime? LastCompleted { get; }

    public DateTime? LastPaused { get; }

    public double Progress { get; }
}

/// <summary>
/// Extends <see cref="ITask"/>, <see cref="Task"/>.
/// </summary>
[Extend<ITask>, Extend<Task>]
public static class XTask;
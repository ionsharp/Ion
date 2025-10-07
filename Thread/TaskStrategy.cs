namespace Ion.Threading;

public enum TaskStrategy
{
    CancelAndRestart,
    FinishAndRestart,
    Ignore
}
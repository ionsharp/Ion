namespace Ion.Threading;

public delegate void TaskActiveEventHandler(Taskable task, TaskEventArgs e);

public delegate void TaskCancelledEventHandler(Taskable task, TaskEventArgs e);

public delegate void TaskCompletedEventHandler(Taskable task, TaskEventArgs e);

public delegate void TaskPausedEventHandler(Taskable task, TaskEventArgs e);

public delegate void TaskProgressedEventHandler(Taskable task, TaskProgressedEventArgs e);
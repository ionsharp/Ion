using System.Threading;

namespace Ion.Threading;

public delegate void TaskManaged(CancellationToken token);

public delegate void TaskManaged<T>(T input, CancellationToken token);
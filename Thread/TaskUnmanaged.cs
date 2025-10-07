using System.Threading;
using System.Threading.Tasks;

namespace Ion.Threading;

public delegate Task TaskUnmanaged(CancellationToken token);

public delegate Task TaskUnmanaged<T>(T input, CancellationToken token);
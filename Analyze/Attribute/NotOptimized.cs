using System;

namespace Ion;

/// <summary>
/// Indicates something isn't optimized (or performs poorly).
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class NotOptimizedAttribute(string Message = "") : NotAttribute(Message);
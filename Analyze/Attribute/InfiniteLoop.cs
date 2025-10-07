using System;

namespace Ion;

/// <summary>
/// Indicates an infinite loop occurs.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class InfiniteLoopAttribute(string Message = "") : NotAttribute(Message);
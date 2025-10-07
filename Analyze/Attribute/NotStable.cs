using System;

namespace Ion;

/// <summary>
/// Indicates something isn't stable.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class NotStableAttribute(string Message = "") : NotAttribute(Message);
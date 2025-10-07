using System;

namespace Ion;

/// <summary>
/// Indicates something isn't complete.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class NotCompleteAttribute(string Message = "") : NotAttribute(Message);
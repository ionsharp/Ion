using System;

namespace Ion;

/// <summary>
/// Indicates something isn't solved.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class NotSolvedAttribute(string Message = "") : NotAttribute(Message);
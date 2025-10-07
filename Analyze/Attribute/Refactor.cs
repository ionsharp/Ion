using System;

namespace Ion;

/// <summary>
/// Indicates something needs refactored.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class RefactorAttribute(string Message = "") : NotAttribute(Message);
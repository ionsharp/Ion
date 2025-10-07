using System;

namespace Ion;

/// <summary>
/// Indicates something isn't tested.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class NotTestedAttribute(string Message = "") : NotAttribute(Message);
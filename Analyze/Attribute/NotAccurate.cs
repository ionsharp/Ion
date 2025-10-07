using System;

namespace Ion;

/// <summary>
/// Indicates something isn't accurate.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class NotAccurateAttribute(string Message = "") : NotAttribute(Message);
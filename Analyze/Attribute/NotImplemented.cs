using System;

namespace Ion;

/// <summary>
/// Indicates something isn't implemented.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public sealed class NotImplementedAttribute(params object[] Messages) : NotAttribute(Messages.ToString(", "));
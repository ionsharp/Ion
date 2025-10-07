using System;

namespace Ion;

/// <summary>
/// Indicates something isn't something.
/// </summary>
[AttributeUsage(AttributeTargets.All), Obsolete]
public abstract class NotAttribute(string Message) : Attribute(), IAttributeWithMessage
{
    public string Message { get; private set; } = Message;
}
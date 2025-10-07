using System;

namespace Ion;

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct)]
public sealed class GroupAttribute() : LocalizableAttribute()
{
    public const string Default = "General";

    public object Index { get; set; }

    public object Name { get; set; }

    public GroupAttribute(object group) : this() => Name = group;
}
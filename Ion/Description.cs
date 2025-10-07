using Ion.Text;
using System;

namespace Ion;

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
public sealed class DescriptionAttribute(string description = "", bool localize = true) : LocalizableAttribute(localize)
{
    public string Description { get; set; } = description;

    public Format Format { get; set; } = Format.MarkUp;
}
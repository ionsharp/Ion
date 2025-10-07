using System;

namespace Ion;

/// <inheritdoc/>
public abstract class LocalizableAttribute(bool Localize = true) : Attribute()
{
    public bool Localize { get; set; } = Localize;
}

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
public sealed class NameAttribute(string Name = "", bool Localize = true) : LocalizableAttribute(Localize)
{
    public string Name { get; set; } = Name;
}
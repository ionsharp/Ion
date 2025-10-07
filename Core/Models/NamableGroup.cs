namespace Ion.Core;

/// <inheritdoc/>
public record class NamableGroup<T> : Namable<T>
{
    public string Group { get => Get(""); set => Set(value); }

    public NamableGroup() : base() { }

    public NamableGroup(string name, string group = default) : base(name) => Group = group;

    public NamableGroup(string name, string group, T value) : base(name, value) => Group = group;
}
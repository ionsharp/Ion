using System.Reflection;

namespace Ion.Reflect;

/// <summary>
/// Extends <see cref="FieldInfo"/>.
/// </summary>
[Extend<FieldInfo>]
public static class XFieldInfo
{
    public static bool IsGettable(this FieldInfo i)
        => i.IsPublic;

    public static bool IsSettable(this FieldInfo i)
        => !i.IsInitOnly && i.IsPublic;
}
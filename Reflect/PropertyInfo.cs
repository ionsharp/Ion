using System.Reflection;

namespace Ion.Reflect;

/// <summary>
/// Extends <see cref="PropertyInfo"/>.
/// </summary>
[Extend<PropertyInfo>]
public static class XPropertyInfo
{
    public static bool IsGettable(this PropertyInfo i)
        => i.CanRead && i.GetGetMethod(true) != null;

    public static bool IsIndexor(this PropertyInfo i)
        => i is PropertyInfo property
        && property.GetIndexParameters()?.Length > 0;

    public static bool IsSettable(this PropertyInfo i)
        => i.CanWrite && i.GetSetMethod(true) != null;

    public static bool IsStatic(this PropertyInfo i)
        => i.GetAccessors(true)[0].IsStatic;
}
using System.Reflection;

namespace Ion.Reflect;

/// <summary>
/// Extends <see cref="MethodInfo"/>.
/// </summary>
[Extend<MethodInfo>]
public static class XMethodInfo
{
    public static bool IsEvent(this MethodInfo i) => (i.Name.StartsWith(nameof(Accessors.add)) || i.Name.StartsWith(nameof(Accessors.remove))) && i.IsSpecialName;

    public static bool IsGetter(this MethodInfo i) => i.Name.StartsWith(nameof(Accessors.get)) && i.IsSpecialName;

    public static bool IsSetter(this MethodInfo i) => i.Name.StartsWith(nameof(Accessors.set)) && i.IsSpecialName;
}
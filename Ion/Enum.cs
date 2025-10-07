using Ion;
using Ion.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion;

/// <summary>
/// Extends <see cref="Enum"/>.
/// </summary>
[Extend<Enum>]
public static class XEnum
{
    public static Enum AddFlag(this Enum i, Enum j) => (Enum)Enum.ToObject(i.GetType(), i.To<int>() | j.To<int>());

    public static Enum RemoveFlag(this Enum i, Enum j) => (Enum)Enum.ToObject(i.GetType(), i.To<int>() & ~j.To<int>());

    public static Attribute GetAttribute(this Enum i, Type attribute) => i.GetAttributes()
        .FirstOrDefault(x => x.GetType() is Type y && attribute is Type z && (y.Inherits(z) || z.IsInterface && y.Implements(z)));

    public static T GetAttribute<T>(this Enum i) where T : Attribute => i.GetAttribute(typeof(T)) as T;

    public static IEnumerable<Attribute> GetAttributes(this Enum i) => i?.GetType().GetField($"{i}")?.GetCustomAttributes(false).Cast<Attribute>() ?? [];

    public static IEnumerable<Attribute> GetAttributes<T>(this Enum i) where T : Attribute => i.GetAttributes().Where<T>();

    public static IEnumerable<T> GetFlags<T>(this T i) where T : Enum => Enum.GetValues(typeof(T)).Where<T>(j => j.HasFlag(i));

    public static IEnumerable<T> GetFlags<T>(this Enum i) where T : Enum => Enum.GetValues(typeof(T)).Where<T>(j => j.HasFlag(i));

    public static bool HasAttribute(this Enum i, Type attribute) => i.GetAttribute(attribute) is not null;

    public static bool HasAttribute<T>(this Enum i) where T : Attribute => i.HasAttribute(typeof(T));
}
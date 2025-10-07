using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ion.Reflect;

/// <summary>
/// Extends <see cref="MemberInfo"/>.
/// </summary>
[Extend<MemberInfo>]
public static class XMemberInfo
{
    #region GetAccess

    private static readonly List<Access> AccessModifiers
        = [Access.Private, Access.Protected, Access.ProtectedInternal, Access.Internal, Access.Public];

    public static Access GetAccess(this FieldInfo field)
    {
        if (field.IsPrivate)
            return Access.Private;

        if (field.IsAssembly)
            return Access.Internal;

        if (field.IsFamily)
            return Access.Protected;
        if (field.IsFamilyAndAssembly)
            return Access.ProtectedInternal;
        if (field.IsFamilyOrAssembly)
            return Access.ProtectedInternal;

        if (field.IsPublic)
            return Access.Public;

        return Access.Undefined;
    }

    public static Access GetAccess(this MethodBase input)
    {
        if (input.IsPrivate)
            return Access.Private;

        if (input.IsFamily)
            return Access.Protected;

        if (input.IsFamilyOrAssembly)
            return Access.ProtectedInternal;

        if (input.IsAssembly)
            return Access.Internal;

        if (input.IsPublic)
            return Access.Public;

        return Access.Undefined;
    }

    public static Access GetAccess(this PropertyInfo property)
    {
        if (property.SetMethod is null)
            return property.GetMethod.GetAccess();

        if (property.GetMethod is null)
            return property.SetMethod.GetAccess();

        var max = Math.Max(AccessModifiers.IndexOf(property.GetMethod.GetAccess()),
            AccessModifiers.IndexOf(property.SetMethod.GetAccess()));

        return AccessModifiers[max];
    }

    public static Access GetAccess(this Type input)
    {
        if (input.IsNotPublic)
            return Access.Private;

        if (input.IsNestedFamily)
            return Access.Protected;

        if (input.IsNestedFamORAssem)
            return Access.ProtectedInternal;

        if (input.IsNestedAssembly)
            return Access.Internal;

        if (input.IsPublic)
            return Access.Public;

        return Access.Undefined;
    }

    public static Access GetAccess(this MemberInfo member)
    {
        if (member is FieldInfo field)
            return field.GetAccess();

        if (member is MethodBase method)
            return method.GetAccess();

        if (member is PropertyInfo property)
            return property.GetAccess();

        if (member is Type type)
            return type.GetAccess();

        return Access.Undefined;
    }

    #endregion

    #region GetInstanceType

    public static MemberInstanceType GetInstanceType(this MemberInfo member) =>
          member is EventInfo
        ? MemberInstanceType.Event
        : member is FieldInfo
        ? MemberInstanceType.Field
        : member is MethodInfo
        ? MemberInstanceType.Method
        : member is PropertyInfo
        ? MemberInstanceType.Property
        : MemberInstanceType.None;

    #endregion

    #region GetAttribute

    public static T GetAttribute<T>(this MemberInfo i) where T : Attribute
        => (T)i.GetAttribute(typeof(T));

    public static Attribute GetAttribute(this MemberInfo i, Type type)
    {
        foreach (var j in i.GetCustomAttributes(true))
        {
            if (j.GetType().Inherits(type))
                return (Attribute)j;
        }
        return null;
    }

    #endregion

    #region GetAttributes

    public static IEnumerable<T> GetAttributes<T>(this MemberInfo i) where T : Attribute
    {
        foreach (var j in i.GetCustomAttributes(true))
        {
            if (j is T k)
                yield return k;
        }
        yield break;
    }

    public static IEnumerable<Attribute> GetAttributes(this MemberInfo i)
        => i.GetAttributes<Attribute>();

    #endregion

    #region GetMemberType

    public static Type GetMemberType(this MemberInfo input) =>
        input is EventInfo e
        ? e.EventHandlerType
            : input is FieldInfo f
        ? f.FieldType
            : input is MethodInfo m
        ? m.ReturnType
            : input is PropertyInfo p
        ? p.PropertyType
            : null;

    #endregion

    #region HasAttribute

    public static bool HasAttribute<T>(this MemberInfo i)
        => i.HasAttribute(typeof(T));

    public static bool HasAttribute(this MemberInfo i, Type type)
        => i.GetAttribute(type) is not null;

    #endregion

    #region IsSettable

    public static bool IsSettable(this MemberInfo i) =>
        i is FieldInfo a
            ? a.IsSettable() :
        i is PropertyInfo b && b.IsSettable();

    #endregion

    #region IsStatic

    public static bool IsStatic(this MemberInfo i) =>
        i is FieldInfo a
            ? a.IsStatic :
        i is MethodInfo b
            ? b.IsStatic :
        i is PropertyInfo c
            ? c.IsStatic() :
        ///Be careful when dealing with CLR types from other languages
        ///https://stackoverflow.com/questions/1175888/determine-if-a-type-is-static
        i is Type d && d.IsAbstract && d.IsSealed;

    #endregion
}
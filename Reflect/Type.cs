using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ion.Reflect;

/// <summary>
/// Extends <see cref="Type"/>.
/// </summary>
[Extend<Type>]
public static class XType
{
    /// Create
    #region

    /// <inheritdoc cref="Activator.CreateInstance(Type, object[])"/>
    public static T Create<T>(this Type input, params object[] parameters) => (T)Activator.CreateInstance(input, parameters);

    #endregion

    /// GetBaseTypes
    #region

    /// <summary>
    /// Gets all types a type derives from.
    /// </summary>
    public static IEnumerable<Type> GetBaseTypes(this Type input)
    {
        Type result = input;
        while (!result.Equals(typeof(object)))
        {
            result = result.BaseType;
            yield return result;
        }
        yield break;
    }

    #endregion

    /// GetDefaultValue
    #region

    public static T GetDefaultValue<T>() => default;

    public static object GetDefaultValue(this Type i)
    {
        //Null
        object result = null;
        if (i is null) return null;

        //Enum
        if (i.IsEnum)
            return i.GetEnumValues().FirstOrDefault();

        //String
        else if (i == typeof(string))
            return "";

        //Class
        if (i.IsClass)
            return i.Create<object>();

        //Other
        else if (i.IsValueType)
            return typeof(XType).GetMethod(nameof(GetDefaultValue)).MakeGenericMethod(i).Invoke(null, null);

        return result;
    }

    #endregion

    /// GetEnumValues
    #region

    /// <summary>
    /// Get <see cref="Enum"/> values of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="NotSupportedException"/>
    public static IEnumerable<Enum> GetEnumValues(this Type i, Browse browse = Browse.Visible, bool sort = false)
        => i.GetEnumValues<Enum>(browse, sort);

    /// <summary>
    /// Get <see cref="Enum"/> values of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="NotSupportedException"/>
    public static IEnumerable<T> GetEnumValues<T>(this Type i, Browse browse = Browse.Visible, bool sort = false) where T : Enum
        => i.GetEnumValues(browse, j => (T)j, sort);

    /// <summary>
    /// Get <see cref="Enum"/> values of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="NotSupportedException"/>
    public static IEnumerable<T> GetEnumValues<T>(this Type i, Browse browse, Func<Enum, T> select, bool sort = false)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArgumentOutOfRangeException>(!i.IsEnum, nameof(i));

        Throw.If<NotSupportedException>(browse == Browse.None, nameof(browse));
        Throw.IfNull(select, nameof(select));

        var result = i.GetEnumValues().Cast<Enum>();
        result = sort ? result.OrderBy(j => j.ToString()) : result;

        foreach (var j in result)
        {
            switch (browse)
            {
                case Browse.Hidden:
                    if (Instance.IsHidden(j))
                        goto case Browse.All;

                    break;

                case Browse.Visible:
                    if (!Instance.IsHidden(j))
                        goto case Browse.All;

                    break;

                case Browse.All:
                    yield return select(j);
                    break;
            }
        }
    }

    #endregion

    /// GetKind
    #region

    public static TypeKind GetKind(this Type input)
    {
        if (input.IsClass)
            return TypeKind.Class;

        if (typeof(Delegate).IsAssignableFrom(input))
            return TypeKind.Delegate;

        if (input.IsEnum)
            return TypeKind.Enum;

        if (input.IsInterface)
            return TypeKind.Interface;

        if (input.IsValueType)
            return TypeKind.Struct;

        return TypeKind.Unknown;
    }

    #endregion

    /// GetMembers
    #region

    private static bool Check(bool? a, Func<bool> b)
        => a is null || (a.Value ? b() : !b());

    public static IEnumerable<MemberInfo> GetMembers(this object i, BindingFlags flags = Instance.Flag.Public, MemberTypes types = Instance.Flag.Types, bool? hidden = null, bool? settable = null, bool? serializable = null, bool? @static = false)
        => Instance.AsType(i).GetMembers(flags).Where<MemberInfo>(j =>
            types.HasFlag(j.MemberType)
            && Check(hidden,
                () => Instance.IsHidden(j))
            && Check(settable,
                j.IsSettable)
            && Check(serializable,
                () =>  !Instance.HasAttribute<NonSerializedAttribute>(j)
                    && !Instance.HasAttribute<NonSerializableAttribute>(j) 
                    && !Instance.HasAttribute<System.Xml.Serialization.XmlIgnoreAttribute>(j))
            && Check(@static,
                j.IsStatic));

    #endregion

    /// GetRealName
    #region

    public static string GetRealName(this Type input, bool full)
    {
        if (!input.IsGenericType)
            return input.Name;

        var result = new StringBuilder();
        try
        {
            //Not always?
            _ = result.Append(input.Name[..input.Name.IndexOf('`')]);
        }
        catch
        {
            return input.Name;
        }
        _ = result.Append('<');

        var appendComma = false;
        foreach (Type arg in input.GetGenericArguments())
        {
            if (appendComma) _ = result.Append(',');
            _ = result.Append(arg.GetRealName(full));
            appendComma = true;
        }

        _ = result.Append('>');

        if (full)
            _ = result.Insert(0, input.FullName[..^input.Name.Length]);

        return result.ToString();
    }

    #endregion

    /// GetSharedType
    #region

    public static Type GetSharedType(this IEnumerable<Type> types)
    {
        if (types is null)
            return null;

        Type a = null;
        foreach (var i in types)
        {
            if (a is null)
            {
                a = i;
                continue;
            }

            var b = i;

            //Compare a and b
            while (a != typeof(object))
            {
                while (b != typeof(object))
                {
                    if (a == b)
                        goto next;

                    b = b.BaseType;
                }
                a = a.BaseType;
                b = i;
            }

            return null;
        next: continue;
        }
        return a;
    }

    #endregion

    /// Implements
    #region

    /// <summary>
    /// Gets whether or not a type implements an interface of <see cref="{T}"/>.
    /// </summary>
    public static bool Implements<T>(this Type input)
        => input.Implements(typeof(T));

    /// <summary>
    /// Gets whether or not a type implements an interface of the given type.
    /// </summary>
    public static bool Implements(this Type input, Type b)
    {
        if (b.GetTypeInfo().IsInterface)
            return b.IsAssignableFrom(input);

        //throw new InvalidCastException("Type is not an interface.");
        return false;
    }

    #endregion

    /// Inherits
    #region

    /// <summary>
    /// Gets whether or not a type inherits <see cref="{T}"/>.
    /// </summary>
    public static bool Inherits<T>(this Type input) => input.Inherits(typeof(T));

    /// <summary>
    /// Gets whether or not a type inherits the given type.
    /// </summary>
    public static bool Inherits(this Type a, Type b)
    {
        if (a?.IsClass == true && b?.IsClass == true)
        {
            while (!a.Equals(typeof(object)))
            {
                if (a.Equals(b))
                    return true;

                a = a.BaseType;
            }
        }
        return false;
    }

    #endregion

    /// IsGeneric
    #region

    public static bool IsGeneric<T>(this Type input)
    {
        foreach (var i in input.GenericTypeArguments)
        {
            if (i == typeof(T))
                return true;
        }
        return false;
    }

    #endregion

    /// IsGenericOf
    #region

    public static bool IsGenericOf<T>(this Type input)
    {
        foreach (var i in input.GenericTypeArguments)
        {
            if (i.IsSubclassOf(typeof(T)))
                return true;
        }
        return false;
    }

    #endregion

    /// IsNullable
    #region

    /// <summary>
    /// Gets whether or not the type is <see cref="Nullable{T}"/>.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsNullable(this Type input)
    {
        if (!input.GetTypeInfo().IsGenericType)
            return false;

        return input.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    #endregion
}
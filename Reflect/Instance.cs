using Ion.Analysis;
using Ion.Collect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Ion.Reflect;

[Extend<Object>]
public static partial class Instance
{
    /// <see cref="Region.Field"/>
    #region

    /// <summary>
    /// If the input is a collection, this stores the items.
    /// </summary>
    public const string CollectionKey = "this";

    /// <summary>
    /// Types to ignore.
    /// </summary>
    public static readonly Type[] NonSerializableTypes = [ typeof(nint), typeof(nuint) ];

    #endregion

    /// <see cref="Region.Method.Private"/>
    #region

    private static void EachMember(object i, Void<FieldInfo, object> f, Void<PropertyInfo, object> p,
        BindingFlags flags, bool deep, bool log,
        List<Type> types)
    {
        var type = AsType(i);
        if (type == typeof(string)
            || type == typeof(Type)
            || Primitives.All.Contains(type)
            || type.Namespace.StartsWith($"{nameof(System)}.{nameof(System.Reflection)}"))
            return;

        void print(Result j) => log.If(() => Log.Write(j));

        ///First time!
        if (types == null)
            types = [type];

        ///Avoid infinite loop (type may contain reference to self somewhere deep within structure)
        else if (types.Contains(type))
            return;

        //Back again
        else types.Add(type);

        var t = f is not null
            ? MemberTypes.Field
            : p is not null
            ? MemberTypes.Property
            : throw new NotSupportedException();

        var members = type.GetMembers(flags, t, null, true, true);

        foreach (var member in members)
        {
            object value;
            if (member is FieldInfo field)
            {
                //1) Get the value
                value = field.GetValue(i);

                //2) Do action with value
                f(field, value);

                //3) Enumerate the value (if possible)
                deep.If(() => EachMember(value, f, p, flags, deep, log, types));
            }
            else if (member is PropertyInfo property && property.IsGettable())
            {
                //1) Get the value
                value = property.GetValue(i);

                //2) Do action with value
                p(property, value);

                //3) Enumerate the value (if possible)
                deep.If(() => EachMember(value, f, p, flags, deep, log, types));
            }
        }
        _ = types.Remove(type);
    }

    private static void EachMember(object i, Void<FieldInfo, object> f, Void<PropertyInfo, object> p,
        BindingFlags flags, bool deep = false, bool log = false)
        => EachMember(i, f, p, flags, deep, log, null);

    #endregion

    /// <see cref="Region.Method.Public"/>
    #region

    /// AsType
    #region

    /// <summary>
    /// Get <see cref="object"/> as <see cref="Type"/>.
    /// </summary>
    /// <returns>
    /// The <see cref="object"/> as <see cref="Type"/>, or <see cref="object.GetType"/> (if not <see langword="null"/>).
    /// </returns>
    public static Type AsType(object i) => i as Type ?? i?.GetType();

    #endregion

    /// CloneDeep
    #region

    /// <summary>
    /// Get deep clone.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [Recursive]
    private static T CloneDeep<T>(T input, ICreateFromObject create, bool log, List<Type> visited, int depth = 0)
    {
        //(a) Null
        if (input is null)
        {
            if (depth == 0)
                throw new ArgumentNullException(nameof(input));

            if (log) Log.Write(new ArgumentNullException(nameof(input)));
            return default;
        }

        var type = input.GetType();
        visited.Add(type);

        void print(string m) => log.If(() => Log.Write($"Clone (Depth = {depth} | Type = {type.FullName}): " + m));

        #region (a) Assembly | Type

        if (input is Assembly || input is Type)
        {
            print($"Assembly | Type");
            return default;
        }

        #endregion

        #region (b) Enum | String | Struct

        if (type.IsEnum || input is string || type.IsValueType)
        {
            print($"Enum | String | Struct");
            return input;
        }

        #endregion

        #region (c) NonCloneable

        if (type.HasAttribute<NonCloneableAttribute>())
        {
            print($"Not cloneable.");
            return default;
        }

        #endregion

        #region (d) Clone handler

        if (create is not null)
        {
            if (Try.Get(() => create.Create(input), e => log.If(() => Log.Write(e))) is T handle)
            {
                print($"Handled clone.");
                return handle;
            }
        }

        #endregion

        #region (e) Manual creation

        print($"Manually creating.");

        var flags = Flag.Private | Flag.Public;

        T result = default;
        _ = Try.Do(() =>
        {
            result = type.Create<T>();
            EachMember
            (
                input,
                (field, value)
                    => field.IfNotNull(i => !i.HasAttribute<NonCloneableAttribute>()
                    && i.IsSettable(), i => i.SetValue(result, CloneDeep(value, create, log, visited, depth + 1))),
                (property, value)
                    => property.IfNotNull(i => !i.HasAttribute<NonCloneableAttribute>()
                    && i.IsSettable(), i => i.SetValue(result, CloneDeep(value, create, log, visited, depth + 1))),
                flags
            );
            print($"Manual creation was successful!");
        },
        e => log.If(() => Log.Write(e)));

        #endregion

        _ = visited.Remove(type);
        return result;
    }

    /// <summary>
    /// Get deep clone.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T CloneDeep<T>(T i, bool log = false) => CloneDeep(i, null, log);

    /// <summary>
    /// Get deep clone.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T CloneDeep<T>(this T i, ICreateFromObject create, bool log = false) => CloneDeep(i, create, log, []);

    #endregion

    /// CloneShallow [<see cref="ObsoleteAttribute"/>]
    #region

    /// <summary>
    /// Get shallow clone.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>https://stackoverflow.com/questions/41998366/how-does-memberwiseclone-create-a-new-object-with-the-cloned-properties</remarks>
    [Obsolete]
    public static T CloneShallow<T>(T i, BindingFlags flags = Flag.Private | Flag.PublicDeclared)
    {
        Throw.IfNull(i, nameof(i));
        var clone = FormatterServices.GetUninitializedObject(i.GetType());
        for (var type = i.GetType(); type != null; type = type.BaseType)
        {
            var fields = type.GetFields(flags);
            foreach (var j in fields)
                j.SetValue(clone, j.GetValue(i));
        }
        return (T)clone;
    }

    #endregion

    /// EachField
    #region

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="field"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachField(object i, Void<FieldInfo> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, (j, k) => action(j), null, flags, deep, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="field"/> and <see langword="value"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachField(object i, Void<FieldInfo, object> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, action, null, flags, deep, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <i>top level</i> <see langword="field"/>, including setting a new <see langword="value"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachField(object i, Func<FieldInfo, object, object> action, BindingFlags flags = Flag.Public, bool log = false)
        => EachMember(i, (j, k) =>
        {
            var x = action(j, k);
            j.SetValue(i, x);
        },
        null, flags, false, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="field"/> and <see langword="value"/> that satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachFieldThat(object i, Condition<FieldInfo, object> that, Void<FieldInfo, object> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, (j, k) => that(j, k).If(() => action(j, k)), null, flags, deep, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="field"/> and <see langword="value"/> that implements <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachFieldThatImplements<T>(object i, Void<FieldInfo, object> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, (j, k) => j.FieldType.Implements<T>().If(() => action(j, k)), null, flags, deep, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="field"/> and <see langword="value"/> that inherits <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachFieldThatInherits<T>(object i, Void<FieldInfo, object> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, (j, k) => j.FieldType.Inherits<T>().If(() => action(j, k)), null, flags, deep, log);

    #endregion

    /// EachProperty
    #region

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="property"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachProperty(object i, Void<PropertyInfo> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, null, (j, k) => action(j), flags, deep, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="property"/> and <see langword="value"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachProperty(object i, Void<PropertyInfo, object> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, null, action, flags, deep, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <i>top level</i> <see langword="property"/>, including setting a new <see langword="value"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachProperty(object i, Func<PropertyInfo, object, object> action, BindingFlags flags = Flag.Public, bool log = false)
        => EachMember(i, null, (j, k) =>
        {
            var x = action(j, k);
            j.SetValue(i, x);
        },
        flags, false, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="property"/> and <see langword="value"/> that satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachPropertyThat(object i, Condition<PropertyInfo, object> that, Void<PropertyInfo, object> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, null, (j, k) => that(j, k).If(() => action(j, k)), flags, deep, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="property"/> and <see langword="value"/> that implements <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachPropertyThatImplements<T>(object i, Void<PropertyInfo, object> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, null, (j, k) => j.PropertyType.Implements<T>().If(() => action(j, k)), flags, deep, log);

    /// <summary>
    /// Do given <see cref="Action"/> with each <see langword="property"/> and <see langword="value"/> that inherits <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void EachPropertyThatInherits<T>(object i, Void<PropertyInfo, object> action, BindingFlags flags = Flag.Public, bool deep = false, bool log = false)
        => EachMember(i, null, (j, k) => j.PropertyType.Inherits<T>().If(() => action(j, k)), flags, deep, log);

    #endregion

    /// GetAttribute
    #region

    /// <summary>
    /// Get <see cref="Attribute"/>.
    /// </summary>
    public static Attribute GetAttribute(object i, Type attribute)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(attribute, nameof(attribute));

        if (i is Enum a)
            return a.GetAttribute(attribute);

        if (i is MemberInfo b)
            return b.GetAttribute(attribute);

        if (i is object c)
            return c.GetType().GetAttribute(attribute);

        return null;
    }

    /// <summary>
    /// Get <see cref="Attribute"/>.
    /// </summary>
    public static T GetAttribute<T>(this object i) where T : Attribute => (T)GetAttribute(i, typeof(T));

    #endregion

    /// GetDescription
    #region

    public static string GetDescription(object i)
        => i?.GetAttribute<System.ComponentModel.DescriptionAttribute>()?.Description ?? i?.GetAttribute<DescriptionAttribute>()?.Description;

    #endregion

    /// GetFieldsThat
    #region

    /// <summary>
    /// Get fields that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<FieldInfo> GetFieldsThat(object i, Condition<FieldInfo> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetFields(flags).Where(j => that(j));
    }

    /// <summary>
    /// Get fields that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<FieldInfo> GetFieldsThat(object i, Condition<FieldInfo, object> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetFields(flags).Where(j => that(j, j.GetValue(i)));
    }

    /// <summary>
    /// Get fields that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<FieldInfo> GetFieldsThat<T>(object i, Condition<FieldInfo> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetFields(flags).Where(j => that(j));
    }

    /// <summary>
    /// Get fields that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<FieldInfo> GetFieldsThat<T>(object i, Condition<FieldInfo, T> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetFields(flags).Where(j => that(j, (T)j.GetValue(i)));
    }

    /// <summary>
    /// Get values of fields that implement <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<FieldInfo> GetFieldsThatImplement<T>(object i, BindingFlags flags = Flag.Public)
    {
        Throw.If<ArgumentException>(!typeof(T).IsInterface, typeof(T).FullName);
        return GetFieldsThat<T>(i, j => j.FieldType.Implements<T>(), flags);
    }

    /// <summary>
    /// Get values of fields that inherit <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<FieldInfo> GetFieldsThatInherit<T>(object i, BindingFlags flags = Flag.Public) where T : class
        => GetFieldsThat<T>(i, j => j.FieldType.Inherits<T>(), flags);

    #endregion

    /// GetFieldValue
    #region

    /// <summary>
    /// Get <see langword="value"/> of <see langword="field"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="FieldInfo.GetValue(object?)"/>
    public static object GetFieldValue(object i, string name)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(name, nameof(name));
        return i.GetType().GetField(name).GetValue(i);
    }

    /// <summary>
    /// Get <see langword="value"/> of <see langword="field"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidCastException"/>
    /// <inheritdoc cref="FieldInfo.GetValue(object?)"/>
    public static T GetFieldValue<T>(object i, string name) => (T)i.GetType().GetField(name).GetValue(i);

    #endregion

    /// GetFieldValuesThat
    #region

    /// <summary>
    /// Get values of fields that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="FieldInfo.GetValue(object?)"/>
    public static IEnumerable GetFieldValuesThat(object i, Condition<object> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetFields(flags).Where(j => that(j.GetValue(i))).Select(j => j.GetValue(i)).Where(j => j is not null);
    }

    /// <summary>
    /// Get values of fields that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="FieldInfo.GetValue(object?)"/>
    public static IEnumerable GetFieldValuesThat(object i, Condition<FieldInfo, object> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetFields(flags).Where(j => that(j, j.GetValue(i))).Select(j => j.GetValue(i)).Where(j => j is not null);
    }

    /// <summary>
    /// Get values of fields that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="FieldInfo.GetValue(object?)"/>
    public static IEnumerable<T> GetFieldValuesThat<T>(object i, Condition<T> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetFields(flags).Where(j => that((T)j.GetValue(i))).Select(j => (T)j.GetValue(i)).Where(j => j is not null);
    }

    /// <summary>
    /// Get values of fields that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="FieldInfo.GetValue(object?)"/>
    public static IEnumerable<T> GetFieldValuesThat<T>(object i, Condition<FieldInfo, T> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetFields(flags).Where(j => that(j, (T)j.GetValue(i))).Select(j => (T)j.GetValue(i)).Where(j => j is not null);
    }

    /// <summary>
    /// Get values of fields that implement <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="FieldInfo.GetValue(object?)"/>
    public static IEnumerable<T> GetFieldValuesThatImplement<T>(object i, BindingFlags flags = Flag.Public)
    {
        Throw.If<ArgumentException>(!typeof(T).IsInterface, typeof(T).FullName);
        return GetFieldValuesThat<T>(i, (j, _) => j.FieldType.Implements<T>(), flags);
    }

    /// <summary>
    /// Get values of fields that inherit <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="FieldInfo.GetValue(object?)"/>
    public static IEnumerable<T> GetFieldValuesThatInherit<T>(object i, BindingFlags flags = Flag.Public) where T : class
        => GetFieldValuesThat<T>(i, (j, _) => j.FieldType.Inherits<T>(), flags);

    #endregion

    /// GetName
    #region

    public static string GetName(object i)
        => i?.GetAttribute<DisplayNameAttribute>()?.DisplayName;

    #endregion

    /// GetPropertiesThat
    #region

    /// <summary>
    /// Get properties that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<PropertyInfo> GetPropertiesThat(object i, Condition<PropertyInfo> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetProperties(flags).Where(j => that(j));
    }

    /// <summary>
    /// Get properties that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<PropertyInfo> GetPropertiesThat(object i, Condition<PropertyInfo, object> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetProperties(flags).Where(j => that(j, j.GetValue(i)));
    }

    /// <summary>
    /// Get properties that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<PropertyInfo> GetPropertiesThat<T>(object i, Condition<PropertyInfo> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetProperties(flags).Where(j => that(j));
    }

    /// <summary>
    /// Get properties that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<PropertyInfo> GetPropertiesThat<T>(object i, Condition<PropertyInfo, T> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetProperties(flags).Where(j => that(j, (T)j.GetValue(i)));
    }

    /// <summary>
    /// Get properties that implement <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<PropertyInfo> GetPropertiesThatImplement<T>(object i, BindingFlags flags = Flag.Public)
    {
        Throw.If<ArgumentException>(!typeof(T).IsInterface, typeof(T).FullName);
        return GetPropertiesThat<T>(i, j => j.PropertyType.Implements<T>(), flags);
    }

    /// <summary>
    /// Get properties that inherit <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<PropertyInfo> GetPropertiesThatInherit<T>(object i, BindingFlags flags = Flag.Public) where T : class
        => GetPropertiesThat<T>(i, j => j.PropertyType.Inherits<T>(), flags);

    #endregion

    /// GetPropertyValue
    #region

    /// <summary>
    /// Get <see langword="value"/> of <see langword="property"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="PropertyInfo.GetValue(object?, object?[]?)"/>
    public static object GetPropertyValue(object i, string name, params object[] index)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(name, nameof(name));
        return i.GetType().GetProperty(name).GetValue(i, index);
    }

    /// <summary>
    /// Get <see langword="value"/> of <see langword="property"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidCastException"/>
    /// <inheritdoc cref="PropertyInfo.GetValue(object?, object?[]?)"/>
    public static T GetPropertyValue<T>(object i, string name, params object[] index) => (T)GetPropertyValue(i, name, index);

    #endregion

    /// GetPropertyValuesThat
    #region

    /// <summary>
    /// Get values of properties that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="PropertyInfo.GetValue(object?, object?[]?)"/>
    public static IEnumerable GetPropertyValuesThat(object i, Condition<object> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetProperties(flags).Where(j => that(j.GetValue(i))).Select(j => j.GetValue(i)).Where(j => j is not null);
    }

    /// <summary>
    /// Get values of properties that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="PropertyInfo.GetValue(object?, object?[]?)"/>
    public static IEnumerable GetPropertyValuesThat(object i, Condition<PropertyInfo, object> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetProperties(flags).Where(j => that(j, j.GetValue(i))).Select(j => j.GetValue(i)).Where(j => j is not null);
    }

    /// <summary>
    /// Get values of properties that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="PropertyInfo.GetValue(object?, object?[]?)"/>
    public static IEnumerable<T> GetPropertyValuesThat<T>(object i, Condition<T> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetProperties(flags).Where(j => that((T)j.GetValue(i))).Select(j => (T)j.GetValue(i)).Where(j => j is not null);
    }

    /// <summary>
    /// Get values of properties that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="PropertyInfo.GetValue(object?, object?[]?)"/>
    public static IEnumerable<T> GetPropertyValuesThat<T>(object i, Condition<PropertyInfo, T> that, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(that, nameof(that));
        return i.GetType().GetProperties(flags).Where(j => that(j, (T)j.GetValue(i))).Select(j => (T)j.GetValue(i)).Where(j => j is not null);
    }

    /// <summary>
    /// Get values of properties that implement <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="PropertyInfo.GetValue(object?, object?[]?)"/>
    public static IEnumerable<T> GetPropertyValuesThatImplement<T>(object i, BindingFlags flags = Flag.Public)
    {
        Throw.If<ArgumentException>(!typeof(T).IsInterface, typeof(T).FullName);
        return GetPropertyValuesThat<T>(i, (j, _) => j.PropertyType.Implements<T>(), flags);
    }

    /// <summary>
    /// Get values of properties that inherit <see cref="{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="PropertyInfo.GetValue(object?, object?[]?)"/>
    public static IEnumerable<T> GetPropertyValuesThatInherit<T>(object i, BindingFlags flags = Flag.Public) where T : class
        => GetPropertyValuesThat<T>(i, (j, _) => j.PropertyType.Inherits<T>(), flags);

    #endregion

    /// HasAttribute
    #region

    /// <summary>
    /// Get if has <see cref="Attribute"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool HasAttribute(object i, Type attribute)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(attribute, nameof(attribute));
        return GetAttribute(i, attribute) != null;
    }

    /// <summary>
    /// Get if has <see cref="Attribute"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool HasAttribute<T>(object i) where T : Attribute => HasAttribute(i, typeof(T));

    #endregion

    /// HasEvent
    #region

    /// <summary>
    /// Get if has <see langword="event"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="Type.GetEvent(string, BindingFlags)"/>
    public static bool HasEvent(object i, string name, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        return AsType(i).GetEvent(name, flags) is not null;
    }

    #endregion

    /// HasField
    #region

    /// <summary>
    /// Get if has <see langword="field"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="Type.GetField(string, BindingFlags)"/>
    public static bool HasField(object i, string name, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        return AsType(i).GetField(name, flags) is not null;
    }

    #endregion

    /// HasMethod
    #region

    /// <summary>
    /// Get if has <see langword="method"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="Type.GetMethod(string, BindingFlags)(string, BindingFlags)"/>
    public static bool HasMethod(object i, string name, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        return AsType(i).GetMethod(name, flags) is not null;
    }

    #endregion

    /// HasProperty
    #region

    /// <summary>
    /// Get if has <see langword="property"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="Type.GetProperty(string, BindingFlags)"/>
    public static bool HasProperty(object i, string name, BindingFlags flags = Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        return AsType(i).GetProperty(name, flags) is not null;
    }

    #endregion

    /// HasMemberWithAttribute
    #region

    /// <summary>
    /// Get if has <see cref="MemberInfo"/> with given <b>attribute</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool HasMemberWithAttribute(object i, Type attribute, Predicate<Attribute> where = null, BindingFlags flags = Flag.Public, MemberTypes types = MemberTypes.All)
    {
        Throw.IfNull(i, nameof(i));
        if (AsType(i) is Type type)
        {
            var members = type.GetMembers(flags, types);
            foreach (var j in members)
            {
                if (j.HasAttribute(attribute) && where?.Invoke(j.GetAttribute(attribute)) != false)
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Get if has <see cref="MemberInfo"/> with given <b>attribute</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool HasMemberWithAttribute<T>(object i, Predicate<T> where = null, BindingFlags flags = Flag.Public, MemberTypes types = MemberTypes.All) where T : Attribute
        => HasMemberWithAttribute(i, typeof(T), i => i is T j && where?.Invoke(j) != false, flags, types);

    /// <summary>
    /// Get if has <see cref="MemberInfo"/> with given <b>attributes</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool HasMemberWithAttributes(object i, in Type[] attributes, Predicate<Attribute> where = null, BindingFlags flags = Flag.Public, MemberTypes types = MemberTypes.All)
    {
        Throw.IfNull(i, nameof(i));
        if (AsType(i) is Type type)
        {
            var members = type.GetMembers(flags, types);
            foreach (var j in members)
            {
                var result = true;
                foreach (var k in attributes)
                {
                    if (!j.HasAttribute(k) || where?.Invoke(j.GetAttribute(k)) == false)
                    {
                        result = false;
                        break;
                    }
                }
                if (result)
                    return true;
            }
        }
        return false;
    }

    #endregion

    /// IsHidden
    #region

    /// <summary>
    /// Get if <see cref="BrowsableAttribute.Browsable"/> = <see langword="false"/> or <see cref="HideAttribute"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsHidden(object i)
    {
        Throw.IfNull(i, nameof(i));
        return i.GetAttribute<BrowsableAttribute>()?.Browsable == false || HasAttribute<HideAttribute>(i);
    }

    #endregion

    /// SetFieldValue
    #region

    /// <summary>
    /// Set <see langword="value"/> of <see langword="field"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="FieldInfo.SetValue(object?, object?)"/>
    public static void SetFieldValue<T>(object i, string name, T value = default)
    {
        Throw.IfNull(i, nameof(i));
        i.GetType().GetField(name).SetValue(i, value);
    }

    #endregion

    /// SetPropertyValue
    #region

    /// <summary>
    /// Set <see langword="value"/> of <see langword="property"/> with given <b>name</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <inheritdoc cref="PropertyInfo.SetValue(object?, object?)"/>
    public static void SetPropertyValue<T>(object i, string name, T value = default)
    {
        Throw.IfNull(i, nameof(i));
        i.GetType().GetProperty(name).SetValue(i, value);
    }

    #endregion

    /// Reset
    #region

    [Recursive]
    private static object _Reset([NotNullIfNotNull(nameof(i))] object i, BindingFlags flags, MemberTypes types)
    {
        if (i is null) return null;

        var type = i.GetType();

        //Value
        if (i is string || type.IsValueType)
            return i.GetType().GetDefaultValue();

        //Reference
        _Reset(i, flags, types);
        return i;
    }

    /// <summary>
    /// Reset given <see cref="object"/> by setting each settable field and property to <see langword="default"/>.
    /// </summary>
    /// <remarks>
    /// <b><see cref="Instance"/> of <see langword="class"/> can be reset when it's unsettable (it's reference wouldn't be reassigned).</b>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static void Reset<T>(T i, BindingFlags flags = Flag.Public, MemberTypes types = MemberTypes.Field | MemberTypes.Property) where T : class
    {
        Throw.IfNull(i, nameof(i));
        var type = i.GetType();

        void fAction(FieldInfo f, object v)
        {
            var a = _Reset(v, flags, types);
            if (f.IsSettable())
                f.SetValue(i, a);
        }
        void pAction(PropertyInfo p, object v)
        {
            var b = _Reset(v, flags, types);
            if (p.IsSettable())
                p.SetValue(i, b);
        }

        EachMember(i, fAction, pAction, flags);
    }

    #endregion

    /// SizeOf
    #region

    /// <summary>Get size of given <see cref="object"/>.</summary>
    /// <param name="i">The <see langword="object"/>.</param>
    /// <param name="averageStringSize">Average size of the <see langword="string"/>.</param>
    /// <returns>Approximate size of <see cref="object"/> (in bytes).</returns>
    /// <remarks>https://stackoverflow.com/questions/2331889/how-to-find-the-size-of-a-class-in-c-sharp</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static int SizeOf(object i, int averageStringSize = -1, BindingFlags flags = Flag.Private | Flag.Public)
    {
        Throw.IfNull(i, nameof(i));
        int pointerSize = nint.Size, size = 0;

        Type type = i.GetType();
        var info = type.GetFields(flags);

        foreach (var field in info)
        {
            if (field.FieldType.IsValueType)
                size += System.Runtime.InteropServices.Marshal.SizeOf(field.FieldType);

            else
            {
                size += pointerSize;
                if (field.FieldType.IsArray)
                {
                    if (field.GetValue(i) is Array array)
                    {
                        var elementType = array.GetType().GetElementType();
                        if (elementType.IsValueType)
                            size += System.Runtime.InteropServices.Marshal.SizeOf(field.FieldType) * array.Length;

                        else
                        {
                            size += pointerSize * array.Length;
                            if (elementType == typeof(string) && averageStringSize > 0)
                                size += averageStringSize * array.Length;
                        }
                    }
                }
                else if (field.FieldType == typeof(string) && averageStringSize > 0)
                    size += averageStringSize;
            }
        }
        return size;
    }

    #endregion

    /// (De)Virtualize
    #region

    /// <summary>
    /// Devirtualize from given <see cref="ObjectDictionary"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [Recursive]
    private static void Devirtualize([NotNull] object data, ObjectDictionary source, BindingFlags flags, MemberValueType types, List<Type> visited, int depth = 0)
    {
        if (data is null || source is null) return;
        var type = data.GetType();

        visited.Add(type);

        //a) Members
        foreach (var i in type.GetMembers(flags, types switch { MemberValueType.Field => MemberTypes.Field, MemberValueType.Property => MemberTypes.Property, _ => MemberTypes.Field | MemberTypes.Property }, null, true, true))
        {
            if (source.TryGetValue(i.Name, out var value))
            {
                var memberType = i.GetMemberType();
                if (NonSerializableTypes.Contains(memberType) || visited.Contains(memberType)) continue;

                if (value is Data j)
                {
                    _ = Try.Do(() =>
                    {
                        value = j.Type.Create<object>();
                        Devirtualize(value, j, flags, types, visited, depth + 1);
                    },
                    e => Log.Write(e));
                }

                var valueType = value?.GetType();
                if (valueType is not null)
                {
                    if (NonSerializableTypes.Contains(valueType) || visited.Contains(valueType))
                        continue;
                }

                _ = Try.Do(() =>
                {
                    if (i is FieldInfo fInfo)
                        fInfo.SetValue(data, value);

                    if (i is PropertyInfo pInfo)
                        pInfo.SetValue(data, value);
                }, e => Log.Write(e));
            }
        }

        //b) Items
        if (data is IList a)
        {
            if (source.TryGetValue(CollectionKey, out var value) && value is IList b)
            {
                b.ForEach(i =>
                {
                    var valueType = i is Data j ? j.Type : i?.GetType();
                    if (valueType is not null)
                    {
                        if (NonSerializableTypes.Contains(valueType) || visited.Contains(valueType)) return;
                        if (i is Data k)
                        {
                            _ = Try.Do(() =>
                            {
                                i = k.Type.Create<object>();
                                Devirtualize(i, k, flags, types, visited, depth + 1);
                            },
                            e => Log.Write(e));
                        }
                    }
                    i.IfNotNull(l => a.Add(l));
                });
            }
        }

        _ = visited.Remove(type);
    }

    /// <summary>
    /// Devirtualize from given <see cref="ObjectDictionary"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void Devirtualize(object i, ObjectDictionary source, BindingFlags flags = Flag.Public, MemberValueType types = MemberValueType.Both)
    {
        Throw.IfNull(i, nameof(i));
        Devirtualize(i, source, flags, types, []);
    }

    #endregion

    /// Virtualize
    #region

    /// <summary>
    /// Virtualize to given <see cref="ObjectDictionary"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [Recursive]
    private static void Virtualize([NotNull] object data, ObjectDictionary target, BindingFlags flags, MemberValueType types, List<Type> visited, int depth = 0)
    {
        if (data is null || target is null) return;

        target.Clear();
        var type = data.GetType();

        visited.Add(type);

        //a) Members
        foreach (var i in type.GetMembers(flags, types switch { MemberValueType.Field => MemberTypes.Field, MemberValueType.Property => MemberTypes.Property, _ => MemberTypes.Field | MemberTypes.Property }, null, true, true))
        {
            var memberType = i.GetMemberType();
            if (NonSerializableTypes.Contains(memberType) || visited.Contains(memberType)) continue;

            object value = null; Type valueType = null;

            _ = Try.Do(() =>
            {
                value
                    = i is FieldInfo f ? f.GetValue(data) : i is PropertyInfo p ? p.GetValue(data) : null;
                valueType
                    = value.GetType();
            },
            e => Log.Write(e));

            if (valueType is not null)
            {
                if (NonSerializableTypes.Contains(valueType) || visited.Contains(valueType)) continue;
                if (!HasAttribute<SerializableAttribute>(value))
                {
                    var j = new Data(valueType);
                    Virtualize(value, j, flags, types, visited, depth + 1);
                    value = j;
                }
            }

            target.Add(i.Name, value);
        }

        //b) Items
        if (data is IList list)
        {
            var items = new List<object>();
            target.Add(CollectionKey, items);

            list.ForEach(i =>
            {
                var value = i; Type valueType = value?.GetType();
                if (valueType is not null)
                {
                    if (NonSerializableTypes.Contains(valueType) || visited.Contains(valueType)) return;
                    if (!HasAttribute<SerializableAttribute>(value))
                    {
                        var j = new Data(valueType);
                        Virtualize(value, j, flags, types, visited, depth + 1);
                        value = j;
                    }
                    items.Add(value);
                }
            });
        }

        _ = visited.Remove(type);
    }

    /// <summary>
    /// Virtualize to given <see cref="ObjectDictionary"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void Virtualize(object i, ObjectDictionary target, BindingFlags flags = Flag.Public, MemberValueType types = MemberValueType.Both)
    {
        Throw.IfNull(i, nameof(i));
        Virtualize(i, target, flags, types, []);
    }

    #endregion

    #endregion
}
using Ion;
using Ion.Reflect;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Ion.Core;

[Extend<IModel>]
public static class XModel
{
    /// <summary>
    /// Get <see langword="value"/> of given <b>propertyName</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    private static TPublic Get<TPublic, TPrivate>(IPropertySet i, TPublic defaultValue, bool serialize, string propertyName, Func<TPublic, TPrivate> convertTo, Func<TPrivate, TPublic> convertBack)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(propertyName, nameof(propertyName));

        static bool isNonSerializable<T>() => typeof(T).IsEnum && !typeof(T).HasAttribute<SerializableAttribute>();

        var properties = serialize ? i.SerializedProperties : i.NonSerializedProperties;

        var keyExists = properties.TryGetValue(propertyName, out var result);

        /// Check if property has ever been set (key won't exist if it hasn't)
        if (!keyExists)
        {
            result = convertTo(defaultValue);
            /// Automatically convert nonserializable enum to string
            result = serialize && isNonSerializable<TPublic>()
                ? $"{result}" : result;

            /// Add default value
            properties.Add(propertyName, result);
        }

        /// Automatically convert string back to nonserializable enum
        if (serialize && isNonSerializable<TPublic>())
            result = Try.Get(() => (TPrivate)Enum.Parse(typeof(TPrivate), $"{result}"));

        var resultFinal = convertBack((TPrivate)result);

        var e = new PropertyGetEventData(propertyName, resultFinal);
        i.If<IPropertyGet>(j => j.OnGetProperty(e));

        return (TPublic)e.Value;
    }

    /// <summary>
    /// Get <see langword="value"/> of given <b>propertyName</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(this IPropertySet i, T defaultValue = default, bool serialize = true, [CallerMemberName] string propertyName = "")
        => Get(i, defaultValue, serialize, propertyName, i => i, i => i);

    /// <summary>
    /// Get <see langword="value"/> of given <b>propertyName</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TPublic Get<TPublic, TPrivate>(this IPropertySet i, TPublic defaultValue, IConvert<TPublic, TPrivate> convert, bool serialize = true, [CallerMemberName] string propertyName = "")
        => Get(i, defaultValue, serialize, propertyName, convert.Convert, convert.ConvertBack);

    /// <summary>
    /// Reset given <b>propertyNames</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void Reset<T>(this IPropertySet i, params Expression<Func<T>>[] propertyNames)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(propertyNames, nameof(propertyNames));
        propertyNames?.ForEach(j =>
        {
            if (j.Body is MemberExpression body)
                i.Reset(body.Member.Name);
        });
    }

    /// <summary>
    /// Reset given <b>propertyName</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void Reset(this IPropertySet i, string propertyName)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(propertyName, nameof(propertyName));
        i.OnSetProperty(new(propertyName, null, null));
    }

    /// <summary>
    /// Set <see langword="value"/> of given <b>propertyName</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    private static bool Set<TPublic, TPrivate>(IPropertySet i, TPublic newValue, bool serialize, bool handle, string propertyName, Func<TPublic, TPrivate> convertTo, Func<TPrivate, TPublic> convertBack)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(propertyName, nameof(propertyName));

        var properties = serialize ? i.SerializedProperties : i.NonSerializedProperties;

        var keyExists = properties.TryGetValue(propertyName, out var result);

        /// Check if property has ever been set (key won't exist if it hasn't)
        if (!keyExists)
        {
            /// Get a default value
            result = convertTo(default);
            properties.Add(propertyName, result);
        }

        /// Convert it to the relevant form
        var oldValue = convertBack((TPrivate)result);
        if (EqualityComparer<TPublic>.Default.Equals(oldValue, newValue))
            return false;

        /// Notify clients before setting
        var e = new PropertySettingEventArgs(propertyName, oldValue, newValue);
        if (!handle)
            i.OnSettingProperty(e);

        /// Check for cancellation
        if (!e.Cancel)
        {
            /// Proceed and set the value
            newValue = (TPublic)e.NewValue;
            properties[propertyName] = convertTo(newValue);

            /// Mark as changed
            if (!handle)
            {
                if (i is IChange x && propertyName != nameof(x.IsChanged))
                    x.IsChanged = true;

                i.OnSetProperty(new(propertyName, oldValue, newValue));
            }

            return true;
        }
        return false;
    }

    /// <summary>
    /// Set <see langword="value"/> of given <b>propertyName</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Set<T>(this IPropertySet i, T newValue, bool serialize = true, bool handle = false, [CallerMemberName] string propertyName = "")
        => Set(i, newValue, serialize, handle, propertyName, i => i, i => i);

    /// <summary>
    /// Set <see langword="value"/> of given <b>propertyName</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Set<T>(this IPropertySet i, Expression<Func<T>> propertyName, T newValue, bool handle = false, bool serialize = true)
        => i.Set(newValue, serialize, handle, propertyName.Body.As<MemberExpression>().Member.Name);

    /// <summary>
    /// Set <see langword="value"/> of given <b>propertyName</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Set<TPublic, TPrivate>(this IPropertySet i, TPublic newValue, IConvert<TPublic, TPrivate> convert, bool serialize = true, bool handle = false, [CallerMemberName] string propertyName = "")
        => Set(i, newValue, serialize, handle, propertyName, convert.Convert, convert.ConvertBack);
}
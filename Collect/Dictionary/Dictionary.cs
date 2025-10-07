using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// Extends <see cref="IDictionary"/>.
/// </summary>
[Extend<IDictionary>]
public static class XDictionary
{
    /// <summary>
    /// Get first (or default) entry of <see cref="{Return}"/> that satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Return FirstOrDefault<Key, Value, Return>(this Dictionary<Key, Value> i, Condition<KeyValuePair<Key, Value>> where = null)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var j in i)
        {
            if (j.Value is Return result)
            {
                if (where is null || where.Invoke(j))
                    return result;
            }
        }
        return default;
    }

    /// <summary>
    /// Get entry with given <b>key</b> and <b>value</b> (or add if <b>key</b> doesn't exist).
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static object GetOrAdd(this IDictionary i, object key, Func<object> defaultValue = null)
    {
        Throw.IfNull(i, nameof(i));
        if (key != null)
        {
            if (!i.Contains(key))
                i.Add(key, defaultValue?.Invoke());

            return i[key];
        }
        return null;
    }

    /// <summary>
    /// Set entry with given <b>key</b> and <b>value</b> (or add if <b>key</b> doesn't exist).
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void SetOrAdd<Value>(this IDictionary i, object key, Value value)
    {
        Throw.IfNull(i, nameof(i));
        if (key != null)
        {
            if (!i.Contains(key))
                i.Add(key, value);

            else i[key] = value;
        }
    }
}
using System;

namespace Ion;

/// <summary>
/// Extends <see cref="IEquatable{T}"/>.
/// </summary>
/// <remarks>
/// <para><b>Implementing <see cref="IEquatable{T}"/></b></para>
/// <para><see langword="public static bool operator"/> !=(<see href="T"/> a, <see href="T"/> b) => !(a == b);</para>
/// <para><see langword="public static bool operator"/> ==(<see href="T"/> a, <see href="T"/> b) => <see cref="IEqual.CheckOperator(object, object)"/>;</para>
/// <para><see langword="public override bool"/> Equals(<see cref="object"/> b) => Equals(b as <see href="T"/>);</para>
/// <para><see langword="public bool"/> Equals(<see href="T"/> b) => <see cref="IEqual.Check(object, object)"/> <see langword="and"/> ...</para>
/// <para><see langword="public override int"/> <see cref="object.GetHashCode"/> => <see langword="default"/>;</para>
/// </remarks>
[Extend(typeof(IEquatable<>))]
public static class XEquatable
{
    /// <summary>
    /// Call in <see cref="object.Equals(object?)"/>.
    /// </summary>
    public static bool Check(object a, object b)
    {
        if (b is null)
            return false;

        if (ReferenceEquals(a, b))
            return true;

        if (a.GetType() != b.GetType())
            return false;

        return true;
    }

    /// <summary>
    /// Call in <see langword="operator"/> ==.
    /// </summary>
    public static bool CheckOperator(object a, object b)
    {
        if (a is null)
        {
            //null is null = true
            if (b is null)
                return true;

            //Only the left side is null
            return false;
        }
        return a.Equals(b);
    }
}
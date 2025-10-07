using System;
using System.Diagnostics.CodeAnalysis;

namespace Ion;

/// <summary>
/// A <see langword="class"/> or <see langword="struct"/> that references one or more others.
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
[SuppressMessage("Performance", "CA1813:Avoid unsealed attributes")]
public class UsingAttribute(params Type[] Types) : Attribute()
{
    public readonly Type[] Types = Types;
}

public sealed class UsingAttribute<T>() : UsingAttribute(typeof(T));

public sealed class UsingAttribute<T1, T2>() : UsingAttribute(typeof(T1), typeof(T2));

public sealed class UsingAttribute<T1, T2, T3>() : UsingAttribute(typeof(T1), typeof(T2), typeof(T3));

public sealed class UsingAttribute<T1, T2, T3, T4>() : UsingAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

public sealed class UsingAttribute<T1, T2, T3, T4, T5>() : UsingAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

public sealed class UsingAttribute<T1, T2, T3, T4, T5, T6>() : UsingAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));

public sealed class UsingAttribute<T1, T2, T3, T4, T5, T6, T7>() : UsingAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));

public sealed class UsingAttribute<T1, T2, T3, T4, T5, T6, T7, T8>() : UsingAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
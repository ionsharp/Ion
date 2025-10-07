using System;

namespace Ion;

/// <summary>
/// An <see cref="object"/> that can convert from one <see cref="Type"/> to another and back.
/// </summary>
public interface IConvert
{
    Type SourceType { get; }

    Type TargetType { get; }
}

/// <summary>
/// An <see cref="object"/> that can convert between <see cref="object"/> and <see cref="{B}"/>.
/// </summary>
public interface IConvert<B> : IConvert
{
    new Type TargetType => typeof(B);

    B ConvertTo();

    object ConvertBack(B b);
}

/// <summary>
/// An <see cref="object"/> that can convert between <see cref="{A}"/> and <see cref="{B}"/>.
/// </summary>
public interface IConvert<A, B> : IConvert<B>
{
    new Type SourceType => typeof(A);

    new Type TargetType => typeof(B);

    B Convert(A a);

    new A ConvertBack(B b);
}

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
public abstract class ConvertAttribute(Type from, Type to) : Attribute()
{
    public Type From { get; set; } = from;

    public Type To { get; set; } = to;

    protected ConvertAttribute() : this(default, default) { }
}

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
public sealed class ConvertAttribute<To, From>() : ConvertAttribute(typeof(From), typeof(To)) { }
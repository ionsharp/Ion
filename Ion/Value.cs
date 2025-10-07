using System;

namespace Ion;

/// <see cref="Value"/>      = Immutable
/// <see cref="ValueTuple"/> = Mutable

/// <summary>
/// A <see cref="ValueTuple"/> that cannot be changed.
/// </summary>
public interface IValueTupleImmutable : IImmutable;

/// <inheritdoc cref="IValueTupleImmutable"/>
/// <remarks><b>≡ <see cref="ValueTuple{T1, T2}"/></b></remarks>
public readonly record struct Value<T1, T2>(T1 A, T2 B) : IValueTupleImmutable
{
    public readonly T1 A { get; } = A;

    public readonly T2 B { get; } = B;

    public static implicit operator (T1 A, T2 B)(Value<T1, T2> i) => (i.A, i.B);

    public static implicit operator Value<T1, T2>((T1 A, T2 B) i) => new(i.A, i.B);

    public static implicit operator TupleMutable<T1, T2>(Value<T1, T2> i) => new(i.A, i.B);

    public static implicit operator Value<T1, T2>(TupleMutable<T1, T2> i) => new(i.A, i.B);

    public static implicit operator Tuple<T1, T2>(Value<T1, T2> i) => new(i.A, i.B);

    public static implicit operator Value<T1, T2>(Tuple<T1, T2> i) => new(i.Item1, i.Item2);
}

/// <inheritdoc cref="IValueTupleImmutable"/>
/// <remarks><b>≡ <see cref="ValueTuple{T1, T2, T3}"/></b></remarks>
public readonly record struct Value<T1, T2, T3>(T1 A, T2 B, T3 C) : IValueTupleImmutable
{
    public readonly T1 A { get; } = A;

    public readonly T2 B { get; } = B;

    public readonly T3 C { get; } = C;

    public static implicit operator (T1 A, T2 B, T3 C)(Value<T1, T2, T3> i) => (i.A, i.B, i.C);

    public static implicit operator Value<T1, T2, T3>((T1 A, T2 B, T3 C) i) => new(i.A, i.B, i.C);

    public static implicit operator TupleMutable<T1, T2, T3>(Value<T1, T2, T3> i) => new(i.A, i.B, i.C);

    public static implicit operator Value<T1, T2, T3>(TupleMutable<T1, T2, T3> i) => new(i.A, i.B, i.C);

    public static implicit operator Tuple<T1, T2, T3>(Value<T1, T2, T3> i) => new(i.A, i.B, i.C);

    public static implicit operator Value<T1, T2, T3>(Tuple<T1, T2, T3> i) => new(i.Item1, i.Item2, i.Item3);
}

/// <inheritdoc cref="IValueTupleImmutable"/>
/// <remarks><b>≡ <see cref="ValueTuple{T1, T2, T3, T4}"/></b></remarks>
public readonly record struct Value<T1, T2, T3, T4>(T1 A, T2 B, T3 C, T4 D) : IValueTupleImmutable
{
    public readonly T1 A { get; } = A;

    public readonly T2 B { get; } = B;

    public readonly T3 C { get; } = C;

    public readonly T4 D { get; } = D;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D)(Value<T1, T2, T3, T4> i) => (i.A, i.B, i.C, i.D);

    public static implicit operator Value<T1, T2, T3, T4>((T1 A, T2 B, T3 C, T4 D) i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator TupleMutable<T1, T2, T3, T4>(Value<T1, T2, T3, T4> i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator Value<T1, T2, T3, T4>(TupleMutable<T1, T2, T3, T4> i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator Tuple<T1, T2, T3, T4>(Value<T1, T2, T3, T4> i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator Value<T1, T2, T3, T4>(Tuple<T1, T2, T3, T4> i) => new(i.Item1, i.Item2, i.Item3, i.Item4);
}

/// <inheritdoc cref="IValueTupleImmutable"/>
/// <remarks><b>≡ <see cref="ValueTuple{T1, T2, T3, T4, T5}"/></b></remarks>
public readonly record struct Value<T1, T2, T3, T4, T5>(T1 A, T2 B, T3 C, T4 D, T5 E) : IValueTupleImmutable
{
    public readonly T1 A { get; } = A;

    public readonly T2 B { get; } = B;

    public readonly T3 C { get; } = C;

    public readonly T4 D { get; } = D;

    public readonly T5 E { get; } = E;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D, T5 E)(Value<T1, T2, T3, T4, T5> i) => (i.A, i.B, i.C, i.D, i.E);

    public static implicit operator Value<T1, T2, T3, T4, T5>((T1 A, T2 B, T3 C, T4 D, T5 E) i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5>(Value<T1, T2, T3, T4, T5> i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator Value<T1, T2, T3, T4, T5>(TupleMutable<T1, T2, T3, T4, T5> i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator Tuple<T1, T2, T3, T4, T5>(Value<T1, T2, T3, T4, T5> i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator Value<T1, T2, T3, T4, T5>(Tuple<T1, T2, T3, T4, T5> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5);
}

/// <inheritdoc cref="IValueTupleImmutable"/>
/// <remarks><b>≡ <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/></b></remarks>
public readonly record struct Value<T1, T2, T3, T4, T5, T6>(T1 A, T2 B, T3 C, T4 D, T5 E, T6 F) : IValueTupleImmutable
{
    public readonly T1 A { get; } = A;

    public readonly T2 B { get; } = B;

    public readonly T3 C { get; } = C;

    public readonly T4 D { get; } = D;

    public readonly T5 E { get; } = E;

    public readonly T6 F { get; } = F;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D, T5 E, T6 F)(Value<T1, T2, T3, T4, T5, T6> i) => (i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6>((T1 A, T2 B, T3 C, T4 D, T5 E, T6 F) i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6>(Value<T1, T2, T3, T4, T5, T6> i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6>(TupleMutable<T1, T2, T3, T4, T5, T6> i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator Tuple<T1, T2, T3, T4, T5, T6>(Value<T1, T2, T3, T4, T5, T6> i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6>(Tuple<T1, T2, T3, T4, T5, T6> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6);
}

/// <inheritdoc cref="IValueTupleImmutable"/>
/// <remarks><b>≡ <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/></b></remarks>
public readonly record struct Value<T1, T2, T3, T4, T5, T6, T7>(T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G) : IValueTupleImmutable
{
    public readonly T1 A { get; } = A;

    public readonly T2 B { get; } = B;

    public readonly T3 C { get; } = C;

    public readonly T4 D { get; } = D;

    public readonly T5 E { get; } = E;

    public readonly T6 F { get; } = F;

    public readonly T7 G { get; } = G;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G)(Value<T1, T2, T3, T4, T5, T6, T7> i) => (i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6, T7>((T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G) i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6, T7>(Value<T1, T2, T3, T4, T5, T6, T7> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6, T7>(TupleMutable<T1, T2, T3, T4, T5, T6, T7> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator Tuple<T1, T2, T3, T4, T5, T6, T7>(Value<T1, T2, T3, T4, T5, T6, T7> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6, T7>(Tuple<T1, T2, T3, T4, T5, T6, T7> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6, i.Item7);
}

/// <inheritdoc cref="IValueTupleImmutable"/>
/// <remarks><b>≡ <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></b></remarks>
public readonly record struct Value<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G, TRest Rest) : IValueTupleImmutable
{
    public readonly T1 A { get; } = A;

    public readonly T2 B { get; } = B;

    public readonly T3 C { get; } = C;

    public readonly T4 D { get; } = D;

    public readonly T5 E { get; } = E;

    public readonly T6 F { get; } = F;

    public readonly T7 G { get; } = G;

    public readonly TRest Rest { get; } = Rest;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G, TRest H)(Value<T1, T2, T3, T4, T5, T6, T7, TRest> i) => (i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6, T7, TRest>((T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G, TRest H) i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.H);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest>(Value<T1, T2, T3, T4, T5, T6, T7, TRest> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6, T7, TRest>(TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(Value<T1, T2, T3, T4, T5, T6, T7, TRest> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6, T7, TRest>(Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6, i.Item7, i.Rest);
}
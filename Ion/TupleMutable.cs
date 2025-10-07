using System;

namespace Ion;

/// <see cref="Tuple"/>        = Immutable
/// <see cref="TupleMutable"/> = Mutable

/// <summary>
/// A <see cref="Tuple"/> that can be changed.
/// </summary>
public static class TupleMutable;

/// <inheritdoc cref="TupleMutable"/>
/// <remarks><b>≡ <see cref="Tuple{T1, T2}"/></b></remarks>
public record class TupleMutable<T1, T2>(T1 A = default, T2 B = default) : IMutable
{
    public T1 A { get; set; } = A;

    public T2 B { get; set; } = B;

    public static implicit operator (T1 A, T2 B)(TupleMutable<T1, T2> i) => (i.A, i.B);

    public static implicit operator TupleMutable<T1, T2>((T1 A, T2 B) i) => new(i.A, i.B);

    public static implicit operator TupleMutable<T1, T2>(Value<T1, T2> i) => new(i.A, i.B);

    public static implicit operator Value<T1, T2>(TupleMutable<T1, T2> i) => new(i.A, i.B);

    public static implicit operator Tuple<T1, T2>(TupleMutable<T1, T2> i) => new(i.A, i.B);

    public static implicit operator TupleMutable<T1, T2>(Tuple<T1, T2> i) => new(i.Item1, i.Item2);
}

/// <inheritdoc cref="TupleMutable"/>
/// <remarks><b>≡ <see cref="Tuple{T1, T2, T3}"/></b></remarks>
public record class TupleMutable<T1, T2, T3>(T1 A = default, T2 B = default, T3 C = default) : TupleMutable<T1, T2>(A, B)
{
    public T3 C { get; set; } = C;

    public static implicit operator (T1 A, T2 B, T3 C)(TupleMutable<T1, T2, T3> i) => (i.A, i.B, i.C);

    public static implicit operator TupleMutable<T1, T2, T3>((T1 A, T2 B, T3 C) i) => new(i.A, i.B, i.C);

    public static implicit operator TupleMutable<T1, T2, T3>(Value<T1, T2, T3> i) => new(i.A, i.B, i.C);

    public static implicit operator Value<T1, T2, T3>(TupleMutable<T1, T2, T3> i) => new(i.A, i.B, i.C);

    public static implicit operator Tuple<T1, T2, T3>(TupleMutable<T1, T2, T3> i) => new(i.A, i.B, i.C);

    public static implicit operator TupleMutable<T1, T2, T3>(Tuple<T1, T2, T3> i) => new(i.Item1, i.Item2, i.Item3);
}

/// <inheritdoc cref="TupleMutable"/>
/// <remarks><b>≡ <see cref="Tuple{T1, T2, T3, T4}"/></b></remarks>
public record class TupleMutable<T1, T2, T3, T4>(T1 A = default, T2 B = default, T3 C = default, T4 D = default) : TupleMutable<T1, T2, T3>(A, B, C)
{
    public T4 D { get; set; } = D;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D)(TupleMutable<T1, T2, T3, T4> i) => (i.A, i.B, i.C, i.D);

    public static implicit operator TupleMutable<T1, T2, T3, T4>((T1 A, T2 B, T3 C, T4 D) i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator TupleMutable<T1, T2, T3, T4>(Value<T1, T2, T3, T4> i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator Value<T1, T2, T3, T4>(TupleMutable<T1, T2, T3, T4> i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator Tuple<T1, T2, T3, T4>(TupleMutable<T1, T2, T3, T4> i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator TupleMutable<T1, T2, T3, T4>(Tuple<T1, T2, T3, T4> i) => new(i.Item1, i.Item2, i.Item3, i.Item4);
}

/// <inheritdoc cref="TupleMutable"/>
/// <remarks><b>≡ <see cref="Tuple{T1, T2, T3, T4, T5}"/></b></remarks>
public record class TupleMutable<T1, T2, T3, T4, T5>(T1 A = default, T2 B = default, T3 C = default, T4 D = default, T5 E = default) : TupleMutable<T1, T2, T3, T4>(A, B, C, D)
{
    public T5 E { get; set; } = E;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D, T5 E)(TupleMutable<T1, T2, T3, T4, T5> i) => (i.A, i.B, i.C, i.D, i.E);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5>((T1 A, T2 B, T3 C, T4 D, T5 E) i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5>(Value<T1, T2, T3, T4, T5> i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator Value<T1, T2, T3, T4, T5>(TupleMutable<T1, T2, T3, T4, T5> i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator Tuple<T1, T2, T3, T4, T5>(TupleMutable<T1, T2, T3, T4, T5> i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5>(Tuple<T1, T2, T3, T4, T5> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5);
}

/// <inheritdoc cref="TupleMutable"/>
/// <remarks><b>≡ <see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></b></remarks>
public record class TupleMutable<T1, T2, T3, T4, T5, T6>(T1 A = default, T2 B = default, T3 C = default, T4 D = default, T5 E = default, T6 F = default) : TupleMutable<T1, T2, T3, T4, T5>(A, B, C, D, E)
{
    public T6 F { get; set; } = F;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D, T5 E, T6 F)(TupleMutable<T1, T2, T3, T4, T5, T6> i) => (i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6>((T1 A, T2 B, T3 C, T4 D, T5 E, T6 F) i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6>(Value<T1, T2, T3, T4, T5, T6> i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6>(TupleMutable<T1, T2, T3, T4, T5, T6> i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator Tuple<T1, T2, T3, T4, T5, T6>(TupleMutable<T1, T2, T3, T4, T5, T6> i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6>(Tuple<T1, T2, T3, T4, T5, T6> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6);
}

/// <inheritdoc cref="TupleMutable"/>
/// <remarks><b>≡ <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></b></remarks>
public record class TupleMutable<T1, T2, T3, T4, T5, T6, T7>(T1 A = default, T2 B = default, T3 C = default, T4 D = default, T5 E = default, T6 F = default, T7 G = default) : TupleMutable<T1, T2, T3, T4, T5, T6>(A, B, C, D, E, F)
{
    public T7 G { get; set; } = G;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G)(TupleMutable<T1, T2, T3, T4, T5, T6, T7> i) => (i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6, T7>((T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G) i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6, T7>(Value<T1, T2, T3, T4, T5, T6, T7> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6, T7>(TupleMutable<T1, T2, T3, T4, T5, T6, T7> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator Tuple<T1, T2, T3, T4, T5, T6, T7>(TupleMutable<T1, T2, T3, T4, T5, T6, T7> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6, T7>(Tuple<T1, T2, T3, T4, T5, T6, T7> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6, i.Item7);
}

/// <inheritdoc cref="TupleMutable"/>
/// <remarks><b>≡ <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></b></remarks>
public record class TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 A = default, T2 B = default, T3 C = default, T4 D = default, T5 E = default, T6 F = default, T7 G = default, TRest Rest = default) : TupleMutable<T1, T2, T3, T4, T5, T6, T7>(A, B, C, D, E, F, G)
{
    public TRest Rest { get; set; } = Rest;

    public static implicit operator (T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G, TRest H)(TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest> i) => (i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest>((T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G, TRest H) i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.H);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest>(Value<T1, T2, T3, T4, T5, T6, T7, TRest> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator Value<T1, T2, T3, T4, T5, T6, T7, TRest>(TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator TupleMutable<T1, T2, T3, T4, T5, T6, T7, TRest>(Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6, i.Item7, i.Rest);
}
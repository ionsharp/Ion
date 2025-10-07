using System;

namespace Ion.Input;

/// <inheritdoc/>
public class EventArgs<T1>(T1 Value, object Parameter = null) : EventArgs
{
    public T1 A { get; } = Value;

    public object Parameter { get; } = Parameter;
}

/// <inheritdoc/>
public class EventArgs<T1, T2>(T1 A, T2 B, object Parameter = null) : EventArgs<T1>(A, Parameter)
{
    public T2 B { get; } = B;
}

/// <inheritdoc/>
public class EventArgs<T1, T2, T3>(T1 A, T2 B, T3 C, object Parameter = null) : EventArgs<T1, T2>(A, B, Parameter)
{
    public T3 C { get; } = C;
}

/// <inheritdoc/>
public class EventArgs<T1, T2, T3, T4>(T1 A, T2 B, T3 C, T4 D, object Parameter = null) : EventArgs<T1, T2, T3>(A, B, C, Parameter)
{
    public T4 D { get; } = D;
}

/// <inheritdoc/>
public class EventArgs<T1, T2, T3, T4, T5>(T1 A, T2 B, T3 C, T4 D, T5 E, object Parameter = null) : EventArgs<T1, T2, T3, T4>(A, B, C, D, Parameter)
{
    public T5 E { get; } = E;
}

/// <inheritdoc/>
public class EventArgs<T1, T2, T3, T4, T5, T6>(T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, object Parameter = null) : EventArgs<T1, T2, T3, T4, T5>(A, B, C, D, E, Parameter)
{
    public T6 F { get; } = F;
}

/// <inheritdoc/>
public class EventArgs<T1, T2, T3, T4, T5, T6, T7>(T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G, object Parameter = null) : EventArgs<T1, T2, T3, T4, T5, T6>(A, B, C, D, E, F, Parameter)
{
    public T7 G { get; } = G;
}

/// <inheritdoc/>
public class EventArgs<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 A, T2 B, T3 C, T4 D, T5 E, T6 F, T7 G, TRest Rest, object Parameter = null) : EventArgs<T1, T2, T3, T4, T5, T6, T7>(A, B, C, D, E, F, G, Parameter)
{
    public TRest Rest { get; } = Rest;
}
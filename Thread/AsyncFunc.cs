using System;
using System.Threading.Tasks;

namespace Ion.Threading;

/// <summary>
/// A <see cref="Func"/> that returns a <see cref="Task"/> (with a <see cref="{Result}"/>).
/// </summary>
/// <remarks><b>≡ <see cref="Func{Result}"/>.</b></remarks>
public delegate Task<Result> FuncAsync<Result>();

/// <remarks><b>≡ <see cref="Func{T1, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T, Result>(T a);

/// <remarks><b>≡ <see cref="Func{T1, T2, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, Result>(T1 a, T2 b);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, Result>(T1 a, T2 b, T3 c);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, Result>(T1 a, T2 b, T3 c, T4 d);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, Result>(T1 a, T2 b, T3 c, T4 d, T5 e);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l, T13 m);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l, T13 m, T14 n);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l, T13 m, T14 n, T15 o);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Result}"/>.</b></remarks>
/// <inheritdoc cref="FuncAsync{Result}"/>
public delegate Task<Result> FuncAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, Result>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l, T13 m, T14 n, T15 o, T16 p);
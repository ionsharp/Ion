using System;
using System.Threading.Tasks;

namespace Ion.Threading;

/// <summary>
/// A <see cref="Func"/> that returns a <see cref="Task"/>.
/// </summary>
/// <remarks><b>≡ <see cref="Func{Result}"/></b></remarks>
public delegate Task VoidAsync();

/// <summary>
/// A <see cref="Func"/> that takes input and returns a <see cref="Task"/>.
/// </summary>
/// <remarks><b>≡ <see cref="Func{T1, Task}"/></b></remarks>
public delegate Task VoidAsync<in T>(T i);

/// <remarks><b>≡ <see cref="Func{T1, T2, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2>(T1 a, T2 b);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3>(T1 a, T2 b, T3 c);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4>(T1 a, T2 b, T3 c, T4 d);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5>(T1 a, T2 b, T3 c, T4 d, T5 e);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l, T13 m);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l, T13 m, T14 n);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l, T13 m, T14 n, T15 o);

/// <remarks><b>≡ <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task}"/></b></remarks>
/// <inheritdoc cref="VoidAsync{T}"/>
public delegate Task VoidAsync<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j, T11 k, T12 l, T13 m, T14 n, T15 o, T16 p);
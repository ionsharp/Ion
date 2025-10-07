using System;

namespace Ion.Numeral;

/// <see cref="IVector"/>

/// <summary>
/// Extends <see cref="IVector"/>.
/// </summary>
[Extend<IVector>]
public static partial class XVector
{
    /// <see cref="return"/> = <see cref="Boolean"/>
    #region

    /// <summary>
    /// Get if contains given <see cref="Array1D"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Contains(this IVector i, object[] j) => i.IndexOf(j) != -1;

    /// <summary>
    /// Get if contains given <see cref="IVector"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Contains(this IVector i, IVector j) => i.IndexOf(j) != -1;

    #endregion

    /// <see cref="return"/> = <see cref="Int32"/>
    #region

    /// <summary>
    /// Get index of given <see cref="object"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotImplemented]
    public static int IndexOf(this IVector i, object[] j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return -1;
    }

    /// <summary>
    /// Get index of given <see cref="IVector"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotImplemented]
    public static int IndexOf(this IVector i, IVector j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return -1;
    }

    #endregion

    /// <see cref="return"/> = <see cref="IVector{T}"/>
    #region

    /// <inheritdoc cref="RotateLeft{T}(IVector{T})"/>
    public static Vector<object> RotateLeft(this IVector i)
        => i.NewType(j => j).RotateLeft();

    /// <inheritdoc cref="RotateLeft{T}(IVector{T}, int)"/>
    public static Vector<object> RotateLeft(this IVector i, int times)
        => i.NewType(j => j).RotateLeft(times);

    /// <inheritdoc cref="RotateRight{T}(IVector{T})"/>
    public static Vector<object> RotateRight(this IVector i)
        => i.NewType(j => j).RotateRight();

    /// <inheritdoc cref="RotateRight{T}(IVector{T}, int)"/>
    public static Vector<object> RotateRight(this IVector i, int times)
        => i.NewType(j => j).RotateRight(times);

    /// <inheritdoc cref="Subvector{T}(IVector{T}, int, int)"/>
    public static Vector<object> Subvector(this IVector i, int index, int length = 0)
        => i.NewType(j => j).Subvector(index, length);

    /// <inheritdoc cref="Transpose{T}(IVector{T})"/>
    public static Vector<object> Transpose(this IVector i)
        => i.NewType(j => j).Transpose();

    #endregion

    /// <see cref="return"/> = <see cref="IVector{TNew}"/>
    #region

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static Vector<object> New(this IVector i)
        => i.NewType(j => j);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{TValue, TValue})"/>
    public static Vector<object> New(this IVector i, Func<object, object> select)
        => i.NewType(j => j).New(select);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{int, TValue, TValue})"/>
    public static Vector<object> New(this IVector i, Func<int, object, object> select)
        => i.NewType(j => j).New(select);

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="IVector"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<TNew> NewType<TNew>(this IVector i, Func<object, TNew> select)
        => i.NewType((_, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="IVector"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<TNew> NewType<TNew>(this IVector i, Func<int, object, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return new(Array1D.Get(i.Length, j => select(j, i[j])), i.Type);
    }

    #endregion
}

[Extend(typeof(IVector2))]
public static partial class XVector
{
    /// <see cref="return"/> = <see cref="IVector2{TNew}"/>
    #region

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TNew> NewType<TNew>(this IVector2 i, Func<object, TNew> select)
        => i.NewType((_, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TNew> NewType<TNew>(this IVector2 i, Func<int, object, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return new(select(0, i[0]), select(1, i[1]), i.Type);
    }

    #endregion
}

[Extend(typeof(IVector3))]
public static partial class XVector
{
    /// <see cref="return"/> = <see cref="IVector3{TNew}"/>
    #region

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TNew> NewType<TNew>(this IVector3 i, Func<object, TNew> select)
        => i.NewType((_, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TNew> NewType<TNew>(this IVector3 i, Func<int, object, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return new(select(0, i[0]), select(1, i[1]), select(2, i[2]), i.Type);
    }

    #endregion
}

[Extend(typeof(IVector4))]
public static partial class XVector
{
    /// <see cref="return"/> = <see cref="IVector4{TNew}"/>
    #region

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TNew> NewType<TNew>(this IVector4 i, Func<object, TNew> select)
        => i.NewType((_, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TNew> NewType<TNew>(this IVector4 i, Func<int, object, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return new(select(0, i[0]), select(1, i[1]), select(2, i[2]), select(3, i[3]), i.Type);
    }

    #endregion
}

[Extend(typeof(IVector<>))]
public static partial class XVector
{
    /// <see cref="return"/> = <see cref="IVector{T}"/>
    #region

    /// <summary>
    /// Get new instance rotated in left direction.
    /// </summary>
    /// <remarks>
    /// <para><b><see cref="VectorType.X"/> ⇒ <see cref="VectorType.Y"/></b></para>
    /// [1]<br/>
    /// [2] ⇒ [1, 2, 3]<br/>
    /// [3]
    /// <para><b><see cref="VectorType.Y"/> ⇒ <see cref="VectorType.X"/></b></para>
    /// [1, 2, 3]<br/>
    /// ⇓<br/>
    /// [3]<br/>
    /// [2]<br/>
    /// [1]
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<T> RotateLeft<T>(this IVector<T> i)
    {
        Throw.IfNull(i, nameof(i));
        return i.Type switch { VectorType.X => i.Transpose(), VectorType.Y => i.Transpose().Flip() };
    }

    /// <summary>
    /// Get new instance rotated in left direction given <b>times</b>.
    /// </summary>
    /// <inheritdoc cref="RotateLeft{T}(IVector{T})"/>
    public static Vector<T> RotateLeft<T>(this IVector<T> i, int times)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(times, 1);

        var result = i.RotateLeft();
        Array1D.Do(times, j => result = result.RotateLeft());
        return result;
    }

    /// <summary>
    /// Get new instance rotated in right direction.
    /// </summary>
    /// <remarks>
    /// <para><b><see cref="VectorType.X"/> ⇒ <see cref="VectorType.Y"/></b></para>
    /// [1]<br/>
    /// [2] ⇒ [3, 2, 1]<br/>
    /// [3]
    /// <para><b><see cref="VectorType.Y"/> ⇒ <see cref="VectorType.X"/></b></para>
    /// [1, 2, 3]<br/>
    /// ⇓<br/>
    /// [1]<br/>
    /// [2]<br/>
    /// [3]
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<T> RotateRight<T>(this IVector<T> i)
    {
        Throw.IfNull(i, nameof(i));
        return i.Type switch { VectorType.X => i.Transpose().Flip(), VectorType.Y => i.Transpose() };
    }

    /// <summary>
    /// Get new instance rotated in right direction given <b>times</b>.
    /// </summary>
    /// <inheritdoc cref="RotateRight{T}(IVector{T})"/>
    public static Vector<T> RotateRight<T>(this IVector<T> i, int times)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(times, 1);

        var result = i.RotateRight();
        Array1D.Do(times, j => result = result.RotateRight());
        return result;
    }

    /// <summary>
    /// Get subvector at given <b>index</b> with given <b>length</b> (if 0, <b>index</b> to <see cref="IVector.Length"/>).
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsGreaterOrEqual"/>
    /// <exception cref="ArgumentIsNegative"/>
    [NotTested]
    public static Vector<T> Subvector<T>(this IVector<T> i, int index, int length = 0)
    {
        Throw.IfNull(i, nameof(i));

        Throw.IfNegative(index, nameof(index));
        Throw.IfGreaterOrEqual(index, i.Length, nameof(index));

        Throw.IfNegative(length, nameof(length));
        length = length + index >= i.Length ? i.Length - index : length;

        return new(i.Type, i.Where((j, k) => j >= index && j < index + length));
    }

    /// <summary>
    /// Get new instance with inverted <see cref="IVector.Type"/>.
    /// </summary>
    /// <remarks>
    /// <para><b><see cref="VectorType.X"/> ⇒ <see cref="VectorType.Y"/></b></para>
    /// [1]<br/>
    /// [2] ⇒ [1, 2, 3]<br/>
    /// [3]<br/>
    /// <para><b><see cref="VectorType.Y"/> ⇒ <see cref="VectorType.X"/></b></para>
    /// [1, 2, 3]<br/>
    /// ⇓<br/>
    /// [1]<br/>
    /// [2]<br/>
    /// [3]<br/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<T> Transpose<T>(this IVector<T> i)
    {
        Throw.IfNull(i, nameof(i));
        return new(IVector.Invert(i.Type), i);
    }

    #endregion

    /// <see cref="return"/> = <see cref="IVector{TNew}"/>
    #region

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static Vector<T> New<T>(this IVector<T> i)
        => i.NewType(j => j);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{TValue, TValue})"/>
    public static Vector<T> New<T>(this IVector<T> i, Func<T, T> select)
        => i.NewType(j => j).New(select);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{int, TValue, TValue})"/>
    public static Vector<T> New<T>(this IVector<T> i, Func<int, T, T> select)
        => i.NewType(j => j).New(select);

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<TNew> NewType<TOld, TNew>(this IVector<TOld> i, Func<TOld, TNew> select)
        => i.NewType((_, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<TNew> NewType<TOld, TNew>(this IVector<TOld> i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return new(Array1D.Get(i.Length, j => select(j, i[j])), i.Type);
    }


    #endregion
}

[Extend(typeof(IVector2<>))]
public static partial class XVector
{
    /// <see cref="return"/> = <see cref="IVector2{TNew}"/>
    #region

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TNew> NewType<TOld, TNew>(this IVector2<TOld> i, Func<TOld, TNew> select)
        => i.NewType((_, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TNew> NewType<TOld, TNew>(this IVector2<TOld> i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return new(select(0, i[0]), select(1, i[1]), i.Type);
    }

    #endregion
}

[Extend(typeof(IVector3<>))]
public static partial class XVector
{
    /// <see cref="return"/> = <see cref="IVector3{TNew}"/>
    #region

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TNew> NewType<TOld, TNew>(this IVector3<TOld> i, Func<TOld, TNew> select)
        => i.NewType((_, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TNew> NewType<TOld, TNew>(this IVector3<TOld> i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return new(select(0, i[0]), select(1, i[1]), select(2, i[2]), i.Type);
    }

    #endregion
}

[Extend(typeof(IVector4<>))]
public static partial class XVector
{
    /// <see cref="return"/> = <see cref="IVector4{TNew}"/>
    #region

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TNew> NewType<TOld, TNew>(this IVector4<TOld> i, Func<TOld, TNew> select)
        => i.NewType((_, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="TNew"/> from instance of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TNew> NewType<TOld, TNew>(this IVector4<TOld> i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return new(select(0, i[0]), select(1, i[1]), select(2, i[2]), select(3, i[3]), i.Type);
    }

    #endregion
}

[Extend(typeof(IVector4<byte>))] 
public static partial class XVector
{
}

[Extend(typeof(IVector<,>))]
public static partial class XVector
{
    private static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Func<IVector<TValue>, Vector<TValue>> action)
        where TSelf : IVector<TSelf, TValue>
    {
        Throw.IfNull(i, nameof(i));
        var result = action(i).ToArray();

        Throw.If<VectorNotImplemented>(result is null, nameof(i));
        return TSelf.Create((TSelf)i, result);
    }

    /// <see cref="return"/> = <see cref="IVector{T}"/>
    #region

    /// <inheritdoc cref="RotateLeft{T}(IVector{T})"/>
    public static TSelf RotateLeft<TSelf, TValue>(this IVector<TSelf, TValue> i)
        where TSelf : IVector<TSelf, TValue> => i.Do(j => j.RotateLeft());

    /// <inheritdoc cref="RotateLeft{T}(IVector{T}, int)"/>
    public static TSelf RotateLeft<TSelf, TValue>(this IVector<TSelf, TValue> i, int times)
        where TSelf : IVector<TSelf, TValue> => i.Do(j => j.RotateLeft(times));

    /// <inheritdoc cref="RotateRight{T}(IVector{T})"/>
    public static TSelf RotateRight<TSelf, TValue>(this IVector<TSelf, TValue> i)
        where TSelf : IVector<TSelf, TValue> => i.Do(j => j.RotateRight());

    /// <inheritdoc cref="RotateRight{T}(IVector{T}, int)"/>
    public static TSelf RotateRight<TSelf, TValue>(this IVector<TSelf, TValue> i, int times)
        where TSelf : IVector<TSelf, TValue> => i.Do(j => j.RotateRight(times));

    /// <inheritdoc cref="Subvector{T}(IVector{T}, int, int)"/>
    public static TSelf Subvector<TSelf, TValue>(this IVector<TSelf, TValue> i, int index, int length = 0)
        where TSelf : IVector<TSelf, TValue> => i.Do(j => j.Subvector(index, length));

    /// <inheritdoc cref="Transpose{T}(IVector{T})"/>
    public static TSelf Transpose<TSelf, TValue>(this IVector<TSelf, TValue> i)
        where TSelf : IVector<TSelf, TValue> => i.Do(j => j.Transpose());

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{TNew}"/>
    #region

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IVector<TSelf, TValue> i)
        where TSelf : IVector<TSelf, TValue> => (i as IArray<TSelf, TValue>).New();

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{TValue, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IVector<TSelf, TValue> i, Func<TValue, TValue> select)
        where TSelf : IVector<TSelf, TValue> => (i as IArray<TSelf, TValue>).New(select);

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{int, TValue, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IVector<TSelf, TValue> i, Func<int, TValue, TValue> select)
        where TSelf : IVector<TSelf, TValue> => (i as IArray<TSelf, TValue>).New(select);

    #endregion
}

[Extend(typeof(IVector<,>))]
[Extend(typeof(IVector2<>), typeof(IVector3<>), typeof(IVector4<>))]
public static partial class XVector
{
    /// <summary>
    /// Get new instance with given <b>w</b>.
    /// </summary>
    public static TSelf W<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue w)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> => i.New((j, k) => j == 3 ? w : k);

    /// <summary>
    /// Get new instance with given <b>w</b>.
    /// </summary>
    public static TSelf W<TSelf, TValue>(this IVector<TSelf, TValue> i, Func<TValue, TValue> w)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> => i.New((j, k) => j == 3 ? w(k) : k);

    /// <summary>
    /// Get new instance with given <b>x</b>.
    /// </summary>
    public static TSelf X<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue x)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> => i.New((j, k) => j == 0 ? x : k);

    /// <summary>
    /// Get new instance with given <b>x</b>.
    /// </summary>
    public static TSelf X<TSelf, TValue>(this IVector<TSelf, TValue> i, Func<TValue, TValue> x)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> => i.New((j, k) => j == 0 ? x(k) : k);

    /// <summary>
    /// Get new instance with given <b>y</b>.
    /// </summary>
    public static TSelf Y<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue y)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> => i.New((j, k) => j == 1 ? y : k);

    /// <summary>
    /// Get new instance with given <b>y</b>.
    /// </summary>
    public static TSelf Y<TSelf, TValue>(this IVector<TSelf, TValue> i, Func<TValue, TValue> y)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> => i.New((j, k) => j == 1 ? y(k) : k);

    /// <summary>
    /// Get new instance with given <b>z</b>.
    /// </summary>
    public static TSelf Z<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue z)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> => i.New((j, k) => j == 2 ? z : k);

    /// <summary>
    /// Get new instance with given <b>z</b>.
    /// </summary>
    public static TSelf Z<TSelf, TValue>(this IVector<TSelf, TValue> i, Func<TValue, TValue> z)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> => i.New((j, k) => j == 2 ? z(k) : k);
}
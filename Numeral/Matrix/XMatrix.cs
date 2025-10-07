using System;
using System.Collections.Generic;
using System.Numerics;

namespace Ion.Numeral;

/// <see cref="IMatrix"/>

/// <summary>
/// Extends <see cref="IMatrix"/>.
/// </summary>
[Extend<IMatrix>]
public static partial class XMatrix
{
    /// <see cref="return"/> = <see cref="Boolean"/>
    #region

    /// <summary>
    /// Get if contains given <see cref="object"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Contains(this IMatrix i, object j) => i.IndexOf(j) != (-1, -1);

    /// <summary>
    /// Get if contains given <see cref="IVector"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Contains(this IMatrix i, IVector j) => i.IndexOf(j) != (-1, -1);

    /// <summary>
    /// Get if contains given <see cref="IMatrix"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Contains(this IMatrix i, IMatrix j) => i.IndexOf(j) != (-1, -1);

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{T}"/>
    #region

    /// <inheritdoc cref="AddColumn{T}(IMatrix{T}, T, int)"/>
    public static Matrix<object> AddColumn(this IMatrix i, object value, int length = 1)
        => i.NewType(j => j).AddColumn(value, length);

    /// <inheritdoc cref="AddColumn{T}(IMatrix{T}, Func{int, int, T}, int)"/>
    public static Matrix<object> AddColumn(this IMatrix i, Func<int, int, object> value, int length = 1)
        => i.NewType(j => j).AddColumn(value, length);

    /// <inheritdoc cref="AddColumnAt{T}(IMatrix{T}, int, T, int)"/>
    public static Matrix<object> AddColumnAt(this IMatrix i, int index, object value, int length = 1)
        => i.NewType(j => j).AddColumnAt(index, value, length);

    /// <inheritdoc cref="AddColumnAt{T}(IMatrix{T}, int, Func{int, int, T}, int)"/>
    public static Matrix<object> AddColumnAt(this IMatrix i, int index, Func<int, int, object> value, int length = 1)
        => i.NewType(j => j).AddColumnAt(index, value, length);

    /// <inheritdoc cref="AddRow{T}(IMatrix{T}, T, int)"/>
    public static Matrix<object> AddRow(this IMatrix i, object value, int length = 1)
        => i.NewType(j => j).AddRow(value, length);

    /// <inheritdoc cref="AddRow{T}(IMatrix{T}, Func{int, int, T}, int)"/>
    public static Matrix<object> AddRow(this IMatrix i, Func<int, int, object> value, int length = 1)
        => i.NewType(j => j).AddRow(value, length);

    /// <inheritdoc cref="AddRowAt{T}(IMatrix{T}, int, T, int)"/>
    public static Matrix<object> AddRowAt(this IMatrix i, int index, object value, int length = 1)
        => i.NewType(j => j).AddRowAt(index, value, length);

    /// <inheritdoc cref="AddRowAt{T}(IMatrix{T}, int, Func{int, int, T}, int)"/>
    public static Matrix<object> AddRowAt(this IMatrix i, int index, Func<int, int, object> value, int length = 1)
        => i.NewType(j => j).AddRowAt(index, value, length);

    /// <inheritdoc cref="Flip{T}(IMatrix{T}, Axis2)"/>
    public static Matrix<object> Flip(this IMatrix i, Axis2 axis)
        => i.NewType(j => j).Flip(axis);

    /// <inheritdoc cref="Join{T}(IMatrix{T}, IMatrix{T}, MatrixJoin)"/>
    public static Matrix<object> Join(this IMatrix i, IMatrix j, MatrixJoin join)
        => i.NewType(x => x).Join(j, join);

    /// <inheritdoc cref="Join{T}(IMatrix{T}, IVector{T}, MatrixJoin)"/>
    public static Matrix<object> Join(this IMatrix i, IVector j, MatrixJoin join)
        => i.NewType(x => x).Join(j, join);

    /// <inheritdoc cref="RemoveColumnsAfter{T}(IMatrix{T}, int)"/>
    public static Matrix<object> RemoveColumnsAfter(this IMatrix i, int index)
        => i.NewType(j => j).RemoveColumnsAfter(index);

    /// <inheritdoc cref="RemoveColumnAt{T}(IMatrix{T}, int, int)"/>
    public static Matrix<object> RemoveColumnAt(this IMatrix i, int index, int length = 1)
        => i.NewType(j => j).RemoveColumnAt(index, length);

    /// <inheritdoc cref="RemoveColumnLast{T}(IMatrix{T})"/>
    public static Matrix<object> RemoveColumnLast(this IMatrix i)
        => i.NewType(j => j).RemoveColumnLast();

    /// <inheritdoc cref="RemoveRowsAfter{T}(IMatrix{T}, int)"/>
    public static Matrix<object> RemoveRowsAfter(this IMatrix i, int index)
        => i.NewType(j => j).RemoveRowsAfter(index);

    /// <inheritdoc cref="RemoveRowAt{T}(IMatrix{T}, int, int)"/>
    public static Matrix<object> RemoveRowAt(this IMatrix i, int index, int length = 1)
        => i.NewType(j => j).RemoveRowAt(index, length);

    /// <inheritdoc cref="RemoveRowLast{T}(IMatrix{T})"/>
    public static Matrix<object> RemoveRowLast(this IMatrix i)
        => i.NewType(j => j).RemoveRowLast();

    /// <inheritdoc cref="Rotate{T}(IMatrix{T}, int)"/>
    public static Matrix<object> Rotate(this IMatrix i, int rotation)
        => i.NewType(j => j).Rotate(rotation);

    /// <inheritdoc cref="Submatrix{T}(IMatrix{T}, int, int, int, int)"/>
    public static Matrix<object> Submatrix(this IMatrix i, int yIndex, int xIndex, int yLength = 0, int xLength = 0)
        => i.NewType(j => j).Submatrix(yIndex, xIndex, yLength, xLength);

    /// <inheritdoc cref="Transpose{T}(IMatrix{T})"/>
    public static Matrix<object> Transpose(this IMatrix i)
        => i.NewType(j => j).Transpose();

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{TNew}"/>
    #region

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static Matrix<object> New(this IMatrix i) => i.New(j => j);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static Matrix<object> New(this IMatrix i, Func<object, object> select)
    {
        Throw.IfNull(select, nameof(select));
        return i.New((_, j) => select(j));
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static Matrix<object> New(this IMatrix i, Func<int, object, object> select)
    {
        Throw.IfNull(select, nameof(select));
        var index = -1;
        return i.New((_, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray2D.New{TSelf, TValue}(IArray2D{TSelf, TValue}, Func{int, int, TValue, TValue})"/>
    public static Matrix<object> New(this IMatrix i, Func<int, int, object, object> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var result = new object[3][];
        Array2D.Do(3, 3, (y, x) =>
        {
            result[y] ??= new object[3];
            result[y][x] = select(y, x, i[y, x]);
        });
        return new Matrix<object>(result);
    }

    /// <summary>
    /// Get instance of <see cref="Matrix{}"/> from instance of <see cref="IMatrix"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix<T> NewType<T>(this IMatrix i, Func<object, T> select)
        => i.NewType((_, _, j) => select(j));

    /// <summary>
    /// Get instance of <see cref="Matrix{}"/> from instance of <see cref="IMatrix"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix<T> NewType<T>(this IMatrix i, Func<int, int, object, T> select)
    {
        var result = new T[i.Rows][];
        for (int y = 0, rows = i.Rows; y < rows; y++)
        {
            result[y] = new T[i.Columns];
            Array1D.Do(i.Columns, x => result[y][x] = select(y, x, i[y, x]));
        }
        return new Matrix<T>(result);
    }

    #endregion

    /// <see cref="return"/> = <see cref="ValueTuple{,}"/>
    #region

    /// <summary>
    /// Get index of given <see cref="{}"/>.
    /// </summary>
    /// <returns>The column and row (-1 if doesn't exist).</returns>
    /// <exception cref="ArgumentNullException"/>
    [NotImplemented]
    public static (int Y, int X) IndexOf(this IMatrix i, object j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return (-1, -1);
    }

    /// <summary>
    /// Get index of given <see cref="IMatrix"/>.
    /// </summary>
    /// <returns>The column and row (-1 if doesn't exist).</returns>
    /// <exception cref="ArgumentNullException"/>
    [NotImplemented]
    public static (int Y, int X) IndexOf(this IMatrix i, IMatrix j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return (-1, -1);
    }

    /// <summary>
    /// Get index of given <see cref="IVector"/>.
    /// </summary>
    /// <returns>The column and row (-1 if doesn't exist).</returns>
    /// <exception cref="ArgumentNullException"/>
    [NotImplemented]
    public static (int Y, int X) IndexOf(this IMatrix i, IVector j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return (-1, -1);
    }

    #endregion
}

[Extend(typeof(IMatrix<>)), NotComplete]
public static partial class XMatrix
{
    /// <see cref="return"/> = <see cref="T"/>
    #region

    /// <summary>
    /// Get <see cref="{}"/> by aggregating elements.
    /// </summary>
    /// <remarks>
    /// <see langword="var"/> result = <see langword="default"/>;<br/>
    /// <see langword="for"/> (<see cref="int"/> y = 0; y &lt; <see cref="IMatrix.Rows"/>; y++)<br/>
    /// ... <see langword="for"/> (<see cref="int"/> x = 0; x &lt; <see cref="IMatrix.Columns"/>; x++)<br/>
    /// ... ... result = <b>aggregate</b>(y, x, result, <b>i</b>[y, x]) <br/>
    /// <see langword="return"/> result;
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static T Aggregate<T>(this IMatrix<T> i, Func<int, int, T, T, T> aggregate)
        => i.Aggregate<T, T>(aggregate);

    /// <summary>
    /// Get <see cref="{}"/> of new <see cref="Type"/> by aggregating elements of old <see cref="Type"/>.
    /// </summary>
    /// <inheritdoc cref="Aggregate{}(Func{int, int, Value, Value})"/>
    public static TNew Aggregate<TOld, TNew>(this IMatrix<TOld> i, Func<int, int, TNew, TOld, TNew> aggregate)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(aggregate, nameof(aggregate));

        TNew result = default;
        Array2D.Do(i.Rows, i.Columns, (y, x) => result = aggregate(y, x, result, i[y, x]));
        return result;
    }

    #endregion

    /// <see cref="return"/> = <see cref="IEnumerable{T}"/>
    #region

    /// <summary>
    /// Get <see cref="{}"/> of given diagonal.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotSquare"/>
    /// <inheritdoc cref="MatrixDiagonal"/>
    public static IEnumerable<T> GetDiagonal<T>(this IMatrix2D<T> i, MatrixDiagonal side = MatrixDiagonal.Right)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<MatrixNotSquare>(!i.IsSquare(), nameof(i));

        var length = i.Rows;
        for (var y = 0; y < length; y++)
        {
            for (var x = 0; x < length; x++)
            {
                if (side == MatrixDiagonal.Left && x == length - 1 - y)
                    yield return i[y, x];

                else if (side == MatrixDiagonal.Right && y == x)
                    yield return i[y, x];
            }
        }
    }

    /// <summary>
    /// Get <see cref="{}"/> below given diagonal.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotSquare"/>
    public static IEnumerable<T> GetDiagonalLower<T>(this IMatrix2D<T> i, MatrixDiagonal side = MatrixDiagonal.Right)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<MatrixNotSquare>(!i.IsSquare(), nameof(i));

        var length = i.Rows;
        for (var y = 0; y < length; y++)
        {
            for (var x = 0; x < length; x++)
            {
                if (side == MatrixDiagonal.Left && x > length - 1 - y)
                    yield return i[y, x];

                else if (side == MatrixDiagonal.Right && x < y)
                    yield return i[y, x];
            }
        }
    }

    /// <summary>
    /// Get <see cref="{}"/> above given diagonal.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotSquare"/>
    public static IEnumerable<T> GetDiagonalUpper<T>(this IMatrix2D<T> i, MatrixDiagonal side = MatrixDiagonal.Right)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<MatrixNotSquare>(!i.IsSquare(), nameof(i));

        var length = i.Rows;
        for (var y = 0; y < length; y++)
        {
            for (var x = 0; x < length; x++)
            {
                if (side == MatrixDiagonal.Left && x < length - 1 - y)
                    yield return i[y, x];

                else if (side == MatrixDiagonal.Right && x > y)
                    yield return i[y, x];
            }
        }
    }

    #endregion

    /// <see cref="return"/> = <see cref="Boolean"/>
    #region

    /// <inheritdoc cref="Contains(IMatrix, object)"/>
    public static bool Contains<T>(this IMatrix<T> i, T j)
        => i.IndexOf(j) != (-1, -1);

    /// <inheritdoc cref="Contains(IMatrix, IVector)"/>
    public static bool Contains<T>(this IMatrix<T> i, IVector<T> j)
        => i.IndexOf(j) != (-1, -1);

    /// <inheritdoc cref="Contains(IMatrix, IMatrix)"/>
    public static bool Contains<T>(this IMatrix<T> i, IMatrix<T> j)
        => i.IndexOf(j) != (-1, -1);

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{T}"/>
    #region

    /// <summary>
    /// Add given <b>length</b> of columns after last with given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> AddColumn<T>(this IMatrix<T> i, T value, int length = 1)
        => i.AddColumn(value, length);

    /// <summary>
    /// Add given <b>length</b> of columns after last with given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> AddColumn<T>(this IMatrix<T> i, Func<int, int, T> value, int length = 1)
        => i.AddColumn(value, length);

    /// <summary>
    /// Add given <b>length</b> of columns at given <b>index</b> with given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> AddColumnAt<T>(this IMatrix<T> i, int index, T value, int length = 1)
        => i.AddColumnAt(index, (_, _) => value, length);

    /// <summary>
    /// Add given <b>length</b> of columns at given <b>index</b> with given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> AddColumnAt<T>(this IMatrix<T> i, int index, Func<int, int, T> value, int length = 1)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(value, nameof(value));

        Throw.IfLess(index, 0, nameof(index));
        Throw.IfLess(length, 1, nameof(length));
        Throw.IfGreater(index, i.Columns - 1, nameof(index));

        T[][] result = new T[i.Rows][];
        Array1D.Do(i.Rows, y =>
        {
            result[y] = new T[i.Columns + length];
            Array1D.Do(i.Columns + length, x => result[y][x] = x < index ? i[y, x] : x < index + length ? value(y, x) : i[y, x - length]);
        });
        return new(result);
    }

    /// <summary>
    /// Add given <b>length</b> of rows after last with given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> AddRow<T>(this IMatrix<T> i, T value, int length = 1)
        => i.AddRow(value, length);

    /// <summary>
    /// Add given <b>length</b> of rows after last with given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> AddRow<T>(this IMatrix<T> i, Func<int, int, T> value, int length = 1)
        => i.AddRow(value, length);

    /// <summary>
    /// Add given <b>length</b> of rows at given <b>index</b> with given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> AddRowAt<T>(this IMatrix<T> i, int index, T value, int length = 1)
        => i.AddRowAt(index, (_, _) => value, length);

    /// <summary>
    /// Add given <b>length</b> of rows at given <b>index</b> with given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> AddRowAt<T>(this IMatrix<T> i, int index, Func<int, int, T> value, int length = 1)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(value, nameof(value));

        Throw.IfLess(index, 0, nameof(index));
        Throw.IfLess(length, 1, nameof(length));
        Throw.IfGreater(index, i.Rows - 1, nameof(index));

        T[][] result = new T[i.Rows + length][];
        Array1D.Do(i.Rows + length, y =>
        {
            result[y] = new T[i.Columns];
            Array1D.Do(i.Columns, x => result[y][x] = y < index ? i[y, x] : y < index + length ? value(y, x) : i[y - length, x]);
        });
        return new(result);
    }

    /// <summary>Flip over x-axis, y-axis, or both.</summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// <para><see cref="Axis2.X"/></para>
    /// [1, 2, 3] ⇒ [3, 2, 1]<br/>
    /// [4, 5, 6] ⇒ [6, 5, 4]<br/>
    /// [7, 8, 9] ⇒ [9, 8, 7]<br/><br/>
    /// <para><see cref="Axis2.Y"/></para>
    /// [1, 2, 3] ⇒ [7, 8, 9]<br/>
    /// [4, 5, 6] ⇒ [4, 5, 6]<br/>
    /// [7, 8, 9] ⇒ [1, 2, 3]<br/><br/>
    /// <para><see cref="Axis2.X"/> | <see cref="Axis2.Y"/></para>
    /// [1, 2, 3] ⇒ [9, 8, 7]<br/>
    /// [4, 5, 6] ⇒ [6, 5, 4]<br/>
    /// [7, 8, 9] ⇒ [3, 2, 1]<br/>
    /// </remarks>
    public static Matrix<T> Flip<T>(this IMatrix<T> i, Axis2 axis) 
    {
        Throw.IfNull(i, nameof(i));
        
        var result = new T[i.Rows, i.Columns];
        Array2D.Do(i.Rows, i.Columns, (y, x) =>
        {
            switch (axis)
            {
                case Axis2.X:
                    result[y, x] = i[y, i.Columns - 1 - x];
                    break;
                case Axis2.Y:
                    result[y, x] = i[i.Rows - 1 - y, x];
                    break;
                case Axis2.X | Axis2.Y:
                    result[y, x] = i[i.Rows - 1 - y, i.Columns - 1 - x];
                    break;
            }
        });
        return new(result.As());
    }

    /// <summary>
    /// Join with given <see cref="IMatrix"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotImplemented]
    public static Matrix<T> Join<T>(this IMatrix<T> a, IMatrix<T> b, MatrixJoin join)
    {
        int xLength = a.Columns + b.Columns, yLength = a.Rows + b.Rows;

        var result = new T[yLength][];

        /// [1, 2, 3]   [1, 2]   [1, 2, 3, 1, 2]
        /// [4, 5, 6] + [3, 4] = [4, 5, 6, 3, 4]
        /// [7, 8, 9]   [5, 6]   [7, 8, 9, 5, 6]
        if (join == MatrixJoin.Right)
        {
            Array1D.Do(yLength, y =>
            {
                result[y] = new T[xLength];
                Array1D.Do(xLength, x =>
                {
                    /// 1st matrix
                    if (x < a.Columns)
                        result[y][x] = y < a.Rows ? a[y, x] : default;

                    /// 2nd matrix
                    else result[y][x] = y < b.Rows ? b[y, x - a.Columns] : default;
                });
            });
        }
        /// [1, 2, 3]   [1, 2]   [1, 2, 3, 1, 2]
        /// [4, 5, 6] + [3, 4] = [4, 5, 6, 3, 4]
        /// [7, 8, 9]   [5, 6]   [7, 8, 9, 5, 6]
        ///                      [1, 2, 0, 0, 0]
        ///                      [3, 4, 0, 0, 0]
        ///                      [5, 6, 0, 0, 0]
        if (join == MatrixJoin.Below)
        {
            Array1D.Do(yLength, y =>
            {
                result[y] = new T[xLength];
                Array1D.Do(xLength, x =>
                {
                    /// 1st matrix
                    if (y < a.Rows)
                        result[y][x] = x < a.Columns ? a[y, x] : default;

                    /// 2nd matrix
                    else result[y][x] = x < b.Columns ? b[y - a.Rows, x] : default;
                });
            });
        }
        return new(result);
    }

    /// <summary>
    /// Join with given <see cref="IVector"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotImplemented]
    public static Matrix<T> Join<T>(this IMatrix<T> a, IVector<T> b, MatrixJoin join)
    {
        T[][] result = null;

        int xLength = 0, yLength = 0;
        if (join == MatrixJoin.Right)
        {
            /// [1, 2, 3]   [1]   [1, 2, 3, 1]
            /// [4, 5, 6] + [2] = [4, 5, 6, 2]
            /// [7, 8, 9]   [3]   [7, 8, 9, 3]
            if (b.Type == VectorType.X)
            {
                xLength = a.Columns + 1;
                yLength = a.Rows + b.Length;
            }
            /// [1, 2, 3]               [1, 2, 3, 1, 2, 3]
            /// [4, 5, 6] + [1, 2, 3] = [4, 5, 6, 0, 0, 0]
            /// [7, 8, 9]               [7, 8, 9, 0, 0, 0]
            if (b.Type == VectorType.Y)
            {
                xLength = a.Columns + b.Length;
                yLength = a.Rows;
            }
        }
        if (join == MatrixJoin.Below)
        {
            /// [1, 2, 3]   [1]   [1, 2, 3]
            /// [4, 5, 6] + [2] = [4, 5, 6]
            /// [7, 8, 9]   [3]   [7, 8, 9]
            ///                   [1, 0, 0]
            ///                   [2, 0, 0]
            ///                   [3, 0, 0]
            if (b.Type == VectorType.X)
            {
                xLength = a.Columns;
                yLength = a.Rows + b.Length;
            }
            /// [1, 2, 3]               [1, 2, 3]
            /// [4, 5, 6] + [1, 2, 3] = [4, 5, 6]
            /// [7, 8, 9]               [7, 8, 9]
            ///                         [1, 2, 3]
            if (b.Type == VectorType.Y)
            {
                xLength = a.Columns + b.Length;
                yLength = a.Rows + 1;
            }
        }
        Array1D.Do(yLength, y =>
        {
            result[y] = new T[xLength];
            Array1D.Do(xLength, x =>
            {
            });
        });
        return new(result);
    }

    /// <summary>
    /// Remove all columns after given <b>index</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> RemoveColumnsAfter<T>(this IMatrix<T> i, int index)
    {
        Throw.IfNull(i, nameof(i));

        Throw.IfLess(index, 0, nameof(index));
        Throw.IfGreater(index, i.Columns - 1, nameof(index));

        return new(Array2D.Get(i.Rows, index + 1, (y, x) => i[y, x]));
    }

    /// <summary>
    /// Remove column at given <b>index</b> for given <b>length</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> RemoveColumnAt<T>(this IMatrix<T> i, int index, int length = 1)
    {
        Throw.IfNull(i, nameof(i));

        Throw.IfLess(index, 0, nameof(index));
        Throw.IfLess(length, 1, nameof(length));

        Throw.IfGreater(index, i.Columns - 1, nameof(index));

        var a = index;
        var b = (index + length).Clamp(0, i.Columns);

        length = b - index;

        T[][] result = new T[i.Rows][];
        Array1D.Do(i.Rows, y =>
        {
            result[y] = new T[i.Columns - length];
            Array1D.Do(i.Columns, x =>
            {
                if (x < a)
                    result[y][x] = i[y, x];

                else if (x >= b) result[y][x - length] = i[y, x];
            });
        });
        return new(result);
    }

    /// <summary>
    /// Remove last column.
    /// </summary>
    [NotTested]
    public static Matrix<T> RemoveColumnLast<T>(this IMatrix<T> i)
        => i.RemoveRowAt(i.Columns - 1, 1);

    /// <summary>
    /// Remove all rows after given <b>index</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> RemoveRowsAfter<T>(this IMatrix<T> i, int index)
    {
        Throw.IfNull(i, nameof(i));

        Throw.IfLess(index, 0, nameof(index));
        Throw.IfGreater(index, i.Rows - 1, nameof(index));

        return new(Array2D.Get(index + 1, i.Columns, (y, x) => i[y, x]));
    }

    /// <summary>
    /// Remove row at given <b>index</b> for given <b>length</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotTested]
    public static Matrix<T> RemoveRowAt<T>(this IMatrix<T> i, int index, int length = 1)
    {
        Throw.IfNull(i, nameof(i));

        Throw.IfLess(index, 0, nameof(index));
        Throw.IfLess(length, 1, nameof(length));

        Throw.IfGreater(index, i.Rows - 1, nameof(index));

        var a = index;
        var b = (index + length).Clamp(0, i.Rows);

        length = b - index;

        T[][] result = new T[i.Rows - length][];
        Array1D.Do(i.Rows, y =>
        {
            if (y < a)
            {
                result[y] = new T[i.Columns];
                Array1D.Do(i.Columns, x => result[y][x] = i[y, x]);
            }
            else if (y >= b)
            {
                result[y - length] = new T[i.Columns];
                Array1D.Do(i.Columns, x => result[y - length][x] = i[y, x]);
            }
        });
        return new(result);
    }

    /// <summary>
    /// Remove last row.
    /// </summary>
    [NotTested]
    public static Matrix<T> RemoveRowLast<T>(this IMatrix<T> i)
        => i.RemoveRowAt(i.Rows - 1, 1);

    /// <summary>Rotate values clockwise or counterclockwise.</summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// <para>Matrices may be rotated by 90°, 180°, or 270° only (or multiples thereof).</para>
    /// <para><b>Clockwise</b></para>
    /// <para>(+) 1 rotation ≡ 90° or -270°.</para> 
    /// <para><b>Counterclockwise</b></para>
    /// <para>(-) 1 rotation ≡ -90° or 270°.</para>
    /// </remarks>
    /// <returns>A new instance rotated.</returns>
    [Refactor("Replace with jagged array.")]
    public static Matrix<T> Rotate<T>(this IMatrix<T> input, int rotation) 
    {
        Throw.IfNull(input, nameof(input));

        T[,] result = default;

        /// Normalize the rotation (example: +-10 ≡ +-2 ≡ +-180° | +-1 ≡ +-1 +- 90°)
        var d = Convert.ToDouble(rotation) / 4d;
        d -= (int)d;

        var degree = (d - 1d) * 4d;

        //This gets the type of rotation to make; there are a total of four unique rotations possible (0°, 90°, 180°, and 270°).
        //Each correspond to 0, 1, 2, and 3, respectively (or 0, -1, -2, and -3, if in the other direction). Since
        //1 is equivalent to -3 and so forth, we combine both cases into one. 
        switch (degree)
        {
            case -3:
            case +1:
                degree = 3;
                break;

            case -2:
            case +2:
                degree = 2;
                break;

            case -1:
            case +3:
                degree = 1;
                break;

            case -4:
            case 0:
            case +4:
                degree = 0;
                break;
        }
        switch (degree)
        {
            //The rotation is 0, +-180°
            case 0:
            case 2:
                result = new T[input.Rows, input.Columns];
                break;

            //The rotation is +-90°
            case 1:
            case 3:
                result = new T[input.Columns, input.Rows];
                break;
        }

        for (int i = 0, x = input.Columns; i < x; ++i)
        {
            for (int j = 0, y = input.Rows; j < y; ++j)
            {
                switch (degree)
                {
                    //If rotation is 0°
                    case 0:
                        result[j, i] = input[j, i];
                        break;

                    //If rotation is -90°
                    case 1:
                        //Transpose, then reverse each column OR reverse each row, then transpose
                        result[i, j] = input[j, x - i - 1];
                        break;

                    //If rotation is +-180°
                    case 2:
                        //Reverse each column, then reverse each row
                        result[y - 1 - j, x - 1 - i] = input[j, i];
                        break;

                    //If rotation is +90°
                    case 3:
                        //Transpose, then reverse each row
                        result[i, j] = input[y - j - 1, i];
                        break;
                }
            }
        }
        return new(result.As());
    }

    /// <summary>
    /// Get submatrix at given <b>yIndex</b> and <b>xIndex</b> with given <b>yLength</b> (if 0, <b>yIndex</b> to <see cref="IMatrix.Rows"/>) and  <b>xLength</b> (if 0, <b>xIndex</b> to <see cref="IMatrix.Columns"/>).
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsGreaterOrEqual"/>
    /// <exception cref="ArgumentIsNegative"/>
    [NotTested]
    public static Matrix<T> Submatrix<T>(this IMatrix<T> i, int yIndex, int xIndex, int yLength = 0, int xLength = 0)
    {
        Throw.IfNull(i, nameof(i));

        Throw.IfNegative(yIndex, nameof(yIndex));
        Throw.IfNegative(xIndex, nameof(xIndex));
        Throw.IfGreaterOrEqual(yIndex, i.Rows, nameof(yIndex));
        Throw.IfGreaterOrEqual(xIndex, i.Columns, nameof(xIndex));

        Throw.IfNegative(yLength, nameof(yLength));
        Throw.IfNegative(xLength, nameof(xLength));
        yLength = yLength == 0 || yLength + yIndex >= i.Rows ? i.Rows - yIndex : yLength;
        xLength = xLength == 0 || xLength + xIndex >= i.Columns ? i.Columns - xIndex : xLength;

        var result = new T[yLength, xLength];
        Array1D.Do(yIndex, yIndex + yLength, 1, y => Array1D.Do(xIndex, xIndex + xLength, 1, x => result[y - yIndex, x - xIndex] = i[y, x]));
        return new(result.As());
    }

    /// <summary>Flip over diagonal by switching row and column indices.</summary>
    /// <remarks>
    /// [1, 2, 3] ⇒ [1, 4, 7]<br/>
    /// [4, 5, 6] ⇒ [2, 5, 8]<br/>
    /// [7, 8, 9] ⇒ [3, 6, 9]<br/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix<T> Transpose<T>(this IMatrix<T> matrix)
    {
        Throw.IfNull(matrix, nameof(matrix));
#pragma warning disable CA1814
        var result = new T[matrix.Columns, matrix.Rows];
#pragma warning restore CA1814
        for (int row = 0, y = matrix.Rows; row < y; row++)
        {
            for (int column = 0, x = matrix.Columns; column < x; column++)
                result[column, row] = matrix[row, column];
        }

        return new(result.As());
    }

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{TNew}"/>
    #region

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static Matrix<T> New<T>(this IMatrix<T> i) 
        => i.NewType(j => j);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static Matrix<T> New<T>(this IMatrix<T> i, Func<T, T> select)
        => i.NewType(j => j).New(select);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static Matrix<T> New<T>(this IMatrix<T> i, Func<int, T, T> select)
        => i.NewType(j => j).New(select);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <inheritdoc cref="XArray2D.New{TSelf, TValue}(IArray2D{TSelf, TValue}, Func{int, int, TValue, TValue})"/>
    public static Matrix<T> New<T>(this IMatrix<T> i, Func<int, int, T, T> select)
        => i.NewType(j => j).New(select);

    /// <inheritdoc cref="New{}(IMatrix, Func{object, Value})"/>
    public static Matrix<TNew> NewType<TOld, TNew>(this IMatrix<TOld> i, Func<TOld, TNew> select)
        => i.NewType((_, _, j) => select(j));

    /// <inheritdoc cref="New{}(IMatrix, Func{int, int, object, Value})"/>
    public static Matrix<TNew> NewType<TOld, TNew>(this IMatrix<TOld> i, Func<int, int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        var result = Array2D.Get(i.Rows, i.Columns, (y, x) => select(y, x, i[y, x]));
        return new Matrix<TNew>(result);
    }

    #endregion

    /// <see cref="return"/> = <see cref="ValueTuple{,}"/>
    #region

    /// <inheritdoc cref="IndexOf(IMatrix, object)"/>
    [NotImplemented]
    public static (int X, int Y) IndexOf<T>(this IMatrix<T> i, T j)
    {
        for (var row = 0; row < i.Rows; row++)
        {
            for (var column = 0; column < i.Columns; column++)
            {
                if (EqualityComparer<T>.Default.Equals(i[row, column], j))
                    return (column, row);
            }
        }
        return (-1, -1);
    }

    /// <inheritdoc cref="IndexOf(IMatrix, IVector)"/>
    [NotImplemented]
    public static (int X, int Y) IndexOf<T>(this IMatrix<T> i, IVector<T> j)
    {
        for (var row = 0; row < i.Rows; row++)
        {
            var found = false; int index = 0, count = 0;
            for (var column = 0; column < i.Columns; column++)
            {
                if (count == j.Length)
                    break;

                if (EqualityComparer<T>.Default.Equals(i[row, column], j[index + count]))
                {
                    if (found)
                        count++;

                    else
                    {
                        found = true;
                        index = column;
                        count = 1;
                    }
                }
                else if (found)
                {
                    found = false;
                    break;
                }
            }
            if (found)
                return (index, row);
        }
        return (-1, -1);
    }

    /// <inheritdoc cref="IndexOf(IMatrix, IMatrix)"/>
    [NotImplemented]
    public static (int X, int Y) IndexOf<T>(this IMatrix<T> i, IMatrix<T> j)
    {
        for (var row = 0; row < i.Rows; row++)
        {
            for (var column = 0; column < i.Columns; column++)
            {
                if (j.Columns > i.Columns - column || j.Rows > i.Rows - row)
                    continue;

                for (var _row = 0; _row < j.Rows; _row++)
                {
                    for (var _column = 0; _column < j.Columns; _column++)
                    {
                        if (!EqualityComparer<T>.Default.Equals(i[row + _row, column + _column], j[_row, _column]))
                            goto Skip;
                    }
                }
                return (column, row);
            Skip: continue;
            }
        }
        return (-1, -1);
    }

    #endregion

    /// <see cref="return"/> = <see cref="Void"/>
    #region

    /// <summary>
    /// Do given <see cref="Void"/> for each value.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void ForEach<T>(this IMatrix<T> i, Void<int, int> action)
        => i.ForEach(new Void<int, int, T>((y, x, _) => action(y, x)));

    /// <summary>
    /// Do given <see cref="Void"/> for each value.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void ForEach<T>(this IMatrix<T> i, Void<int, int, T> action)
        => Array2D.Do(i.Rows, i.Columns, (row, column) => action(row, column, i[row, column]));

    #endregion
}

[Extend(typeof(IMatrix2D<,>))]
public static partial class XMatrix
{
    private static TSelf Do<TSelf, TValue>(this IMatrix<TSelf, TValue> i, Func<IMatrix<TValue>, Matrix<TValue>> action)
        where TSelf : IMatrix<TSelf, TValue>
    {
        Throw.IfNull(i, nameof(i));
        var result = action(i).ToArray();

        Throw.If<MatrixNotImplemented>(result is null, nameof(i));
        return TSelf.Create((TSelf)i, result);
    }

    /// <see cref="return"/> = <see cref="IMatrix{T}"/>
    #region

    /// <inheritdoc cref="AddColumn{T}(IMatrix{T}, T, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf AddColumn<TSelf, TValue>(this IMatrix<TSelf, TValue> i, TValue value, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.AddColumn(value, length));

    /// <inheritdoc cref="AddColumn{T}(IMatrix{T}, Func{int, int, T}, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf AddColumn<TSelf, TValue>(this IMatrix<TSelf, TValue> i, Func<int, int, TValue> value, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.AddColumn(value, length));

    /// <inheritdoc cref="AddColumnAt{T}(IMatrix{T}, int, T, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf AddColumnAt<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int index, TValue value, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.AddColumnAt(index, value, length));

    /// <inheritdoc cref="AddColumnAt{T}(IMatrix{T}, int, Func{int, int, T}, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf AddColumnAt<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int index, Func<int, int, TValue> value, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.AddColumnAt(index, value, length));

    /// <inheritdoc cref="AddRow{T}(IMatrix{T}, T, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf AddRow<TSelf, TValue>(this IMatrix<TSelf, TValue> i, TValue value, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.AddRow(value, length));

    /// <inheritdoc cref="AddRow{T}(IMatrix{T}, Func{int, int, T}, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf AddRow<TSelf, TValue>(this IMatrix<TSelf, TValue> i, Func<int, int, TValue> value, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.AddRow(value, length));

    /// <inheritdoc cref="AddRowAt{T}(IMatrix{T}, int, T, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf AddRowAt<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int index, TValue value, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.AddRowAt(index, value, length));

    /// <inheritdoc cref="AddRowAt{T}(IMatrix{T}, int, Func{int, int, T}, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf AddRowAt<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int index, Func<int, int, TValue> value, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.AddRowAt(index, value, length));

    /// <inheritdoc cref="Flip{T}(IMatrix{T}, Axis2)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf Flip<TSelf, TValue>(this IMatrix<TSelf, TValue> i, Axis2 axis)
        where TSelf : IMatrix<TSelf, TValue> => i.Do(j => j.Flip(axis));

    /// <inheritdoc cref="Join{T}(IMatrix{T}, IMatrix{T}, MatrixJoin)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf Join<TSelf, TValue>(this IMatrix<TSelf, TValue> i, IMatrix<TValue> j, MatrixJoin join)
        where TSelf : IMatrix<TSelf, TValue> => i.Do(x => x.Join(j, join));

    /// <inheritdoc cref="Join{T}(IMatrix{T}, IVector{T}, MatrixJoin)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf Join<TSelf, TValue>(this IMatrix<TSelf, TValue> i, IVector<TValue> j, MatrixJoin join)
        where TSelf : IMatrix<TSelf, TValue> => i.Do(x => x.Join(j, join));

    /// <inheritdoc cref="RemoveColumnsAfter{T}(IMatrix{T}, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf RemoveColumnsAfter<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int index)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.RemoveColumnsAfter(index));

    /// <inheritdoc cref="RemoveColumnAt{T}(IMatrix{T}, int, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf RemoveColumnAt<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int index, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.RemoveColumnAt(index, length));

    /// <inheritdoc cref="RemoveColumnLast{T}(IMatrix{T})"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf RemoveColumnLast<TSelf, TValue>(this IMatrix<TSelf, TValue> i)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.RemoveColumnLast());

    /// <inheritdoc cref="RemoveRowsAfter{T}(IMatrix{T}, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf RemoveRowsAfter<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int index)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.RemoveRowsAfter(index));

    /// <inheritdoc cref="RemoveRowAt{T}(IMatrix{T}, int, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf RemoveRowAt<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int index, int length = 1)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.RemoveRowAt(index, length));

    /// <inheritdoc cref="RemoveRowLast{T}(IMatrix{T})"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf RemoveRowLast<TSelf, TValue>(this IMatrix<TSelf, TValue> i)
        where TSelf : IMatrix<TSelf, TValue>, IMatrixUnfixed<TValue> => i.Do(j => j.RemoveRowLast());

    /// <inheritdoc cref="Rotate{T}(IMatrix{T}, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf Rotate<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int rotation)
        where TSelf : IMatrix<TSelf, TValue> => i.Do(j => j.Rotate(rotation));

    /// <inheritdoc cref="Submatrix{T}(IMatrix{T}, int, int, int, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf Submatrix<TSelf, TValue>(this IMatrix<TSelf, TValue> i, int yIndex, int xIndex, int yLength = 0, int xLength = 0)
        where TSelf : IMatrix<TSelf, TValue> => i.Do(j => j.Submatrix(yIndex, xIndex, yLength, xLength));

    /// <inheritdoc cref="Transpose{T}(IMatrix{T})"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static TSelf Transpose<TSelf, TValue>(this IMatrix<TSelf, TValue> i)
        where TSelf : IMatrix<TSelf, TValue> => i.Do(j => j.Transpose());

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{TNew}"/>
    #region

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i)
        where TSelf : IMatrix2D<TSelf, TValue> => (i as IArray<TSelf, TValue>).New();

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{TValue, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, Func<TValue, TValue> select)
        where TSelf : IMatrix2D<TSelf, TValue> => (i as IArray<TSelf, TValue>).New(select);

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{int, TValue, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, Func<int, TValue, TValue> select)
        where TSelf : IMatrix2D<TSelf, TValue> => (i as IArray<TSelf, TValue>).New(select);

    /// <inheritdoc cref="XArray2D.New{TSelf, TValue}(IArray2D{TSelf, TValue}, Func{int, int, TValue, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, Func<int, int, TValue, TValue> select)
        where TSelf : IMatrix2D<TSelf, TValue> => (i as IArray2D<TSelf, TValue>).New(select);

    #endregion
}

[Extend(typeof(IMatrix3D<,>))]
public static partial class XMatrix
{
    /// <see cref="return"/> = <see cref="IMatrix{TNew}"/>
    #region

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IMatrix3D<TSelf, TValue> i) where TSelf : IMatrix3D<TSelf, TValue> => i.New(j => j);

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{TValue, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IMatrix3D<TSelf, TValue> i, Func<TValue, TValue> select)
        where TSelf : IMatrix3D<TSelf, TValue> => (i as IArray<TSelf, TValue>).New(select);

    /// <inheritdoc cref="XArray1D.New{TSelf, TValue}(IArray{TSelf, TValue}, Func{int, TValue, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IMatrix3D<TSelf, TValue> i, Func<int, TValue, TValue> select)
        where TSelf : IMatrix3D<TSelf, TValue> => (i as IArray<TSelf, TValue>).New(select);

    /// <inheritdoc cref="XArray3D.New{TSelf, TValue}(IArray3D{TSelf, TValue}, Func{int, int, int, TValue, TValue})"/>
    public static TSelf New<TSelf, TValue>(this IMatrix3D<TSelf, TValue> i, Func<int, int, int, TValue, TValue> select)
        where TSelf : IMatrix3D<TSelf, TValue> => (i as IArray3D<TSelf, TValue>).New(select);

    #endregion
}

[Extend(typeof(IMatrix3x3<>))]
public static partial class XMatrix
{
    /// <see cref="return"/> = <see cref="IMatrix{T}"/>
    #region

    /// <inheritdoc cref="AddColumn{T}(IMatrix{T}, T, int)"/>
    public static Matrix<T> AddColumn<T>(this IMatrix3x3<T> i, IVector3<T> value, int length = 1)
        => i.AddColumn(value, length);

    /// <inheritdoc cref="AddColumnAt{T}(IMatrix{T}, int, T, int)"/>
    public static Matrix<T> AddColumnAt<T>(this IMatrix3x3<T> i, int index, IVector3<T> value, int length = 1)
        => i.AddColumnAt(index, (y, x) => value[y], length);

    /// <inheritdoc cref="AddRow{T}(IMatrix{T}, T, int)"/>
    public static Matrix<T> AddRow<T>(this IMatrix3x3<T> i, IVector3<T> value, int length = 1)
        => i.AddRow(value, length);

    /// <inheritdoc cref="AddRowAt{T}(IMatrix{T}, int, T, int)"/>
    public static Matrix<T> AddRowAt<T>(this IMatrix3x3<T> i, int index, IVector3<T> value, int length = 1)
        => i.AddRowAt(index, (y, x) => value[x], length);

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{TNew}"/>
    #region

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> New<T>(this IMatrix3x3<T> i) 
        where T : INumber<T> => i.New(j => j);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> New<T>(this IMatrix3x3<T> i, Func<T, T> select)
        where T : INumber<T>
    {
        Throw.IfNull(select, nameof(select));
        return i.New((_, j) => select(j));
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> New<T>(this IMatrix3x3<T> i, Func<int, T, T> select)
        where T : INumber<T>
    {
        Throw.IfNull(select, nameof(select));
        var index = -1;
        return i.New((_, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> New<T>(this IMatrix3x3<T> i, Func<int, int, T, T> select)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var result = new T[3][];
        Array2D.Do(3, 3, (y, x) =>
        {
            result[y] ??= new T[3];
            result[y][x] = select(y, x, i[y, x]);
        });
        return result.To(x => new Matrix3x3<T>(x[0][0], x[0][1], x[0][2], x[1][0], x[1][1], x[1][2], x[2][0], x[2][1], x[2][2]));
    }

    #endregion
}
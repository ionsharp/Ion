using System;
using System.Text;

namespace Ion.Numeral;

/// <summary>
/// A 3-dimensional <see cref="IMatrix"/>.
/// </summary>
[Using(typeof(Array2D), typeof(Array3D), typeof(IMatrix))]
[Using(typeof(Throw))]
public interface IMatrix3D : IMatrix2D, IArray3D, IArray3DRank
{
    new public const string Description = "An 3-dimensional set of values.";

    /// <summary>
    /// Because <see langword="this"/> is <see cref="IMatrix3D{,}"/>, not <see cref="IMatrix{,}"/>.
    /// </summary>
    public const string NotImplemented = "";

    /// <remarks><b>Needed for terminology.</b></remarks>
    new public object this[int slice, int row, int column] { get; }

    object IArray3D.this[int z, int y, int x] => this[z, y, x];

    /// <remarks><b>Needed for terminology (<see cref="IArray3D.Length"/>).</b></remarks>
    new (int Slices, int Rows, int Columns) Length => (Slices, Rows, Columns);

    /// <remarks><b>Needed for terminology (<see cref="IArray3D.ZLength"/>).</b></remarks>
    public int Slices { get; }

    int IArray.Length => Slices * Columns * Rows;

    (int Z, int Y, int X) IArray3D.Length => (Slices, Rows, Columns);

    int IArray3D.ZLength => Slices;

    /// <see cref="Region.Method"/>

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> with same columns, rows, and slices.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static T[][][] Format<T>(int length, in T value = default)
        => Format(length, length, length, value);

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> with same columns, rows, and slices, and <see cref="MatrixFill"/> with values <b>a</b> and <b>b</b>.
    /// </summary>
    /// <inheritdoc cref="IMatrix.Format{T}(int, in T, in T, MatrixFill)"/>
    public static T[][][] Format<T>(int length, in T a, in T b = default, MatrixFill fill = default, int slices = 1, Matrix3DFillSide side = default)
        => Format(IMatrix.Format(length, a, b, fill), slices, side);

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> with given <b>rows</b> and <b>columns</b>, and 1 slice.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Format<T>(int rows, int columns, in T value, int slices = 1, Matrix3DFillSide side = default)
    {
        Throw.IfNull(value, nameof(value));
        var _value = value;

        var result = Array2D.Get(rows, columns, (y, x) => _value);
        return Format(result, slices, side);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> with given <b>slices</b>, <b>rows</b> and <b>columns</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Format<T>(int slices, int rows, int columns, in T value = default)
    {
        Throw.IfNull(value, nameof(value));

        var _value = value;
        return Array3D.Get(slices, rows, columns, (z, y, x) => _value);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given 1-dimensional <see cref="Array"/>.
    /// </summary>
    /// <inheritdoc cref="IMatrix.Format{T}(in T[], int, Axis2)"/>
    public static T[][][] Format<T>(in T[] i, Matrix2DFromArray1D fill = default, int slices = 1, Matrix3DFillSide side = default)
        => Format(IMatrix.Format(i, fill), slices, side);

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given 2-dimensional <see cref="Array"/> with given <b>slices</b>.
    /// </summary>
    /// <inheritdoc cref="IMatrix.Format{T}(in T[][])"/>
    public static T[][][] Format<T>(in T[][] i, int slices = 1, Matrix3DFillSide side = default)
    {
        _ = IMatrix.Format(i);
        var length = i.GetLength();

        T[][][] result = null;

        int zCount = slices, yCount = length.Y, xCount = length.X;

        switch (side)
        {
            case Matrix3DFillSide.Back:
            case Matrix3DFillSide.Front:
                result = new T[slices][][];
                break;
            case Matrix3DFillSide.Bottom:
            case Matrix3DFillSide.Top:
                result = new T[length.Y][][];

                yCount = slices;
                zCount = length.Y;
                break;
            case Matrix3DFillSide.Left:
            case Matrix3DFillSide.Right:
                result = new T[length.X][][];

                xCount = slices;
                zCount = length.X;
                break;
        }

        for (var z = 0; z < zCount; z++)
        {
            switch (side)
            {
                case Matrix3DFillSide.Back:
                case Matrix3DFillSide.Front:
                case Matrix3DFillSide.Left:
                case Matrix3DFillSide.Right:
                    result[z] = new T[length.Y][];
                    break;
                case Matrix3DFillSide.Bottom:
                case Matrix3DFillSide.Top:
                    result[z] = new T[slices][];
                    break;
            }
            for (var y = 0; y < yCount; y++)
            {
                switch (side)
                {
                    case Matrix3DFillSide.Back:
                    case Matrix3DFillSide.Front:
                    case Matrix3DFillSide.Bottom:
                    case Matrix3DFillSide.Top:
                        result[z][y] = new T[length.X];
                        break;
                    case Matrix3DFillSide.Left:
                    case Matrix3DFillSide.Right:
                        result[z][y] = new T[slices];
                        break;
                }
                for (var x = 0; x < xCount; x++)
                {
                    switch (side)
                    {
                        case Matrix3DFillSide.Back:
                            result[z][y][x] = i[y][xCount - 1 - x];
                            break;
                        case Matrix3DFillSide.Bottom:
                            result[z][y][x] = i[z][x];
                            break;
                        case Matrix3DFillSide.Front:
                            result[z][y][x] = i[y][x];
                            break;
                        case Matrix3DFillSide.Left:
                            result[z][y][x] = i[y][zCount - 1 - z];
                            break;
                        case Matrix3DFillSide.Right:
                            result[z][y][x] = i[y][z];
                            break;
                        case Matrix3DFillSide.Top:
                            result[z][y][x] = i[zCount - 1 - z][x];
                            break;
                    }
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given 3-dimensional <see cref="Array"/>.
    /// </summary>
    /// <remarks>
    ///  <b>All slices have same rows and all rows have same columns, with any number of slices, rows, and columns.</b>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayJaggedZeroColumns"/>
    /// <exception cref="ArrayJaggedZeroRows"/>
    /// <exception cref="ArrayJaggedZeroSlices"/>
    /// <exception cref="ArrayJaggedNotUniform"/>
    new public static T[][][] Format<T>(in T[][][] i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArrayJaggedZeroSlices>(i.Length < 1, nameof(i));

        var result = new T[i.Length][][];
        for (int z = 0, zLength = 0; z < i.Length; z++)
        {
            var slice = i[z];
            Throw.IfNull(slice, nameof(slice));

            result[z] = new T[slice.Length][];

            if (zLength == 0)
                zLength = slice.Length;

            Throw.IfEqual<ArrayJaggedZeroRows>(slice.Length, 0, nameof(i));
            Throw.IfNotEqual<ArrayJaggedNotUniform>(slice.Length, zLength, nameof(i));

            for (int y = 0, yLength = 0; y < slice.Length; y++)
            {
                var row = i[y];
                Throw.IfNull(row, nameof(row));

                result[z][y] = new T[row.Length];

                if (yLength == 0)
                    yLength = row.Length;

                Throw.IfEqual<ArrayJaggedZeroColumns>(row.Length, 0, nameof(i));
                Throw.IfNotEqual<ArrayJaggedNotUniform>(row.Length, yLength, nameof(i));

                for (var x = 0; x < row.Length; x++)
                    result[z][y][x] = i[z][y][x];
            }
        }
        return result;
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given <see cref="IArray1D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayNotImplemented"/>
    public static T[][][] Format<T>(in IArray1D<T> i, Matrix2DFromArray1D fill = default, int slices = 1, Matrix3DFillSide side = default)
    {
        Throw.IfNull(i, nameof(i));

        var j = i.ToArray();
        return Try.Get(() => Format(j, fill, slices, side), e => throw new ArrayNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given <see cref="IArray2D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayNotImplemented"/>
    public static T[][][] Format<T>(in IArray2D<T> i, int slices = 1, Matrix3DFillSide side = default)
    {
        Throw.IfNull(i, nameof(i));

        var j = i.ToArray();
        return Try.Get(() => Format(j, slices, side), e => throw new ArrayNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given <see cref="IArray3D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayNotImplemented"/>
    new public static T[][][] Format<T>(in IArray3D<T> i)
    {
        Throw.IfNull(i, nameof(i));

        var j = i.ToArray();
        return Try.Get(() => Format(j), e => throw new ArrayNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given <see cref="IMatrix2D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Format<T>(in IMatrix2D<T> i, int slices = 1, Matrix3DFillSide side = default)
    {
        Throw.IfNull(i, nameof(i));

        var j = i.ToArray();
        return Try.Get(() => Format(j, slices, side), e => throw new MatrixNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given <see cref="IMatrix3D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static T[][][] Format<T>(in IMatrix3D<T> i)
    {
        Throw.IfNull(i, nameof(i));

        var j = i.ToArray();
        return Try.Get(() => Format(j), e => throw new MatrixNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3D"/> from given <see cref="IVector{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Format<T>(in IVector<T> i, int repeat = 1, int slices = 1, Matrix3DFillSide side = default)
    {
        Throw.IfNull(i, nameof(i));

        var j = i.ToArray(); var k = i.Type;
        return Try.Get(() => Format(j, new(repeat, (Axis2)(int)k), slices, side), e => throw new VectorNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get <see cref="IMatrix3D{}"/> as <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    /// <inheritdoc cref="IMatrix.ToString{T}(IMatrix2D{T}, string, IFormatProvider)"/>
    [NotTested]
    public static string ToString<T>(IMatrix3D<T> matrix, string format, IFormatProvider provider)
    {
        Throw.IfNull(matrix, nameof(matrix));

        StringBuilder result = new();
        for (var z = 0; z < matrix.Slices; z++)
        {
            result.Append($"Z = {z}\n");

            var s = Try.Get(() => matrix.ToArray(z), e => throw new MatrixNotImplemented(nameof(matrix), e));
            var slice = new Matrix<T>(s);

            result.Append(IMatrix.ToString(slice, format, provider));
            result.Append("\n\n");
        }
        return default;
    }
}

/// <inheritdoc/>
public interface IMatrix3D<T> : IMatrix3D, IMatrix2D<T>, IArray3D<T>, IArray3DRank<T>
{
    /// <remarks><b>Illogical to support!</b></remarks>
    T IMatrix<T>.this[int row, int column] => throw new NotSupportedException();

    /// <remarks><b>Needed for terminology.</b></remarks>
    new T this[int slice, int row, int column] { get; }

#pragma warning disable CA1819 /// Properties should not return arrays
    /// <remarks><b>Needed for terminology.</b></remarks>
    new T[] this[int slice, int row] { get; }

    /// <remarks><b>Needed for terminology.</b></remarks>
    new T[][] this[int slice] { get; }
#pragma warning restore CA1819

    T IArray3D<T>.this[int z, int y, int x] => this[z, y, x];

#pragma warning disable CA1819 /// Properties should not return arrays
    T[] IArray3D<T>.this[int z, int y] => this[z, y];

    T[][] IArray3D<T>.this[int z] => this[z];
#pragma warning restore CA1819

    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => Slices * Columns * Rows;

    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    (int Z, int Y, int X) IArray3D.Length => (Slices, Rows, Columns);

    /// <remarks><b>Needed for terminology.</b></remarks>
    new public T[] ToArray(int slice, int row);

    /// <remarks><b>Needed for terminology.</b></remarks>
    new public T[][] ToArray(int slice);

    /// <remarks><b>Illogical to support!</b></remarks>
    object[] IArray1D.ToArray() => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T[] IArray1D<T>.ToArray() => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    object[][] IArray2D.ToArray() => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T[] IArray2D<T>.ToArray(int y) => throw new NotSupportedException();

    T[] IArray3D<T>.ToArray(int z, int y) => ToArray(y, z);

    T[][] IArray3D<T>.ToArray(int z) => ToArray(z);

    /// <remarks><b>Illogical to support!</b></remarks>
    T[] IMatrix<T>.ToArray(int row) => throw new NotSupportedException();
}

/// <inheritdoc/>
public interface IMatrix3D<TSelf, TValue> : IMatrix3D<TValue>, IArray3D<TSelf, TValue> where TSelf : IMatrix3D<TSelf, TValue>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => Slices * Columns * Rows;

    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    (int Z, int Y, int X) IArray3D.Length => (Slices, Rows, Columns);
}
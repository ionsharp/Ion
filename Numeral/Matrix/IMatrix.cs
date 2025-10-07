using System;
using System.Text;

namespace Ion.Numeral;

/// <summary>
/// An <i>n</i>-dimensional set of values (where <i>n</i> >= 2).
/// </summary>
[Using(typeof(Array2D))]
[Using(typeof(Throw))]
public interface IMatrix : IArray2D
{
    public const string Description = "An n-dimensional set of values.";

    /// <remarks><b>Needed for terminology (<see cref="IArray1D.XLength"/>).</b></remarks>
    public int Columns { get; }

    /// <remarks><b>Needed for terminology (<see cref="IArray2D.YLength"/>).</b></remarks>
    public int Rows { get; }

    /// <remarks><b>Needed for terminology (<see cref="IArray2D.Length"/>).</b></remarks>
    new (int Rows, int Columns) Length => (Rows, Columns);

    int IArray1D.XLength => Columns;

    int IArray2D.YLength => Rows;

    /// <see cref="Region.Field"/>
    #region

    /// <summary>A prefix to indicate a horizontal string representation.</summary>
    /// <remarks>
    /// <para><b>X-</b></para>
    /// <para>[0,1,0],[1,0,1]...</para>
    /// </remarks>
    public const string StringFormatX = "X-";

    /// <summary>A delimiter to separate columns.</summary>
    /// <remarks>Applies to <see cref="StringFormatX"/>.</remarks>
    public const char StringFormatXDelimiterColumn = ',';

    /// <summary>A delimiter to separate rows.</summary>
    /// <remarks>Applies to <see cref="StringFormatX"/>.</remarks>
    public const char StringFormatXDelimiterRow = ',';

    /// <summary>The string format of each row.</summary>
    /// <remarks>Applies to <see cref="StringFormatX"/>.</remarks>
    public const string StringFormatXRow = "[{0}]";

    /// <summary>See <see cref="StringFormatXRow"/>.</summary>
    /// <remarks>Applies to <see cref="StringFormatX"/>.</remarks>
    public static readonly CompositeFormat StringFormatXRowComposite = CompositeFormat.Parse(StringFormatXRow);

    /// <summary>A prefix to indicate a vertical string representation.</summary>
    /// <remarks>
    /// <para><b>Y-</b></para>
    /// <para>[0,1,0]</para>
    /// <para>[0,1,0]</para>
    /// <para>...</para>
    /// </remarks>
    public const string StringFormatY = "Y-";

    /// <summary>A character to append to left and right of decimal point so all decimal points align.</summary>
    /// <remarks>
    /// <para>Appends to right of negative sign. Negative sign will occur at farthest left!</para>
    /// <para>Applies to <see cref="StringFormatY"/>.</para>
    /// </remarks>
    public const char StringFormatYPad = ' ';

    /// <summary>The string format of each row.</summary>
    /// <remarks>Applies to <see cref="StringFormatY"/>.</remarks>
    public const string StringFormatYRow = "[{0}]";

    /// <summary>See <see cref="StringFormatYRow"/>.</summary>
    /// <remarks>Applies to <see cref="StringFormatY"/>.</remarks>
    public static readonly CompositeFormat StringFormatYRowComposite = CompositeFormat.Parse(StringFormatYRow);

    /// <summary>The string to append to each value.</summary>
    /// <remarks>Applies to <see cref="StringFormatY"/>.</remarks>
    public const string StringFormatYSpace = ", ";

    #endregion

    /// <see cref="Region.Method"/>

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> with same columns and rows, and given <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Format<T>(int length, in T value = default) => Format(length, length, value);

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> with same columns and rows, and <see cref="MatrixFill"/> with values <b>a</b> and <b>b</b>.
    /// </summary>
    /// <remarks>
    /// <para><i>Default</i> ≡ <see cref="MatrixFill.AlternateXY"/></para>
    /// <para><b>Square</b></para>
    /// <see cref="MatrixFill"/>.All
    /// <para><b>Rectangle</b></para>
    /// <see cref="MatrixFill.AlternateX"/> | <see cref="MatrixFill.AlternateXY"/> | <see cref="MatrixFill.AlternateY"/>
    /// </remarks>
    /// <returns>A new instance of <see cref="IMatrix"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotCompatible"/>
    public static T[][] Format<T>(int length, in T a, in T b, MatrixFill fill)
    {
        Throw.IfNull(a, nameof(a));
        Throw.IfNull(b, nameof(b));

        var _a = a;
        var _b = b;

        var yLength = length;
        var xLength = length;

        T[][] result = null;
        switch (fill)
        {
            case MatrixFill.AlternateX:
            case MatrixFill.AlternateXY:
                /// y ⇓
                for (int y = 0, flip = 0; y < yLength; y++, flip++)
                {
                    result[y] = new T[xLength];
                    /// x ⇒
                    for (var x = 0; x < xLength; x++)
                    {
                        var m = int.IsEvenInteger(flip);
                        var n = int.IsEvenInteger(x);
                        result[y][x] = fill switch
                        {
                            MatrixFill.AlternateX => n ? _a : _b,
                            MatrixFill.AlternateXY => m && n ? _a : m && !n ? _b : n ? _b : _a
                        };
                    }
                }
                break;
            case MatrixFill.AlternateY:
                var temp = new T[yLength, xLength];
                Array2D.Do(yLength, xLength, (y, x) => temp[y, x] = int.IsEvenInteger(y) ? _a : _b);
                result = temp.As();
                break;
            case MatrixFill.DiagonalBoth:
            case MatrixFill.DiagonalLeft:
            case MatrixFill.DiagonalRight:
            case MatrixFill.TriagonalLowerLeft:
            case MatrixFill.TriagonalLowerRight:
            case MatrixFill.TriagonalUpperLeft:
            case MatrixFill.TriagonalUpperRight:
                Throw.IfNotEqual<MatrixNotCompatible>(xLength, yLength);
                /// i ⇓
                for (var y = 0; y < length; y++)
                {
                    var last = length - 1 - y;

                    var left = fill == MatrixFill.DiagonalLeft || fill == MatrixFill.TriagonalLowerLeft || fill == MatrixFill.TriagonalUpperLeft;

                    var triangular = fill == MatrixFill.TriagonalLowerLeft || fill == MatrixFill.TriagonalUpperLeft || fill == MatrixFill.TriagonalLowerRight || fill == MatrixFill.TriagonalUpperRight;

                    var x = left ? last : y;

                    result[y] = new T[length];
                    result[y][x] = _a;

                    if (fill == MatrixFill.DiagonalBoth)
                        result[y][length - 1 - x] = _a;

                    if (triangular)
                    {
                        x++;

                        /// j ⇒
                        for (var x1 = x; x1 < length; x1++)
                        {
                            if (fill == MatrixFill.TriagonalLowerLeft || fill == MatrixFill.TriagonalLowerRight)
                                result[y][x1] = _b;

                            if (fill == MatrixFill.TriagonalUpperLeft || fill == MatrixFill.TriagonalUpperRight)
                                result[y][x1] = _a;
                        }

                        x = (left ? last : y) - 1;

                        /// ⇐ j
                        for (var x2 = x; x2 >= 0; x2--)
                        {
                            if (fill == MatrixFill.TriagonalLowerRight || fill == MatrixFill.TriagonalLowerLeft)
                                result[y][x2] = _a;

                            if (fill == MatrixFill.TriagonalUpperRight || fill == MatrixFill.TriagonalUpperLeft)
                                result[y][x2] = _b;
                        }
                    }
                }
                break;
        }
        return result;
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> with given <b>columns</b>, <b>rows</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Format<T>(int rows, int columns, in T value)
    {
        Throw.IfNull(value, nameof(value));

        var _value = value;
        return Array2D.Get(rows, columns, (y, x) => _value);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given 1-dimensional <see cref="Array"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>1-dimensional <see cref="Array"/> ≡ <see cref="IVector.DefaultType"/> ≡ <see cref="VectorType.X"/></b></para>
    /// Must have minimum length of 1.
    /// </remarks>
    /// <exception cref="ArgumentIsNegativeOrZero"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayLengthZero"/>
    public static T[][] Format<T>(in T[] i, Matrix2DFromArray1D fill = default)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArrayLengthZero>(i.Length == 0, nameof(i));
        Throw.IfNegativeOrZero(fill.Repeat, nameof(fill));

        var j = i;
        return fill.Axis switch
        {
            Axis2.X => Array2D.Get(i.Length, fill.Repeat, (y, x) => j[y]),
            Axis2.Y => Array2D.Get(fill.Repeat, i.Length, (y, x) => j[x]),
        };
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given 2-dimensional <see cref="Array"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayJaggedZeroColumns"/>
    /// <exception cref="ArrayJaggedZeroRows"/>
    /// <exception cref="ArrayJaggedNotUniform"/>
    /// <remarks><para><b>All rows have same columns with any number of rows and columns.</b></para></remarks>
    public static T[][] Format<T>(in T[][] i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfEqual<ArrayJaggedZeroRows>(i.Length, 0);

        var result = new T[i.Length][];
        for (int y = 0, yLength = 0; y < i.Length; y++)
        {
            var row = i[y];
            result[y] = new T[row.Length];

            if (yLength == 0)
                yLength = row.Length;

            Throw.IfEqual<ArrayJaggedZeroColumns>(row.Length, 0);
            Throw.IfNotEqual<ArrayJaggedNotUniform>(row.Length, yLength);

            for (var x = 0; x < row.Length; x++)
                result[y][x] = i[y][x];
        }
        return result;
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given 3-dimensional <see cref="Array"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Format<T>(in T[][][] i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfEqual<ArrayJaggedZeroSlices>(i.Length, 0);
        return Format(i[0]);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given <see cref="IArray1D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayNotImplemented"/>
    public static T[][] Format<T>(in IArray1D<T> i, Matrix2DFromArray1D fill = default)
    {
        Throw.IfNull(i, nameof(i));
        T[] j = i.ToArray();
        return Try.Get(() => Format(j, fill), e => throw new ArrayNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given <see cref="IArray2D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayNotImplemented"/>
    public static T[][] Format<T>(in IArray2D<T> i)
    {
        Throw.IfNull(i, nameof(i));
        T[][] j = i.ToArray();
        return Try.Get(() => Format(j), e => throw new ArrayNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given <see cref="IArray3D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Format<T>(in IArray3D<T> i)
    {
        Throw.IfNull(i, nameof(i));
        T[][][] j = i.ToArray();
        return Try.Get(() => Format(j), e => throw new ArrayNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given <see cref="IMatrix2D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    public static T[][] Format<T>(in IMatrix2D<T> i)
    {
        Throw.IfNull(i, nameof(i));
        T[][] j = i.ToArray();
        return Try.Get(() => Format(j), e => throw new MatrixNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given <see cref="IMatrix3D{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Format<T>(in IMatrix3D<T> i)
    {
        Throw.IfNull(i, nameof(i));
        T[][][] j = i.ToArray();
        return Try.Get(() => Format(j), e => throw new MatrixNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2D"/> from given <see cref="IVector{}"/>.
    /// </summary>
    /// <remarks>
    /// If <b>repeat</b> = <see cref="IVector.Length"/>, <see cref="IMatrix"/> = <see cref="Matrix2DProperty.Square"/>.
    /// <para><b><see cref="VectorType.X"/> = <see cref="IMatrix"/>[<see cref="IVector.Length"/>, 1]</b> (<b>repeat</b> = 3)</para>
    /// <see cref="IVector"/>[1] ⇒ <see cref="IMatrix"/>[1, 1, 1]<br/>
    /// <see cref="IVector"/>[2] ⇒ <see cref="IMatrix"/>[2, 2, 2]<br/>
    /// <see cref="IVector"/>[3] ⇒ <see cref="IMatrix"/>[3, 3, 3]
    /// <para><b><see cref="VectorType.Y"/> = <see cref="IMatrix"/>[1, <see cref="IVector.Length"/>]</b> (<b>repeat</b> = 3)</para>
    /// <see cref="IVector"/>[1, 2, 3]<br/>
    /// ⇒<br/>
    /// <see cref="IMatrix"/>[1, 2, 3]<br/>
    /// <see cref="IMatrix"/>[1, 2, 3]<br/>
    /// <see cref="IMatrix"/>[1, 2, 3]<br/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorNotImplemented"/>
    public static T[][] Format<T>(in IVector<T> i, int repeat = 1)
    {
        Throw.IfNull(i, nameof(i));
        T[] j = i.ToArray(); var k = i.Type;
        return Try.Get(() => Format(j, new(repeat, (Axis2)(int)k)), e => throw new VectorNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get <see cref="IMatrix2D{}"/> as <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// <para><b>X-N1</b></para>
    /// <para>[0.1,0.1,0.1],[1.0,1.0,1.0]...</para>
    /// <para><b>Y-N2</b></para>
    /// <para>[0.10,1.01,0.10]</para>
    /// <para>[1.01,0.10,1.01]</para>
    /// <para>...</para>
    /// <para>See <see cref="StringFormatX"/> and <see cref="StringFormatY"/>.</para>
    /// </remarks>
    /// <inheritdoc cref="object.ToString"/>
    [NotTested]
    public static string ToString<T>(IMatrix2D<T> matrix, string format, IFormatProvider provider)
    {
        Throw.IfNull(matrix, nameof(matrix));

        var horizontal = true;

        if (format.StartsWith(StringFormatX))
        {
            format = format[2..];
            horizontal = true;
        }
        else if (format.StartsWith(StringFormatY))
        {
            format = format[2..];
            horizontal = false;
        }

        var xText = new StringBuilder();
        var yText = new StringBuilder();

        /// Horizontal representation (with delimiter)
        if (horizontal)
        {
            for (int row = 0, rows = matrix.Rows; row < rows; row++)
            {
                xText.Clear();
                for (int column = 0, columns = matrix.Columns; column < columns; column++)
                {
                    var v = matrix[row, column];
                    var value = v is IFormattable f ? f.ToString(format, provider) : v.ToString();

                    xText.Append(value);
                    if (column < columns - 1)
                        xText.Append(StringFormatXDelimiterColumn);
                }

                _ = yText.AppendFormat(provider, StringFormatXRowComposite, xText);

                /// Horizontal representation (with delimiter)
                if (horizontal)
                {
                    if (row < rows - 1)
                        _ = yText.Append(StringFormatXDelimiterRow);
                }
                /// Vertical representation (with no padding)
                else
                {
                    _ = yText.Append('\n'); /// Inaccessible and undesirable!
                }
            }
        }
        /// Vertical representation (with padding)
        else
        {
            var longest = matrix.MaximumString(format, provider);
            var longestString = longest.B;

            int aLeft = 0, aRight = 0;

            /// Example, -123,456.789
            if (longestString.Contains('.'))
            {
                /// left = -123,456
                aLeft = longestString.Before(".").Length;

                /// right = 789
                aRight = longestString.After(".").Length;
            }
            else
            {
                aLeft = longestString.Length;
                aRight = 0;
            }

            for (int row = 0, rows = matrix.Rows; row < rows; row++)
            {
                xText.Clear();
                for (int column = 0, columns = matrix.Columns; column < columns; column++)
                {
                    var v = matrix[row, column];
                    var value = v is IFormattable f ? f.ToString(format, provider) : v.ToString();

                    int bLeft = 0, bRight = 0;

                    /// 123,456.0
                    if (value.Contains('.'))
                    {
                        bLeft = value.Before(".").Length;
                        bRight = value.After(".").Length;
                    }
                    /// 123,456
                    else
                    {
                        bLeft = value.Length;
                        bRight = 0;
                    }

                    var padLeft = aLeft - bLeft;
                    var padRight = aRight - bRight;

                    /// (-)
                    if (value.StartsWith('-'))
                    {
                        _ = xText.Append('-');
                        padLeft--;

                        value = value[1..];
                    }

                    _ = xText.Append(value.PadLeft(padLeft, StringFormatYPad).PadRight(padRight, StringFormatYPad));

                    if (column < columns - 1)
                        _ = xText.Append(StringFormatYSpace);
                }

                _ = yText.AppendFormat(provider, StringFormatYRowComposite, xText);
                if (row < rows - 1)
                    _ = yText.AppendLine(string.Empty);
            }
        }

        return yText.ToString();
    }
}
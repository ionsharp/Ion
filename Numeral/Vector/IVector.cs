using Ion.Collect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ion.Numeral;

/// <summary>
/// A 1-dimensional set of values with magnitude and direction.
/// </summary>
public interface IVector : IArray1D, IArray1DRank
{
    public const string Description = "A 1-dimensional set of values with magnitude and direction.";

    /// <see cref="Region.Field"/>
    #region

    /// <inheritdoc cref="VectorType.X"/>
    public const VectorType DefaultType = VectorType.X;

    public const int LengthMinimum = 1;

    /// <summary>The horizontal string format.</summary>
    public const string StringFormatX = "[0]";

    /// <summary>The horizontal string format delimiter.</summary>
    public const string StringFormatXDelimiter = ", ";

    /// <summary>A prefix that indicates a horizontal string representation.</summary>
    /// <remarks>
    /// <para><b>X-</b></para>
    /// <para>[0,1,0,1,0]...</para>
    /// </remarks>
    public const string StringFormatXPrefix = "X-";

    /// <summary>The vertical string format.</summary>
    public const string StringFormatY = "[0]";

    /// <summary>The vertical string format delimiter.</summary>
    public const string StringFormatYDelimiter = "\n";

    /// <summary>A prefix that indicates a vertical string representation.</summary>
    /// <remarks>
    /// <para><b>Y-</b></para>
    /// <para>[0]</para>
    /// <para>[1]</para>
    /// <para>[0]</para>
    /// <para>...</para>
    /// </remarks>
    public const string StringFormatYPrefix = "Y-";

    #endregion

    int IArray.Length => Length;

    new public int Length { get; }

    /// <inheritdoc cref="VectorType"/>
    public VectorType Type { get; }

    object IArray.this[int i] => this[i];

    object IArray1D.this[int i] => this[i];
    
    int IArray1D.XLength => Length;

    /// <see cref="Region.Method"/>
    #region

    /// <summary>
    /// Get new instance compatible with <see cref="IVector"/> from given <see cref="IEnumerable{}"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="IEnumerable{}"/> ≡ <see cref="IVector.DefaultType"/> ≡ <see cref="VectorType.X"/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EnumerableEmpty"/>
    public static (T[] Value, VectorType Type) Format<T>(VectorType type, in IEnumerable<T> i)
        => Format(i, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector"/> from given <see cref="IEnumerable{}"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="IEnumerable{}"/> ≡ <see cref="IVector.DefaultType"/> ≡ <see cref="VectorType.X"/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EnumerableEmpty"/>
    public static (T[] Value, VectorType Type) Format<T>(in IEnumerable<T> i, VectorType type)
    {
        Throw.IfNull(i, nameof(i));

        var result = i.ToArray();
        Throw.If<EnumerableEmpty>(result.Length == 0, nameof(i));

        return (result, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector"/> from given 1-dimensional <see cref="Array"/>.
    /// </summary>
    /// <remarks>
    /// 1-dimensional <see cref="Array"/> ≡ <see cref="IVector.DefaultType"/> ≡ <see cref="VectorType.X"/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EnumerableEmpty"/>
    public static (T[] Value, VectorType Type) Format<T>(params T[] i)
        => Format(i, IVector.DefaultType);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector"/> from given 2-dimensional <see cref="Array"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayJaggedUnexpectedDimensions"/>
    /// <exception cref="ArrayJaggedZeroColumns"/>
    /// <exception cref="ArrayJaggedZeroRows"/>
    public static (T[] Value, VectorType Type) Format<T>(in T[][] i, VectorType? newType)
    {
        Throw.IfNull(i, nameof(i));

        T[] result = null;

        var rows = i.Length;

        /// [0, ?]
        Throw.IfEqual<ArrayJaggedZeroRows>(rows, 0, nameof(i));

        VectorType type = default;

        /// [1, ?]
        if (rows == 1)
        {
            /// [1, 0]
            Throw.IfEqual<ArrayJaggedZeroColumns>(i[0].Length, 0, nameof(i));
            type = VectorType.Y;

            /// [1, 1]
            if (i[0].Length == 1)
            {
                type = VectorType.X;
                return (i[0], type);
            }
            /// [1, n] where n > 1
            i[0].ForEach((j, k) => result[j] = k);
        }
        /// [n, ?] where n > 1
        else
        {
            type = VectorType.X;

            result = new T[rows];
            for (int y = 0; y < rows; y++)
            {
                var row = i[y];
                /// [n, 0]
                Throw.IfEqual<ArrayJaggedZeroColumns>(row.Length, 0, nameof(i));

                /// [n, 1]
                if (row.Length == 1)
                    result[y] = i[y][0];

                /// [n, m] where n > 1 and m > 1
                Throw.If<ArrayJaggedUnexpectedDimensions>(row.Length > 1, nameof(i));
            }
        }
        return (result, newType ?? type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector"/> from given <see cref="IArray1D{}"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="IArray1D{}"/> ≡ <see cref="IVector.DefaultType"/> ≡ <see cref="VectorType.X"/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayNotImplemented"/>
    public static (T[] Value, VectorType Type) Format<T>(in IArray1D<T> i, VectorType type)
    {
        Throw.IfNull(i, nameof(i));

        var _i = i.ToArray();
        return (Try.Get(() => Format(_i), e => throw new ArrayNotImplemented(nameof(i), e)).Value, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector"/> from given <see cref="IMatrix{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MatrixNotImplemented"/>
    /// <remarks>
    /// <para><b><see cref="IMatrix"/>[<see cref="Length"/>, 1] = <see cref="VectorType.X"/></b></para>
    /// <see cref="IMatrix"/>[1] ⇒ <see cref="IVector"/>[1]<br/>
    /// <see cref="IMatrix"/>[2] ⇒ <see cref="IVector"/>[2]<br/>
    /// <see cref="IMatrix"/>[3] ⇒ <see cref="IVector"/>[3]
    /// <para><b><see cref="IMatrix"/>[1, <see cref="Length"/>] = <see cref="VectorType.Y"/></b></para>
    /// <see cref="IMatrix"/>[1, 2, 3] ⇒ <see cref="IVector"/>[1, 2, 3]
    /// </remarks>
    public static (T[] Value, VectorType Type) Format<T>(in IMatrix<T> i, VectorType? type)
    {
        Throw.IfNull(i, nameof(i));

        var _i = i.ToArray();
        return Try.Get(() => Format(_i, type), e => throw new MatrixNotImplemented(nameof(i), e));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector"/> from given <see cref="IVector{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorNotImplemented"/>
    public static (T[] Value, VectorType Type) Format<T>(in IVector<T> i, VectorType? type)
    {
        Throw.IfNull(i, nameof(i));

        var _i = i.ToArray();
        return (Try.Get(() => Format(_i), e => throw new VectorNotImplemented(nameof(i), e)).Value, type ?? i.Type);
    }

    /// <summary>
    /// Invert given <see cref="VectorType"/>.
    /// </summary>
    public static VectorType Invert(VectorType type) => (VectorType)(type == 0 ? 1 : 0);

    /// <summary>
    /// Get if given <b>index</b> is valid based on given <b>length</b>.
    /// </summary>
    public static bool IsValidIndex(int index, int length) => length < 0 ? false : index >= 0 && index < length;

    /// <summary>
    /// Get <see cref="IVector{}"/> as <see cref="string"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>X-N1</b></para>
    /// <para>[0.1,0.1,0.1]</para>
    /// <para><b>Y-N2</b></para>
    /// <para>[0.10]</para>
    /// <para>[1.01]</para>
    /// <para>[0.10]</para>
    /// <para>...</para>
    /// <para>See <see cref="StringFormatX"/> and <see cref="StringFormatY"/>.</para>
    /// </remarks>
    /// <inheritdoc cref="object.ToString"/>
    [NotTested]
    public static string ToString<T>(IVector<T> input, string format, IFormatProvider provider)
    {
        var result = new StringBuilder();

        var horizontal = false;

        if (format.StartsWith(StringFormatXPrefix))
        {
            format = format[2..];
            horizontal = true;
        }
        else if (format.StartsWith(StringFormatYPrefix))
        {
            format = format[2..];
            horizontal = false;
        }

        if (horizontal)
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (input is IVector j)
                {
                    _ = result.Append(j[i] is IFormattable f ? f.ToString(format, provider) : $"{j[i]}");
                    if (i < input.Length - 1)
                        _ = result.Append(StringFormatXDelimiter);
                }
            }
            return StringFormatX.F(result);
        }
        else
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (input is IVector j)
                {
                    _ = result.Append(StringFormatY.F(j[i] is IFormattable f ? f.ToString(format, provider) : $"{j[i]}"));
                    if (i < input.Length - 1)
                        _ = result.Append(StringFormatYDelimiter);
                }
            }
            return result.ToString();
        }
    }

    #endregion
}
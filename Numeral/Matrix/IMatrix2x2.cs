namespace Ion.Numeral;

/// <summary>
/// A 2-dimensional set of values arranged in 2 columns and rows.
/// </summary>
public interface IMatrix2x2 : IMatrixFixed, IMatrixUniform
{
    new public const string Description = "A 2-dimensional set of values arranged in 2 columns and rows.";

    public const int LengthDimensional = 2;

    int IArray.Length => LengthDimensional * LengthDimensional;

    (int Y, int X) IArray2D.Length => (LengthDimensional, LengthDimensional);
    
    int IArray1D.XLength => LengthDimensional;

    int IArray2D.YLength => LengthDimensional;

    int IMatrix.Columns => LengthDimensional;

    int IMatrix.Rows => LengthDimensional;

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2x2"/> with given <b>xyz</b> and <see cref="Matrix2x2Fill"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <returns>
    /// [x, x]<br/>
    /// [x, x]
    /// </returns>
    public static T[][] Format<T>(in T x) => Format(x, x, x, default);

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2x2"/> with given <b>r0c0</b> ...
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <returns>
    /// [r0c0, r0c1]<br/>
    /// [r1c0, r1c1]
    /// </returns>
    public static T[][] Format<T>(in T r0c0, in T r0c1, in T r1c0, in T r1c1)
    {
        Throw.IfNull(r0c0, nameof(r0c0));
        Throw.IfNull(r0c1, nameof(r0c1));

        Throw.IfNull(r1c0, nameof(r1c0));
        Throw.IfNull(r1c1, nameof(r1c1));

        return
        [
            [r0c0, r0c1],
            [r1c0, r1c1],
        ];
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2x2"/> from given <see cref="IMatrix2x2{}"/>.
    /// </summary>
    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix2D{T})"/>
    public static T[][] Format<T>(in IMatrix2x2<T> matrix) => IMatrix.Format(matrix);

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix2x2"/>  with given <see cref="IVector3{}"/> rows.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <returns>
    /// [r0.X, r0.Y]<br/>
    /// [r1.X, r1.Y]
    /// </returns>
    public static T[][] Format<T>(in IVector2<T> r0, in IVector2<T> r1)
    {
        Throw.IfNull(r0, nameof(r0));
        Throw.IfNull(r1, nameof(r1));
        return Format(r0.X, r0.Y, r1.X, r1.Y);
    }
}

/// <inheritdoc/>
public interface IMatrix2x2<T> : IMatrix2x2, IMatrixFixed<T>, IMatrixUniform<T>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => LengthDimensional * LengthDimensional;
}

public interface IMatrix2x2<TSelf, TValue> : IMatrix2x2<TValue>, IMatrix<TSelf, TValue> where TSelf : IMatrix2x2<TSelf, TValue>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => LengthDimensional * LengthDimensional;
}
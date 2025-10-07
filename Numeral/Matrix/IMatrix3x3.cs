namespace Ion.Numeral;

/// <summary>
/// A 2-dimensional set of values arranged in 3 columns and rows.
/// </summary>
public interface IMatrix3x3 : IMatrixFixed, IMatrixUniform
{
    public const Matrix3x3Fill DefaultFill = Matrix3x3Fill.Right;

    new public const string Description = "A 2-dimensional set of values arranged in 3 columns and rows.";

    public const int LengthDimensional = 3;

    int IArray.Length => LengthDimensional * LengthDimensional;

    int IArray1D.XLength => LengthDimensional;

    (int Y, int X) IArray2D.Length => (LengthDimensional, LengthDimensional);

    int IArray2D.YLength => LengthDimensional;

    int IMatrix.Columns => LengthDimensional;

    int IMatrix.Rows => LengthDimensional;

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3x3"/> with given <b>xyz</b> and <see cref="Matrix3x3Fill"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <returns>
    /// [x, x, x]<br/>
    /// [x, x, x]<br/>
    /// [x, x, x]
    /// </returns>
    public static T[][] Format<T>(in T x) => Format(x, x, x, default);

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3x3"/> with given <b>x</b>, <b>y</b>, <b>z</b>, and <see cref="Matrix3x3Fill"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <returns>
    /// <para><b><see cref="Matrix3x3Fill.Default"/> = <see cref="Matrix3x3Fill.Right"/></b></para>
    /// [x, x, x]<br/>
    /// [y, y, y]<br/>
    /// [z, z, z]
    /// </returns>
    public static T[][] Format<T>(in T x, in T y, in T z, Matrix3x3Fill fill = Matrix3x3Fill.Default)
    {
        Throw.IfNull(x, nameof(x));
        Throw.IfNull(y, nameof(y));
        Throw.IfNull(z, nameof(z));

        fill = fill == Matrix3x3Fill.Default ? IMatrix3x3.DefaultFill : fill;

        var i = default(T);
        return fill switch
        {
            Matrix3x3Fill.Alternate =>
            [
                [z, y, z],
                [y, x, y],
                [z, y, z],
            ],
            Matrix3x3Fill.CenterX =>
            [
                [i, i, i],
                [x, y, z],
                [i, i, i],
            ],
            Matrix3x3Fill.CenterY =>
            [
                [i, x, i],
                [i, y, i],
                [i, z, i],
            ],
            Matrix3x3Fill.CenterXY =>
            [
                [i, x, i],
                [x, y, z],
                [i, z, i],
            ],
            Matrix3x3Fill.DiagonalLeft =>
            [
                [i, i, x],
                [i, y, i],
                [z, i, i],
            ],
            Matrix3x3Fill.DiagonalLeftRight =>
            [
                [x, i, x],
                [i, y, i],
                [z, i, z],
            ],
            Matrix3x3Fill.DiagonalRight =>
            [
                [x, i, i],
                [i, y, i],
                [i, i, z],
            ],
            Matrix3x3Fill.Down =>
            [
                [x, y, z],
                [x, y, z],
                [x, y, z],
            ],
            Matrix3x3Fill.Right =>
            [
                [x, x, x],
                [y, y, y],
                [z, z, z],
            ],
            Matrix3x3Fill.TriagonalLower =>
            [
                [x, i, i],
                [y, x, i],
                [z, y, x],
            ],
            Matrix3x3Fill.TriagonalUpper =>
            [
                [x, y, z],
                [i, x, y],
                [i, i, x],
            ],
        };
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3x3"/> with given <b>r0c0</b> ...
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <returns>
    /// [r0c0, r0c1, r0c2]<br/>
    /// [r1c0, r1c1, r1c2]<br/>
    /// [r2c0, r2c1, r2c2]
    /// </returns>
    public static T[][] Format<T>(in T r0c0, in T r0c1, in T r0c2, in T r1c0, in T r1c1, in T r1c2, in T r2c0, in T r2c1, in T r2c2)
    {
        Throw.IfNull(r0c0, nameof(r0c0));
        Throw.IfNull(r0c1, nameof(r0c1));
        Throw.IfNull(r0c2, nameof(r0c2));

        Throw.IfNull(r1c0, nameof(r1c0));
        Throw.IfNull(r1c1, nameof(r1c1));
        Throw.IfNull(r1c2, nameof(r1c2));

        Throw.IfNull(r2c0, nameof(r2c0));
        Throw.IfNull(r2c1, nameof(r2c1));
        Throw.IfNull(r2c2, nameof(r2c2));

        return
        [
            [r0c0, r0c1, r0c2],
            [r1c0, r1c1, r1c2],
            [r2c0, r2c1, r2c2],
        ];
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3x3"/> from given <see cref="IMatrix3x3{}"/>.
    /// </summary>
    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix2D{T})"/>
    public static T[][] Format<T>(in IMatrix3x3<T> matrix) => IMatrix.Format(matrix);

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3x3"/>  with given <see cref="IVector3{}"/> row and <see cref="Matrix3x3Fill"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Format<T>(in IVector3<T> abc, Matrix3x3Fill fill = Matrix3x3Fill.Default)
    {
        Throw.IfNull(abc, nameof(abc));
        return Format(abc.X, abc.Y, abc.Z, fill);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IMatrix3x3"/>  with given <see cref="IVector3{}"/> rows.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <returns>
    /// [r0.X, r0.Y, r0.Z]<br/>
    /// [r1.X, r1.Y, r1.Z]<br/>
    /// [r2.X, r2.Y, r2.Z]
    /// </returns>
    public static T[][] Format<T>(in IVector3<T> r0, in IVector3<T> r1, in IVector3<T> r2)
    {
        Throw.IfNull(r0, nameof(r0));
        Throw.IfNull(r1, nameof(r1));
        Throw.IfNull(r2, nameof(r2));
        return Format(r0.X, r0.Y, r0.Z, r1.X, r1.Y, r1.Z, r2.X, r2.Y, r2.Z);
    }
}

/// <inheritdoc/>
public interface IMatrix3x3<T> : IMatrix3x3, IMatrixFixed<T>, IMatrixUniform<T>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => LengthDimensional * LengthDimensional;
}

public interface IMatrix3x3<TSelf, TValue> : IMatrix3x3<TValue>, IMatrix<TSelf, TValue> where TSelf : IMatrix3x3<TSelf, TValue>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => LengthDimensional * LengthDimensional;
}
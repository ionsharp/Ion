using System;
using System.Numerics;

namespace Ion.Numeral;

/// <summary>
/// Extends <see cref="IMatrix2D"/>.
/// </summary>
[Extend(typeof(IMatrix2D))]
public static partial class XMatrix2D
{
    /// <summary>
    /// Get if <see cref="Matrix2DProperty.Square"/> (<see cref="IMatrix.Columns"/> = <see cref="IMatrix.Rows"/>).
    /// </summary>
    public static bool IsSquare(this IMatrix2D i) => i.XLength == i.YLength;
}

[Extend(typeof(IMatrix2D<,>))]
[Extend(typeof(ByteVector2), typeof(ByteVector3), typeof(ByteVector4))]
public static partial class XMatrix2D
{
    public static TSelf A<TSelf>(this IMatrix2D<TSelf, ByteVector4> i, byte a)
        where TSelf : IMatrix2D<TSelf, ByteVector4>
        => i.New(j => j.A(a));

    public static TSelf A<TSelf>(this IMatrix2D<TSelf, ByteVector4> i, Func<byte, byte> a)
        where TSelf : IMatrix2D<TSelf, ByteVector4>
        => i.New(j => j.A(a));

    public static TSelf R<TSelf>(this IMatrix2D<TSelf, ByteVector2> i, byte r)
        where TSelf : IMatrix2D<TSelf, ByteVector2>
        => i.New(j => j.R(r));

    public static TSelf R<TSelf>(this IMatrix2D<TSelf, ByteVector2> i, Func<byte, byte> r)
        where TSelf : IMatrix2D<TSelf, ByteVector2>
        => i.New(j => j.R(r));

    public static TSelf R<TSelf>(this IMatrix2D<TSelf, ByteVector3> i, byte r)
        where TSelf : IMatrix2D<TSelf, ByteVector3>
        => i.New(j => j.R(r));

    public static TSelf R<TSelf>(this IMatrix2D<TSelf, ByteVector3> i, Func<byte, byte> r)
        where TSelf : IMatrix2D<TSelf, ByteVector3>
        => i.New(j => j.R(r));

    public static TSelf R<TSelf>(this IMatrix2D<TSelf, ByteVector4> i, byte r)
        where TSelf : IMatrix2D<TSelf, ByteVector4>
        => i.New(j => j.R(r));

    public static TSelf R<TSelf>(this IMatrix2D<TSelf, ByteVector4> i, Func<byte, byte> r)
        where TSelf : IMatrix2D<TSelf, ByteVector4>
        => i.New(j => j.R(r));

    public static TSelf G<TSelf>(this IMatrix2D<TSelf, ByteVector2> i, byte g)
        where TSelf : IMatrix2D<TSelf, ByteVector2>
        => i.New(j => j.G(g));

    public static TSelf G<TSelf>(this IMatrix2D<TSelf, ByteVector2> i, Func<byte, byte> g)
        where TSelf : IMatrix2D<TSelf, ByteVector2>
        => i.New(j => j.G(g));

    public static TSelf G<TSelf>(this IMatrix2D<TSelf, ByteVector3> i, byte g)
        where TSelf : IMatrix2D<TSelf, ByteVector3>
        => i.New(j => j.G(g));

    public static TSelf G<TSelf>(this IMatrix2D<TSelf, ByteVector3> i, Func<byte, byte> g)
        where TSelf : IMatrix2D<TSelf, ByteVector3>
        => i.New(j => j.G(g));

    public static TSelf G<TSelf>(this IMatrix2D<TSelf, ByteVector4> i, byte g)
        where TSelf : IMatrix2D<TSelf, ByteVector4>
        => i.New(j => j.G(g));

    public static TSelf G<TSelf>(this IMatrix2D<TSelf, ByteVector4> i, Func<byte, byte> g)
        where TSelf : IMatrix2D<TSelf, ByteVector4>
        => i.New(j => j.G(g));

    public static TSelf B<TSelf>(this IMatrix2D<TSelf, ByteVector3> i, byte b)
        where TSelf : IMatrix2D<TSelf, ByteVector3>
        => i.New(j => j.B(b));

    public static TSelf B<TSelf>(this IMatrix2D<TSelf, ByteVector3> i, Func<byte, byte> b)
        where TSelf : IMatrix2D<TSelf, ByteVector3>
        => i.New(j => j.B(b));

    public static TSelf B<TSelf>(this IMatrix2D<TSelf, ByteVector4> i, byte b)
        where TSelf : IMatrix2D<TSelf, ByteVector4>
        => i.New(j => j.B(b));

    public static TSelf B<TSelf>(this IMatrix2D<TSelf, ByteVector4> i, Func<byte, byte> b)
        where TSelf : IMatrix2D<TSelf, ByteVector4>
        => i.New(j => j.B(b));
}

/// <see cref="INumber{}"/>

[Extend(typeof(IMatrix2D<>))]
public static partial class XMatrix
{
    [NotTested]
    public static Matrix<TValue> Invert<TValue>(this IMatrix2D<TValue> i, MatrixInverseFormula formula = MatrixInverseFormula.LU, bool standardize = false)
        where TValue : INumber<TValue> => new Matrix<TValue>(i).Invert(formula, standardize);
}

[Extend(typeof(IMatrix2D<>))]
[Extend(typeof(IMatrix2x2<>))]
public static partial class XMatrix
{
    [NotComplete]
    public static Matrix2x2<TValue> Invert<TValue>(this IMatrix2x2<TValue> i, MatrixInverseFormula formula = MatrixInverseFormula.LU, bool standardize = false)
        where TValue : INumber<TValue> => default; //new Matrix<TValue>(i).Invert(formula, standardize);
}

[Extend(typeof(IMatrix2D<>))]
[Extend(typeof(IMatrix3x3<>))]
public static partial class XMatrix
{
    [NotComplete]
    public static Matrix3x3<TValue> Invert<TValue>(this IMatrix3x3<TValue> i, MatrixInverseFormula formula = MatrixInverseFormula.LU, bool standardize = false)
        where TValue : INumber<TValue> => default; //new Matrix<TValue>(i).Invert(formula, standardize);
}

[Extend(typeof(IMatrix2D<,>))]
public static partial class XMatrix
{
    /// <summary>
    /// Get inverse using given <see cref="MatrixInverseFormula"/>.
    /// </summary>
    /// <exception cref="MatrixNoInverse"/>
    /// <exception cref="MatrixNotCompatible"/>
    /// <exception cref="MatrixNotStandardizable"/>
    public static TSelf Invert<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, MatrixInverseFormula formula, bool standardize)
        where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
    {
        return formula switch
        {
            MatrixInverseFormula.Analytic
                => MatrixInverse.InvertAnalytic(i),
            MatrixInverseFormula.Blockwise
                => MatrixInverse.InvertBlockwise(i),
            MatrixInverseFormula.Cayley
                => MatrixInverse.InvertCayley(i),
            MatrixInverseFormula.Cholesky
                => MatrixInverse.InvertCholesky(i),
            MatrixInverseFormula.Drazin
                => MatrixInverse.InvertDrazin(i),
            MatrixInverseFormula.Eigen
                => MatrixInverse.InvertEigen(i),
            MatrixInverseFormula.Gaussian
                => MatrixInverse.InvertGaussian(i),
            MatrixInverseFormula.LU
                => MatrixInverse.InvertLU(i),
            MatrixInverseFormula.Moore
                => MatrixInverse.InvertMoore(i),
            MatrixInverseFormula.Neumann
                => MatrixInverse.InvertNeumann(i),
            MatrixInverseFormula.Pseudo
                => MatrixInverse.InvertPseudo(i),
            MatrixInverseFormula.QR
                => default, //MatrixInverse.InvertQR(i),
            MatrixInverseFormula.Strassen
                => MatrixInverse.InvertStrassen(i)
        };
    }
}

[Extend(typeof(IMatrix2D<,>))]
[Extend(typeof(IMatrix2x2<>))]
public static partial class XMatrix2D
{
    /// <see cref="INumber{}"/>
    #region

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, Operator action, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Do(action, j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Divide(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Minus(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Modulo(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Multiply(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> multiple)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(multiple, nameof(multiple));
        return i.New((y, x, w) => w.Nearest(multiple[y, x]));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Plus(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Pow(j[y, x]));
    }

    #endregion

    /// <see cref="IFloatingPoint{}"/>
    #region

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.IaN(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.NaN(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Root(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix2x2<int> digits, MidpointRounding mode = default)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix2x2<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(digits, nameof(digits));
        return i.New((y, x, w) => w.Round(digits[y, x], mode));
    }

    #endregion
}

[Extend(typeof(IMatrix2D<,>))]
[Extend(typeof(IMatrix3x3<>))]
public static partial class XMatrix2D
{
    /// <see cref="INumber{}"/>
    #region

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, Operator action, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Do(action, j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Divide(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New(w => w);
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Modulo(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Multiply(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> multiple)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(multiple, nameof(multiple));
        return i.New((y, x, w) => w.Nearest(multiple[y, x]));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Plus(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Pow(j[y, x]));
    }

    #endregion

    /// <see cref="IFloatingPoint{}"/>
    #region

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.IaN(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.NaN(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Root(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix3x3<int> digits, MidpointRounding mode = default)
        where TSelf : IArray2D<TSelf, TValue>, IMatrix2D<TSelf, TValue>, IMatrix3x3<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(digits, nameof(digits));
        return i.New((y, x, w) => w.Round(digits[y, x], mode));
    }

    #endregion
}

[Extend(typeof(IMatrix2D<,>))]
[Extend(typeof(IMatrixUnfixed<>))]
public static partial class XMatrix
{
    /// <see cref="INumber{}"/>
    #region

    /// <inheritdoc cref="XNumber.Do{}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, Operator action, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Do(action, j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Divide{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Divide(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Minus{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Minus(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Modulo{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Modulo(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Multiply{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Multiply(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Nearest{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> multiple)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(multiple, nameof(multiple));
        return i.New((y, x, w) => w.Nearest(multiple[y, x]));
    }

    /// <inheritdoc cref="XNumber.Plus{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Plus(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Pow{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Pow(j[y, x]));
    }

    #endregion

    /// <see cref="IFloatingPoint{}"/>
    #region

    /// <inheritdoc cref="XNumber.IaN{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.IaN(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.NaN{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.NaN(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Root{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<TValue> j)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Root(j[y, x]));
    }

    /// <inheritdoc cref="XNumber.Round{}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i, IMatrix<int> digits, MidpointRounding mode = default)
        where TSelf : IMatrix2D<TSelf, TValue>, IMatrixUnfixed<TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(digits, nameof(digits));
        return i.New((y, x, w) => w.Round(digits[y, x], mode));
    }

    #endregion
}
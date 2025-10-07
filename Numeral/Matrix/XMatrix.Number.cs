using System;
using System.Numerics;

namespace Ion.Numeral;

[Extend(typeof(IMatrix<>))]
public static partial class XMatrix
{
    /// <see cref="return"/> = <see cref="T"/>
    #region

    /// <summary>Dot multiply by given <see cref="IMatrix{}"/> (from top left to bottom right).</summary>
    /// <remarks>
    /// <para>Both must have same <see cref="IMatrix.Columns"/> and <see cref="IMatrix.Rows"/>.</para>
    /// <para>
    /// [1a,2a,3a] x [1b,2b,3b] <br/>
    /// [4a,5a,6a] x [4b,5b,6b] = (1a x 1b) + (2a x 2b) + (3a x 3b) + (4a x 4b) ...<br/>
    /// [7a,8a,9a] x [7b,8b,9b] <br/>
    /// </para>
    /// </remarks>
    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static T Dot<T>(this IMatrix<T> i, IMatrix<T> j)
        where T : INumber<T>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, j.Columns, nameof(j));
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, j.Rows, nameof(j));
        return i.Aggregate((y, x, a, b) => a + (b * j[y, x]));
    }

    #endregion

    /// <see cref="return"/> = <see cref="Boolean"/>
    #region

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.Diagonal"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsDiagnoal<T>(this IMatrix2D<T> i, MatrixDiagonal side = MatrixDiagonal.Right)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsTriangularLower(side) && i.IsTriangularUpper(side);
    }

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.Identity"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsIdentity<T>(this IMatrix2D<T> i, MatrixDiagonal side = MatrixDiagonal.Right)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsDiagnoal(side) && i.GetDiagonal(side).All<T>(j => j == T.One);
    }

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.SkewSymmetric"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsSkewSymmetric<T>(this IMatrix2D<T> i)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsSquare() && Equals(i, i.Transpose().New(j => -j));
    }

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.Symmetric"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsSymmetric<T>(this IMatrix2D<T> i)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsSquare() && Equals(i, i.Transpose());
    }

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.Triagonal"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsTriangular<T>(this IMatrix2D<T> i, MatrixDiagonal side = MatrixDiagonal.Right)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsTriangularLower(side) || i.IsTriangularUpper(side);
    }

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.TriagonalLower"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsTriangularLower<T>(this IMatrix2D<T> i, MatrixDiagonal side = MatrixDiagonal.Right)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsSquare() && i.GetDiagonalUpper(side).All<T>(j => j == T.Zero);
    }

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.TriagonalUpper"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsTriangularUpper<T>(this IMatrix2D<T> i, MatrixDiagonal side = MatrixDiagonal.Right)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsSquare() && i.GetDiagonalLower(side).All<T>(j => j == T.Zero);
    }

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{}"/>
    #region

    /// <inheritdoc cref="Convert.ToByte(object?)"/>
    public static Matrix<byte> ToByte<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToByte());

    /// <inheritdoc cref="Convert.ToDecimal(object?)"/>
    public static Matrix<decimal> ToDecimal<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToDecimal());

    /// <inheritdoc cref="Convert.ToDouble(object?)"/>
    public static Matrix<double> ToDouble<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToDouble());

    /// <inheritdoc cref="Convert.ToInt16(object?)"/>
    public static Matrix<short> ToInt16<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToInt16());

    /// <inheritdoc cref="Convert.ToInt32(object?)"/>
    public static Matrix<int> ToInt32<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToInt32());

    /// <inheritdoc cref="Convert.ToInt64(object?)"/>
    public static Matrix<long> ToInt64<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToInt64());

    /// <inheritdoc cref="Convert.ToByte(object?)"/>
    public static Matrix<sbyte> ToSByte<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToSByte());

    /// <inheritdoc cref="Convert.ToSingle(object?)"/>
    public static Matrix<float> ToSingle<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToSingle());

    /// <inheritdoc cref="Convert.ToUInt16(object?)"/>
    public static Matrix<ushort> ToUInt16<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToUInt16());

    /// <inheritdoc cref="Convert.ToUInt32(object?)"/>
    public static Matrix<uint> ToUInt32<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToUInt32());

    /// <inheritdoc cref="Convert.ToUInt64(object?)"/>
    public static Matrix<ulong> ToUInt64<T>(this IMatrix<T> i)
        where T : INumber<T>
        => i.NewType(j => j.ToUInt64());

    #endregion

    /// <see cref="return"/> = <see cref="IMatrix{T}"/>
    #region

    /// <summary>
    /// Get adjugate.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// First, calculate the minor for each element of the matrix by removing the row and column containing that element and finding the determinant of the resulting submatrix. 
    /// Next, multiply each minor by (−1)^(i + j), where i and j are the row and column indices of the element, to get the cofactor matrix.
    /// Finally, take the transpose of the cofactor matrix to obtain the adjugate matrix.
    /// </remarks>
    [Recursive(MatrixDeterminantFormula.CofactorExpansion)]
    public static Matrix<T> GetAdjugate<T>(this IMatrix<T> matrix, MatrixDeterminantFormula formula = MatrixDeterminantFormula.Leibniz)
        where T : IFloatingPoint<T>, INumber<T>
    {
        int n = matrix.Rows;
        var cofactorMatrix = matrix.GetCofactor(formula);

        T[,] result = new T[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                result[i, j] = cofactorMatrix[j, i];
        }
        return new Matrix<T>(result.As());
    }

    [Recursive(MatrixDeterminantFormula.CofactorExpansion)]
    private static T GetCofactor<T>(this IMatrix<T> matrix, int row, int col, MatrixDeterminantFormula formula)
        where T : IFloatingPoint<T>, INumber<T>
    {
        int n = matrix.Rows;

        T[,] result = new T[n - 1, n - 1];
        int p = 0, q = 0;
        for (int i = 0; i < n; i++)
        {
            if (i == row) continue;
            q = 0;
            for (int j = 0; j < n; j++)
            {
                if (j == col) continue;
                result[p, q] = matrix[i, j];
                q++;
            }
            p++;
        }
        return Math.Pow(-1, row + col).Create<T>() * new Matrix<T>(result.As()).GetDeterminant(formula);
    }

    /// <summary>
    /// Get cofactor.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [Recursive(MatrixDeterminantFormula.CofactorExpansion)]
    public static Matrix<T> GetCofactor<T>(this IMatrix<T> matrix, MatrixDeterminantFormula formula = MatrixDeterminantFormula.Leibniz)
        where T : IFloatingPoint<T>, INumber<T>
    {
        int n = matrix.Rows;
        T[,] result = new T[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                result[i, j] = GetCofactor(matrix, i, j, formula);
        }
        return new Matrix<T>(result.As());
    }

    /// <summary>
    /// Get minor at given <b>row</b> and <b>column</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix<T> GetMinor<T>(this IMatrix<T> i, int row, int column)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));

        var minor = new T[i.Rows - 1, i.Columns - 1];
        int ySub = 0, xSub = 0;
        for (int y = 0; y < i.Rows; y++)
        {
            if (y == row) continue;

            xSub = 0;
            for (int x = 0; x < i.Columns; x++)
            {
                if (x == column) continue;

                minor[ySub, xSub] = i[y, x];
                xSub++;
            }
            ySub++;
        }
        return new Matrix<T>(minor.As());
    }

    #endregion

    /// <see cref="return"/> = <see cref="IVector{}"/>
    #region

    /// <inheritdoc cref="XNumber.Do{TNumber}(TNumber, Operator, TNumber)"/>
    /// <exception cref="MatrixVectorMismatch"/>
    public static Vector<T> Do<T>(this IMatrix<T> a, Operator action, IVector<T> b) where T : INumber<T>
    {
        Throw.IfNotEqual<MatrixVectorMismatch>(a.Columns, b.Length, nameof(b));

        var result = new T[a.Rows];
        Array2D.Do(a.Rows, b.Length, (y, x) => result[y] += a[y, x].Do(action, b[x]));
        return new(result);
    }

    /// <inheritdoc cref="XNumber.Do{TNumber}(TNumber, Operator, TNumber)"/>
    /// <exception cref="MatrixVectorMismatch"/>
    public static Vector2<T> Do<T>(this IMatrix<T> a, Operator action, IVector2<T> b) where T : INumber<T>
    {
        Throw.IfNotEqual<MatrixVectorMismatch>(a.Columns, IVector2.Length, nameof(b));

        var result = new T[a.Rows];
        Array2D.Do(a.Rows, IVector2.Length, (y, x) => result[y] += a[y, x].Do(action, b[x]));
        return new(result[0], result[1]);
    }

    /// <inheritdoc cref="XNumber.Do{TNumber}(TNumber, Operator, TNumber)"/>
    /// <exception cref="MatrixVectorMismatch"/>
    public static Vector3<T> Do<T>(this IMatrix<T> a, Operator action, IVector3<T> b) where T : INumber<T>
    {
        Throw.IfNotEqual<MatrixVectorMismatch>(a.Columns, IVector3.Length, nameof(b));

        var result = new T[a.Rows];
        Array2D.Do(a.Rows, IVector3.Length, (y, x) => result[y] += a[y, x].Do(action, b[x]));
        return new(result[0], result[1], result[2]);
    }

    /// <inheritdoc cref="XNumber.Do{TNumber}(TNumber, Operator, TNumber)"/>
    /// <exception cref="MatrixVectorMismatch"/>
    public static Vector4<T> Do<T>(this IMatrix<T> a, Operator action, IVector4<T> b) where T : INumber<T>
    {
        Throw.IfNotEqual<MatrixVectorMismatch>(a.Columns, IVector4.Length, nameof(b));

        var result = new T[a.Rows];
        Array2D.Do(a.Rows, IVector4.Length, (y, x) => result[y] += a[y, x].Do(action, b[x]));
        return new(result[0], result[1], result[2], result[3]);
    }

    #endregion

    /// <see cref="IFloatingPoint{}"/> | <see cref="return"/> = <see cref="T"/>
    #region

    /// <summary>
    /// Get determinant using given <see cref="MatrixDeterminantFormula"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [Recursive(MatrixDeterminantFormula.CofactorExpansion)]
    public static T GetDeterminant<T>(this IMatrix<T> i, MatrixDeterminantFormula formula = MatrixDeterminantFormula.Leibniz)
        where T : IFloatingPoint<T>, INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return formula switch
        {
            MatrixDeterminantFormula.CholeskyDecomposition
                => MatrixDeterminant<T>.CholeskyDecomposition(i),
            MatrixDeterminantFormula.CofactorExpansion
                => MatrixDeterminant<T>.CofactorExpansion(i),
            MatrixDeterminantFormula.ColumnOperations
                => MatrixDeterminant<T>.ColumnOperations(i),
            MatrixDeterminantFormula.EigenvalueDecomposition
                => MatrixDeterminant<T>.EigenvalueDecomposition(i),
            MatrixDeterminantFormula.Leibniz
                => MatrixDeterminant<T>.Leibniz(i),
            MatrixDeterminantFormula.LUDecomposition
                => MatrixDeterminant<T>.LUDecomposition(i),
            MatrixDeterminantFormula.RowOperations
                => MatrixDeterminant<T>.RowOperations(i),
            MatrixDeterminantFormula.SarrusRule
                => MatrixDeterminant<T>.SarrusRule(i),
        };
    }

    #endregion

    /// <see cref="IFloatingPoint{}"/> | <see cref="return"/> = <see cref="Boolean"/>
    #region

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.Invertible"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsInvertible<T>(this IMatrix2D<T> i, MatrixInverseFormula formula = MatrixInverseFormula.LU)
        where T : IFloatingPoint<T>, INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return Try.Do(() => i.Invert(formula));
    }

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.Orthogonal"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsOrthogonal<T>(this IMatrix2D<T> i, MatrixInverseFormula formula = MatrixInverseFormula.LU)
        where T : IFloatingPoint<T>, INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsSquare() && Equals(i.Transpose(), i.Invert(formula));
    }

    /// <summary>
    /// Get if <see cref="Matrix2DProperty.Singular"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool IsSingular<T>(this IMatrix2D<T> i, MatrixDeterminantFormula formula = MatrixDeterminantFormula.Leibniz)
        where T : IFloatingPoint<T>, INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        return i.IsSquare() && i.GetDeterminant(formula) == T.Zero;
    }

    #endregion

    /// <see cref="IFloatingPoint{}"/> | <see cref="ISignedNumber{}"/>
    #region

    /// <summary>Get distance from given value (from top left to bottom right).</summary>
    /// <inheritdoc cref="Distance{}(IMatrix{}, IMatrix{})"/>
    public static Value Distance<Value>(this IMatrix<Value> i, Value j)
        where Value : IFloatingPoint<Value>, INumber<Value>, ISignedNumber<Value>
        => i.Distance(new Matrix<Value>(i.Rows, i.Columns, j));

    /// <summary>Get distance from given <see cref="Array"/> (from top left to bottom right).</summary>
    /// <inheritdoc cref="Distance{}(IMatrix{}, IMatrix{})"/>
    public static Value Distance<Value>(this IMatrix<Value> i, Value[][] j)
        where Value : IFloatingPoint<Value>, INumber<Value>, ISignedNumber<Value>
        => i.Distance(new Matrix<Value>(j));

    /// <summary>Get distance from given <see cref="IMatrix"/> (from top left to bottom right).</summary>
    /// <remarks>
    /// <para>Both must have same <see cref="IMatrix.Columns"/> and <see cref="IMatrix.Rows"/>.</para>
    /// <para>
    /// [1a,2a,3a] x [1b,2b,3b] <br/>
    /// [4a,5a,6a] x [4b,5b,6b] = √(|1a - 1b|^2 + |2a - 2b|^2 + |3a - 3b|^2 + |4a - 4b|^2 ...)<br/>
    /// [7a,8a,9a] x [7b,8b,9b] <br/>
    /// </para>
    /// </remarks>
    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static Value Distance<Value>(this IMatrix<Value> i, IMatrix<Value> j)
        where Value : IFloatingPoint<Value>, INumber<Value>, ISignedNumber<Value>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, j.Columns, nameof(j));
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, j.Rows, nameof(j));
        return i.Aggregate((y, x, a, b) => a + (b - j[y, x]).Abs().Pow2()).Root2();
    }

    #endregion

    /// <see cref="IMinMaxValue{}"/>
    #region

    /// <inheritdoc cref="XNumber.Denormalize{TNumber}(double)"/>
    public static Matrix<Value> Denormalize<Value>(this IMatrix<Double1> i) where Value : IMinMaxValue<Value>, INumber<Value>
        => i.Denormalize(Value.MinValue, Value.MaxValue);

    /// <inheritdoc cref="XNumber.Denormalize{TNumber}(Double1, TNumber)"/>
    public static Matrix<Value> Denormalize<Value>(this IMatrix<Double1> i, Value maximum) where Value : IMinMaxValue<Value>, INumber<Value>
        => i.Denormalize(Value.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{TNumber}(Double1, TNumber, TNumber)"/>
    public static Matrix<Value> Denormalize<Value>(this IMatrix<Double1> i, Value minimum, Value maximum) where Value : IMinMaxValue<Value>, INumber<Value>
    {
        var result = new Value[i.Rows][];
        var range = new Range<double>(Convert.ToDouble(minimum), Convert.ToDouble(maximum));
        Array1D.Do(i.Rows, row =>
        {
            result[row] = new Value[i.Columns];
            Array1D.Do(i.Columns, column => result[row][column] = Value.CreateSaturating(range.Denormalize(Convert.ToDouble(i[row, column]))));
        });
        return new Matrix<Value>(result);
    }

    /// <inheritdoc cref="XNumber.Denormalize{TNumber}(Double1, TNumber)"/>
    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static Matrix<Value> Denormalize<Value>(this IMatrix<Double1> i, IMatrix<Value> maximum) where Value : IMinMaxValue<Value>, INumber<Value>
        => i.Denormalize(new Matrix<Value>(i.Rows, i.Columns, Value.MinValue), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{TNumber}(Double1, TNumber, TNumber)"/>
    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static Matrix<Value> Denormalize<Value>(this IMatrix<Double1> i, IMatrix<Value> minimum, IMatrix<Value> maximum) where Value : IMinMaxValue<Value>, INumber<Value>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, minimum.Columns, nameof(minimum));
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, maximum.Columns, nameof(maximum));
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, minimum.Rows, nameof(minimum));
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, maximum.Rows, nameof(maximum));

        var result = new Value[i.Rows][];
        Array1D.Do(i.Rows, row =>
        {
            result[row] = new Value[i.Columns];
            Array1D.Do(i.Columns, column => result[row][column] = Value.CreateSaturating(new Range<double>(minimum[row, column].ToDouble(), maximum[row, column].ToDouble()).Denormalize(Convert.ToDouble(i[row, column]))));
        });
        return new Matrix<Value>(result);
    }

    /// <inheritdoc cref="XNumber.Denormalize{TNumber}(Double1, IRange{TNumber})"/>
    public static Matrix<Value> Denormalize<Value>(this IMatrix<Double1> i, IRange<Value> range) where Value : IMinMaxValue<Value>, INumber<Value>
        => i.Denormalize(range.Minimum, range.Maximum);

    /// <inheritdoc cref="XNumber.Normalize{TNumber}(TNumber)"/>
    public static Matrix<Double1> Normalize<Value>(this IMatrix<Value> i) where Value : IMinMaxValue<Value>, INumber<Value>
        => i.Normalize(Value.MinValue, Value.MaxValue);

    /// <inheritdoc cref="XNumber.Normalize{TNumber}(TNumber, TNumber)"/>
    public static Matrix<Double1> Normalize<Value>(this IMatrix<Value> i, Value maximum) where Value : IMinMaxValue<Value>, INumber<Value>
        => i.Normalize(Value.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{TNumber}(TNumber, TNumber, TNumber)"/>
    public static Matrix<Double1> Normalize<Value>(this IMatrix<Value> i, Value minimum, Value maximum) where Value : IMinMaxValue<Value>, INumber<Value>
    {
        var result = new Double1[i.Rows, i.Columns];
        Array2D.Do(i.Rows, i.Columns, (row, column) => result[row, column] = i[row, column].Normalize(minimum, maximum));
        return new(result.As());
    }

    /// <inheritdoc cref="XNumber.Normalize{TNumber}(TNumber, TNumber)"/>
    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static Matrix<Double1> Normalize<Value>(this IMatrix<Value> i, IMatrix<Value> maximum) where Value : IMinMaxValue<Value>, INumber<Value>
        => i.Normalize(new Matrix<Value>(i.Rows, i.Columns, Value.MinValue), maximum);

    /// <inheritdoc cref="XNumber.Normalize{TNumber}(TNumber, TNumber, TNumber)"/>
    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static Matrix<Double1> Normalize<Value>(this IMatrix<Value> i, IMatrix<Value> minimum, IMatrix<Value> maximum) where Value : IMinMaxValue<Value>, INumber<Value>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, minimum.Columns, nameof(minimum));
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, maximum.Columns, nameof(maximum));
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, minimum.Rows, nameof(minimum));
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, maximum.Rows, nameof(maximum));

        var result = new Double1[i.Rows, i.Columns];
        Array2D.Do(i.Rows, i.Columns, (row, column) => result[row, column] = i[row, column].Normalize(minimum[row, column], maximum[row, column]));
        return new(result.As());
    }

    /// <inheritdoc cref="XNumber.Normalize{TNumber}(TNumber, IRange{TNumber})"/>
    public static Matrix<Double1> Normalize<Value>(this IMatrix<Value> i, IRange<Value> range) where Value : IMinMaxValue<Value>, INumber<Value>
        => i.Normalize(range.Minimum, range.Maximum);

    public static Matrix<Double1> Normalize<Value>(this IMatrix<Value> i, RangeType By)
        where Value : IMinMaxValue<Value>, INumber<Value>
    {
        if (By == RangeType.Type)
            return i.Normalize();

        Value minimum = i.Minimum(), maximum = i.Maximum();
        minimum = minimum == maximum ? maximum - Value.One : minimum;

        return new(Array2D.Get(i.Rows, i.Columns, (y, x) => i[y, x].Normalize(minimum, maximum)));
    }

    #endregion
}

[Extend(typeof(IMatrix3x3<>))]
public static partial class XMatrix
{
    /// <inheritdoc cref="XNumber.Do{}(Value, Operator, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static Matrix3x3<T> Do<T>(this IMatrix3x3<T> i, Operator action, IMatrix3x3<T> j)
        where T : INumber<T>
        //=> i.New((x, y) => y.Do(action, j));
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));

        var result = new T[3][];
        Array2D.Do(3, 3, (y, x) => { result[y] ??= new T[3]; result[y][x] += i[y, x].Do(action, j[y, x]); });
        return result.To(x => new Matrix3x3<T>(x[0][0], x[0][1], x[0][2], x[1][0], x[1][1], x[1][2], x[2][0], x[2][1], x[2][2]));
    }

    /// <inheritdoc cref="Do{}(Value, Operator, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Do<T>(this IMatrix3x3<T> i, Operator action, T j) where T : INumber<T>
        => i.New(x => x.Do(action, j));

    /// <inheritdoc cref="Divide{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static Matrix3x3<T> Divide<T>(this IMatrix3x3<T> i, T j) where T : INumber<T>
        => i.New(x => x.Do(Operator.Divide, j));

    /// <inheritdoc cref="XNumber.Divide{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static Matrix3x3<T> Divide<T>(this IMatrix3x3<T> i, IMatrix3x3<T> j)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Divide(j[y, x]));
    }

    /// <inheritdoc cref="Divide2{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide2<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide2);

    /// <inheritdoc cref="Divide3{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide3<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide3);

    /// <inheritdoc cref="Divide4{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide4<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide4);

    /// <inheritdoc cref="Divide5{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide5<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide5);

    /// <inheritdoc cref="Divide6{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide6<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide6);

    /// <inheritdoc cref="Divide7{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide7<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide7);

    /// <inheritdoc cref="Divide8{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide8<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide8);

    /// <inheritdoc cref="Divide9{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide9<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide9);

    /// <inheritdoc cref="Divide10{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Divide10<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Divide10);

    /// <inheritdoc cref="XNumber.Minus{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Minus<T>(this IMatrix3x3<T> i, IMatrix3x3<T> j)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Minus(j[y, x]));
    }

    /// <inheritdoc cref="Modulo{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo<T>(this IMatrix3x3<T> i, T j) where T : INumber<T>
        => i.New(x => x.Modulo(j));

    /// <inheritdoc cref="XNumber.Modulo{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo<T>(this IMatrix3x3<T> i, IMatrix3x3<T> j)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Modulo(j[y, x]));
    }

    /// <inheritdoc cref="Modulo2{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo2<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo2);

    /// <inheritdoc cref="Modulo3{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo3<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo3);

    /// <inheritdoc cref="Modulo4{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo4<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo4);

    /// <inheritdoc cref="Modulo5{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo5<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo5);

    /// <inheritdoc cref="Modulo6{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo6<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo6);

    /// <inheritdoc cref="Modulo7{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo7<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo7);

    /// <inheritdoc cref="Modulo8{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo8<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo8);

    /// <inheritdoc cref="Modulo9{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo9<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo9);

    /// <inheritdoc cref="Modulo10{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Modulo10<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Modulo10);

    /// <inheritdoc cref="Multiply{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply<T>(this IMatrix3x3<T> i, T j) where T : INumber<T>
        => i.New(x => x.Multiply(j));

    /// <inheritdoc cref="XNumber.Multiply{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply<T>(this IMatrix3x3<T> i, IMatrix3x3<T> j)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Multiply(j[y, x]));
    }

    /// <inheritdoc cref="Multiply2{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply2<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply2);

    /// <inheritdoc cref="Multiply3{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply3<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply3);

    /// <inheritdoc cref="Multiply4{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply4<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply4);

    /// <inheritdoc cref="Multiply5{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply5<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply5);

    /// <inheritdoc cref="Multiply6{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply6<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply6);

    /// <inheritdoc cref="Multiply7{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply7<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply7);

    /// <inheritdoc cref="Multiply8{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply8<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply8);

    /// <inheritdoc cref="Multiply9{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply9<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply9);

    /// <inheritdoc cref="Multiply10{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Multiply10<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Multiply10);

    /// <inheritdoc cref="Nearest{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Nearest<T>(this IMatrix3x3<T> i, T multiple)
        where T : INumber<T> => i.New(j => j.Nearest(multiple));

    /// <inheritdoc cref="XNumber.Nearest{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Nearest<T>(this IMatrix3x3<T> i, IMatrix3x3<T> multiple)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(multiple, nameof(multiple));
        return i.New((y, x, w) => w.Nearest(multiple[y, x]));
    }

    /// <inheritdoc cref="XNumber.Plus{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Plus<T>(this IMatrix3x3<T> i, IMatrix3x3<T> j)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Plus(j[y, x]));
    }

    /// <inheritdoc cref="Pow{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow<T>(this IMatrix3x3<T> i, double j) where T : INumber<T> => i.New((x, y) => T.CreateSaturating(Math.Pow(Convert.ToDouble(y), j)));

    /// <inheritdoc cref="XNumber.Pow{}(Value, Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow<T>(this IMatrix3x3<T> i, IMatrix3x3<T> j)
        where T : INumber<T>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((y, x, w) => w.Pow(j[y, x]));
    }

    /// <inheritdoc cref="Pow2{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow2<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow2);

    /// <inheritdoc cref="Pow3{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow3<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow3);

    /// <inheritdoc cref="Pow4{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow4<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow4);

    /// <inheritdoc cref="Pow5{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow5<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow5);

    /// <inheritdoc cref="Pow6{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow6<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow6);

    /// <inheritdoc cref="Pow7{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow7<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow7);

    /// <inheritdoc cref="Pow8{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow8<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow8);

    /// <inheritdoc cref="Pow9{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow9<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow9);

    /// <inheritdoc cref="Pow10{}(Value)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Matrix3x3<T> Pow10<T>(this IMatrix3x3<T> i) where T : INumber<T> => i.New(XNumber.Pow10);
}
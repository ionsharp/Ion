using System;
using System.Numerics;

namespace Ion.Numeral;

public static class MatrixDeterminant<T> where T : IFloatingPoint<T>, INumber<T>
{
    private static void SwapRows(T[][] matrix, int row1, int row2)
    {
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            T temp = matrix[row1][i];
            matrix[row1][i] = matrix[row2][i];
            matrix[row2][i] = temp;
        }
    }

    private static T[,] GetCholeskyDecomposition(IMatrix<T> i)
    {
        int n = (int)i.Columns.ToDouble().Root2();

        T[,] ret = new T[n, n];

        for (int y = 0; y < n; y++)
            for (int x = 0; x <= y; x++)
            {
                if (x == y)
                {
                    T sum = T.Zero;
                    for (int j = 0; j < x; j++)
                        sum += ret[x, j] * ret[x, j];

                    ret[x, x] = (i[x, x] - sum).Root2();
                }
                else
                {
                    T sum = T.Zero;
                    for (int j = 0; j < x; j++)
                        sum += ret[y, j] * ret[x, j];

                    ret[y, x] = T.One / ret[x, x] * (i[y, x] - sum);
                }
            }

        return ret;
    }

    /// <summary>
    /// Get determinant using <see cref="MatrixDeterminantFormula.CholeskyDecomposition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// <b>Must be <see cref="IMatrix3x3"/>, <see cref="Matrix2DProperty.PositiveDefinite"/>, and <see cref="Matrix2DProperty.Symmetric"/>.</b>
    /// </remarks>
    public static T CholeskyDecomposition(IMatrix<T> i)
    {
        Throw.IfNull(i, nameof(i));

        var L = GetCholeskyDecomposition(i);

        T result = T.One;
        for (int x = 0; x < L.Length; x++)
            result *= L[x, x];

        return result.Pow2();
    }

    /// <summary>
    /// Get determinant using <see cref="MatrixDeterminantFormula.CofactorExpansion"/>.
    /// </summary>
    /// <remarks>
    /// <b>Recursive nature can lead to stack overflow for large matrices.</b>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    [Recursive]
    public static T CofactorExpansion(IMatrix<T> i) 
    {
        Throw.IfNull(i, nameof(i));
        int n = i.Rows;
        if (n == 1)
            return i[0, 0];

        if (n == 2)
            return i[0, 0] * i[1, 1] - i[0, 1] * i[1, 0];

        T result = T.Zero;
        for (int m = 0; m < n; m++)
            result += Math.Pow(-1, m).Create<T>() * i[0, m] * i.GetMinor(0, m).GetDeterminant(MatrixDeterminantFormula.CofactorExpansion);

        return result;
    }

    /// <summary>
    /// Get determinant using <see cref="MatrixDeterminantFormula.ColumnOperations"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T ColumnOperations(IMatrix<T> i)
    {
        Throw.IfNull(i, nameof(i));

        var array = i.ToArray();
        int n = array.Length;

        T result = T.One;
        for (int index = 0; index < n; index++)
        {
            /// Find pivot element in current column
            int maxRow = index;
            for (int j = index + 1; j < n; j++)
            {
                if (T.Abs(array[j][index]) > T.Abs(array[maxRow][index]))
                {
                    maxRow = j;
                }
            }

            /// Swap rows if necessary to ensure pivot is non-zero
            if (maxRow != index)
            {
                T[] temp = array[index];
                array[index] = array[maxRow];
                array[maxRow] = temp;
                result *= -T.One; /// Change sign if rows are swapped
            }

            /// If pivot is zero, determinant is zero
            if (array[index][index] == T.Zero)
                return T.Zero;

            /// Make all elements below pivot zero
            for (int j = index + 1; j < n; j++)
            {
                T factor = array[j][index] / array[index][index];
                for (int k = index; k < n; k++)
                    array[j][k] -= factor * array[index][k];
            }
        }

        /// Calculate determinant as product of diagonal elements
        for (int index = 0; index < n; index++)
            result *= array[index][index];

        return result;
    }

    /// <summary>
    /// Get determinant using <see cref="MatrixDeterminantFormula.EigenvalueDecomposition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotImplemented]
    public static T EigenvalueDecomposition(IMatrix<T> i) 
    {
        Throw.IfNull(i, nameof(i));
        return default;
    }

    /// <summary>
    /// Get determinant using <see cref="MatrixDeterminantFormula.Leibniz"/>.
    /// </summary>
    /// <remarks>
    /// <b>Recursive nature can lead to stack overflow for large matrices.</b>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    [Recursive]
    public static T Leibniz(IMatrix<T> i) 
    {
        Throw.IfNull(i, nameof(i));

        int n = i.Columns;
        T result = T.Zero;
        for (int x = 0; x < n; x++)
        {
            T sign = Math.Pow(-1, x).Create<T>();
            T cofactor = T.Zero;
            for (int y = 1; y < n; y++)
                cofactor += i[y, x] * Leibniz(i.GetMinor(y, x));

            result += sign * i[0, x] * cofactor;
        }
        return result;
    }

    private static (T[,] L, T[,] U) GetLUDecomposition(IMatrix<T> matrix)
    {
        Throw.IfNull(matrix, nameof(matrix));

        int n = matrix.Columns;

        T[,] L = new T[n, n];
        T[,] U = new T[n, n];

        /// Initialize L and U matrices
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                /// Diagonal elements of L are 1
                if (i == j)
                    L[i, j] = T.One;
                else L[i, j] = T.Zero;

                U[i, j] = T.Zero;
            }
        }

        /// Perform LU decomposition
        for (int k = 0; k < n; k++)
        {
            /// Decompose the upper triangular matrix
            for (int i = k; i < n; i++)
            {
                T sum = T.Zero;
                for (int p = 0; p < k; p++)
                    sum += L[i, p] * U[p, k];

                U[i, k] = matrix[i, k] - sum;
            }
            /// Decompose the lower triangular matrix
            for (int i = k + 1; i < n; i++)
            {
                T sum = T.Zero;
                for (int p = 0; p < k; p++)
                    sum += L[i, p] * U[p, k];

                L[i, k] = (matrix[i, k] - sum) / U[k, k];
            }
        }
        return (L, U);
    }

    /// <summary>
    /// Get determinant using <see cref="MatrixDeterminantFormula.LUDecomposition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T LUDecomposition(IMatrix<T> i)
    {
        Throw.IfNull(i, nameof(i));
        return default;
    }

    /// <summary>
    /// Get determinant using <see cref="MatrixDeterminantFormula.RowOperations"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T RowOperations(IMatrix<T> i)
    {
        int n = i.Rows;

        var matrix = i.ToArray();

        T result = T.One;
        for (int m = 0; m < n; m++)
        {
            // Find the pivot row and swap if necessary
            int maxRow = m;
            for (int j = m + 1; j < n; j++)
            {
                if (T.Abs(matrix[j][m]) > T.Abs(matrix[maxRow][m]))
                    maxRow = j;
            }
            if (maxRow != m)
            {
                SwapRows(matrix, m, maxRow);
                result *= -T.One;
            }

            // If the pivot element is zero, the determinant is zero
            if (matrix[m][m] == T.Zero)
                return T.Zero;

            // Perform row operations to make all elements below the pivot zero
            for (int j = m + 1; j < n; j++)
            {
                T factor = matrix[j][m] / matrix[m][m];
                for (int k = m; k < n; k++)
                    matrix[j][k] -= factor * matrix[m][k];
            }

            // Multiply the pivot element to the determinant
            result *= matrix[m][m];
        }
        return result;
    }

    /// <summary>
    /// Get determinant using <see cref="MatrixDeterminantFormula.SarrusRule"/>.
    /// </summary>
    /// <remarks>
    /// <b>Only applies to <see cref="IMatrix3x3"/>.</b>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static T SarrusRule(IMatrix<T> i) 
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArgumentException>(i is not IMatrix3x3 && (i.Columns != 3 || i.Rows != 3), nameof(i));

        T result = T.Zero;
        result += i[0, 0] * i[1, 1] * i[2, 2];
        result -= i[0, 1] * i[1, 2] * i[2, 0];
        result -= i[0, 2] * i[1, 0] * i[2, 1];
        result += i[0, 2] * i[1, 1] * i[2, 0];
        result -= i[0, 0] * i[1, 1] * i[2, 2];
        return result;
    }
}
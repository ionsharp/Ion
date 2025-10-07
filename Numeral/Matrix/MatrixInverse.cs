using System;
using System.Numerics;

namespace Ion.Numeral;

public static class MatrixInverse
{
    public static TSelf InvertAnalytic<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i)
        where TSelf : IMatrix2D<TSelf, TValue>
        where TValue : INumber<TValue>
    {
        if (i.Columns == i.Rows && i.Rows == 2)
            return i.InvertAnalytic2x2();

        if (i.Columns == i.Rows && i.Rows == 3)
            return i.InvertAnalytic3x3();

        if (i.Columns == i.Rows && i.Rows == 4)
            return i.InvertAnalytic4x4();

        throw new MatrixNotCompatible();
    }

    /// <summary>Get inverse of 2x2 <see cref="IMatrix"/> using <see cref="MatrixInverseFormula.Analytic"/>.</summary>
    /// <exception cref="MatrixNoInverse"/>
    /// <exception cref="MatrixNot2x2"/>
    [NotTested]
    public static TSelf InvertAnalytic2x2<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i)
        where TSelf : IMatrix2D<TSelf, TValue>
        where TValue : INumber<TValue>
    {
        Throw.IfNotEqual<MatrixNot2x2>(i.Columns, 2, nameof(i));
        Throw.IfNotEqual<MatrixNot2x2>(i.Columns, i.Rows, nameof(i));

        var d = i[0, 0] * i[1, 1] - i[0, 1] * i[1, 0];

        Throw.If<MatrixNoInverse>(d.IsZero());
        return TSelf.Create
        (
            (TSelf)i,
            new Array2D<TValue>
            ([
                [  i[1, 1] / d, -i[0, 1] / d],
                [ -i[1, 0] / d,  i[0, 0] / d]
            ])
        );
    }

    /// <summary>Get inverse of 3x3 <see cref="IMatrix"/> using <see cref="MatrixInverseFormula.Analytic"/>.</summary>
    /// <exception cref="MatrixNoInverse"/>
    /// <exception cref="MatrixNot3x3"/>
    [NotTested]
    public static TSelf InvertAnalytic3x3<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i)
        where TSelf : IMatrix2D<TSelf, TValue>
        where TValue : INumber<TValue>
    {
        Throw.IfNotEqual<MatrixNot3x3>(i.Columns, 3, nameof(i));
        Throw.IfNotEqual<MatrixNot3x3>(i.Columns, i.Rows, nameof(i));

        var A =   i[1, 1] * i[2, 2] - i[1, 2] * i[2, 1];
        var B = -(i[1, 0] * i[2, 2] - i[1, 2] * i[2, 0]);
        var C =   i[1, 0] * i[2, 1] - i[1, 1] * i[2, 0];
        var D = -(i[0, 1] * i[2, 2] - i[0, 2] * i[2, 1]);
        var E =   i[0, 0] * i[2, 2] - i[0, 2] * i[2, 0];
        var F = -(i[0, 0] * i[2, 1] - i[0, 1] * i[2, 0]);
        var G =   i[0, 1] * i[1, 2] - i[0, 2] * i[1, 1];
        var H = -(i[0, 0] * i[1, 2] - i[0, 2] * i[1, 0]);
        var I =   i[0, 0] * i[1, 1] - i[0, 1] * i[1, 0];

        var d = i[0, 0] * A + i[0, 1] * B + i[0, 2] * C;

        Throw.If<MatrixNoInverse>(d.IsZero());
        return TSelf.Create
        (
            (TSelf)i,
            new Array2D<TValue>
            ([
                [A / d, D / d, G / d],
                [B / d, E / d, H / d],
                [C / d, F / d, I / d],
            ])
        );
    }

    /// <summary>Get inverse of 4x4 <see cref="IMatrix"/> using <see cref="MatrixInverseFormula.Analytic"/>.</summary>
    /// <exception cref="MatrixNoInverse"/>
    /// <exception cref="MatrixNot4x4"/>
    [NotTested]
    public static TSelf InvertAnalytic4x4<TSelf, TValue>(this IMatrix2D<TSelf, TValue> i)
        where TSelf : IMatrix2D<TSelf, TValue>
        where TValue : INumber<TValue>
    {
        Throw.IfNotEqual<MatrixNot4x4>(i.Columns, 4, nameof(i));
        Throw.IfNotEqual<MatrixNot4x4>(i.Columns, i.Rows, nameof(i));

        TValue
            A = i[0, 0], B = i[0, 1], C = i[0, 2], D = i[0, 3],
            E = i[1, 0], F = i[1, 1], G = i[1, 2], H = i[1, 3],
            I = i[2, 0], J = i[2, 1], K = i[2, 2], L = i[2, 3],
            M = i[3, 0], N = i[3, 1], O = i[3, 2], P = i[3, 3];

        var d =
            A * (F * (K * P - L * O) - G * (J * P - L * N)) +
            B * (E * (K * P - L * O) - G * (I * P - L * N)) +
            C * (E * (J * P - L * N) - F * (I * P - L * M));

        Throw.If<MatrixNoInverse>(d.IsZero());
        return TSelf.Create
        (
            (TSelf)i,
            new Array2D<TValue>
            ([
                [
                     (F * K * P - G * J * P - F * L * O + H * J * O) / d,
                    (-B * K * P + C * J * P + B * L * O - D * J * O) / d,
                     (B * G * P - C * F * P - B * H * O + D * F * O) / d,
                    (-B * G * L + C * F * L + B * H * K - D * F * K) / d
                ],
                [
                    (-E * K * P + G * I * P + E * L * O - H * I * O) / d,
                     (A * K * P - C * I * P - A * L * O + D * I * O) / d,
                    (-A * G * P + C * E * P + A * H * O - D * E * O) / d,
                     (A * G * L - C * E * L - A * H * K + D * E * K) / d
                ],
                [
                     (E * J * P - F * I * P - E * L * O + H * I * O) / d,
                    (-A * J * P + B * I * P + A * L * O - D * I * O) / d,
                     (A * F * P - B * E * P - A * H * O + D * E * O) / d,
                    (-A * F * L + B * E * L + A * H * K - D * E * K) / d
                ],
                [
                    (-E * J * L + F * I * L + E * K * O - G * I * O) / d,
                     (A * J * L - B * I * L - A * K * O + C * I * O) / d,
                    (-A * F * L + B * E * L + A * G * O - C * E * O) / d,
                     (A * F * K - B * E * K - A * G * L + C * E * L) / d
                ]
            ])
        );
    }

    internal static TSelf InvertBlockwise<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
        => default;

    internal static TSelf InvertCayley<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
        => default;

    internal static Matrix<TValue> DecomposeCholesky<TValue>(IMatrix<TValue> matrix) where TValue : INumber<TValue>
    {
        int n = matrix.Rows * matrix.Columns;
        var result = new Matrix<TValue>(n, TValue.Zero).ToArray();

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j <= i; ++j)
            {
                TValue sum = TValue.Zero;
                for (int k = 0; k < j; ++k)
                    sum += result[i][k] * result[j][k];

                if (i == j)
                {
                    TValue tmp = matrix[i, i] - sum;
                    Throw.If<MatrixNoInverse>(tmp < TValue.Zero);

                    result[i][j] = TValue.CreateSaturating(Math.Sqrt(tmp.ToDouble()));
                }
                else
                {
                    Throw.IfEqual<MatrixNoInverse>(result[j][j], TValue.Zero);
                    result[i][j] = (TValue.One / result[j][j]) * (matrix[i, j] - sum);
                }
            }
        }
        return new(result);
    }

    internal static TSelf InvertCholesky<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
    {
        var L = DecomposeCholesky(matrix);

        int n = L.Rows;

        var result = new TValue[n, n];
        for (var i = 0; i < n; i++)
        {
            result[i, i] = TValue.One / L[i, i];
            for (var j = i + 1; j < n; j++)
                result[i, j] = -L[i, j] * result[j, j];
        }
        return TSelf.Create((TSelf)matrix, result.As());
    }

    internal static TSelf InvertDrazin<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
        => default;

    internal static TSelf InvertEigen<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
        => default;

    internal static TSelf InvertGaussian<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
    {
        var n = matrix.Rows;
        TValue[,] augmented = new TValue[n, n * 2];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                augmented[i, j] = matrix[i, j];
                augmented[i, j + n] = (i == j) ? TValue.One : TValue.Zero;
            }
        }
        for (var i = 0; i < n; i++)
        {
            var pivotRow = i;
            for (var j = i + 1; j < n; j++)
            {
                if (augmented[j, i].ToDouble().Abs() > augmented[pivotRow, i].ToDouble().Abs())
                    pivotRow = j;
            }

            if (pivotRow != i)
            {
                for (var k = 0; k < 2 * n; k++)
                {
                    var temp = augmented[i, k];
                    augmented[i, k] = augmented[pivotRow, k];
                    augmented[pivotRow, k] = temp;
                }
            }

            Throw.If<MatrixNoInverse>(augmented[i, i].ToDouble().Abs() < 1e-10);

            var pivot = augmented[i, i];
            for (var j = 0; j < 2 * n; j++)
                augmented[i, j] /= pivot;

            for (var j = 0; j < n; j++)
            {
                if (j != i)
                {
                    var factor = augmented[j, i];
                    for (var k = 0; k < 2 * n; k++)
                        augmented[j, k] -= factor * augmented[i, k];
                }
            }
        }

        var result = new TValue[n, n];
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
                result[i, j] = augmented[i, j + n];
        }

        return TSelf.Create((TSelf)matrix, result.As());
    }

    internal static void DecomposeLU<TValue>(IMatrix<TValue> matrix, out Matrix<TValue> L, out Matrix<TValue> U, out int[] Perm) where TValue : INumber<TValue>
    {
        int n = matrix.Rows;

        var l = new TValue[n, n];
        var u = new TValue[n, n];
        Perm = new int[n];

        for (var i = 0; i < n; i++)
        {
            // Crout's algorithm
            for (var k = 0; k < i; k++)
            {
                var sum = TValue.Zero;
                for (int j = 0; j < k; j++)
                    sum += l[i, j] * u[j, k];

                l[i, k] = matrix[i, k] - sum;
            }

            // Normalize L[i, i]
            TValue lii = l[i, i];
            l[i, i] = TValue.One;
            for (var j = 0; j < i; j++)
                l[i, j] /= lii;

            // Update U
            for (int j = i + 1; j < n; j++)
                u[i, j] = (matrix[i, j] - l[i, i] * u[i, i]) / l[i, i];
        }

        // Permutation matrix
        for (int i = 0; i < n; i++)
            Perm[i] = i;

        L = new Matrix<TValue>(l.As());
        U = new Matrix<TValue>(u.As());
    }

    internal static TSelf InvertLU<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
    {
        DecomposeLU(matrix, out Matrix<TValue> L, out Matrix<TValue> U, out int[] P);

        var result = new TValue[matrix.Rows, matrix.Columns];
        for (var i = 0; i < matrix.Rows; i++)
        {
            for (var j = 0; j < matrix.Columns; j++)
            {
                result[i, j] = TValue.Zero;
                for (var k = 0; k < matrix.Rows; k++)
                    result[i, j] += L[i, k] * U[k, j];
            }
        }

        return TSelf.Create((TSelf)matrix, result.As());
    }

    internal static TSelf InvertMoore<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
        => default;

    internal static TSelf InvertNeumann<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
        => default;

    internal static TSelf InvertPseudo<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
        => default;

    internal static void DecomposeQR<TValue>(IMatrix<TValue> mat, out TValue[][] q, out TValue[][] r, bool standardize) where TValue : IFloatingPoint<TValue>, INumber<TValue>, ISignedNumber<TValue>
    {
        // QR decomposition, Householder algorithm.
        // doesn't require square matrix

        int m = mat.Rows;
        int n = mat.Columns;

        if (standardize)
            Throw.IfNotEqual<MatrixNotStandardizable>(m, n);

        TValue[][] Q = new Matrix<TValue>(m, TValue.One, TValue.Zero, MatrixFill.DiagonalRight).ToArray();
        TValue[][] R = mat.ToArray();

        int end;
        if (m == n) end = n - 1; else end = n;
        for (int i = 0; i < end; ++i)
        {
            TValue[][] H = new Matrix<TValue>(m, TValue.One, TValue.Zero, MatrixFill.DiagonalRight).ToArray();
            TValue[] a = new TValue[n - i];
            int k = 0;
            for (int ii = i; ii < n; ++ii)
                a[k++] = R[ii][i];

            TValue normA = new Vector<TValue>(a).Norm();
            if (a[0] < TValue.Zero)
                normA = -normA;

            TValue[] v = new TValue[a.Length];
            for (var j = 0; j < v.Length; ++j)
                v[j] = a[j] / (a[0] + normA);

            v[0] = TValue.One;

            TValue[][] h = new Matrix<TValue>(a.Length, TValue.One, TValue.Zero, MatrixFill.DiagonalRight).ToArray();
            TValue vvDot = new Vector<TValue>(v).Dot(v);

            TValue[][] alpha = new Matrix<TValue>(new Vector<TValue>(v, VectorType.Y)).ToArray();    //y, x = v.Length, 1
            TValue[][] beta = new Matrix<TValue>(new Vector<TValue>(v, VectorType.X)).ToArray();  //y, x = 1, v.Length

            TValue[][] aMultB = new Matrix<TValue>(alpha).Do(Operator.Multiply, new Matrix<TValue>(beta)).ToArray();

            for (var ii = 0; ii < h.Length; ++ii)
                for (var jj = 0; jj < h[0].Length; ++jj)
                    h[ii][jj] -= (2.Create<TValue>() / vvDot) * aMultB[ii][jj];

            // copy h into lower right of H
            var d = n - h.Length;
            for (var ii = 0; ii < h.Length; ++ii)
                for (var jj = 0; jj < h[0].Length; ++jj)
                    H[ii + d][jj + d] = h[ii][jj];

            Q = new Matrix<TValue>(Q).Do(Operator.Multiply, new Matrix<TValue>(H)).ToArray();
            R = new Matrix<TValue>(H).Do(Operator.Multiply, new Matrix<TValue>(R)).ToArray();
        }

        if (standardize)
        {
            /// Standardize so R diagonal is all positive
            TValue[][] D = new Matrix<TValue>(n).ToArray(); // m == n
            for (var i = 0; i < n; ++i)
            {
                if (R[i][i] < TValue.Zero)
                    D[i][i] = -TValue.One;

                else D[i][i] = TValue.One;
            }
            Q = new Matrix<TValue>(Q).Do(Operator.Multiply, new Matrix<TValue>(D)).ToArray();
            R = new Matrix<TValue>(D).Do(Operator.Multiply, new Matrix<TValue>(R)).ToArray();
        }

        q = Q;
        r = R;
    }

    internal static TSelf InvertQR<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix)
        where TSelf : IMatrix2D<TSelf, TValue>
        where TValue : IFloatingPoint<TValue>, INumber<TValue>, ISignedNumber<TValue>
    {
        DecomposeQR(matrix, out TValue[][] Q, out TValue[][] R, true);

        var result = new TValue[matrix.Rows, matrix.Columns];
        for (var i = 0; i < matrix.Rows; i++)
        {
            for (var j = 0; j < matrix.Columns; j++)
            {
                result[i, j] = TValue.Zero;
                for (var k = 0; k < matrix.Rows; k++)
                    result[i, j] += Q[i][k] * R[k][j];
            }
        }

        return TSelf.Create((TSelf)matrix, result.As());
    }

    internal static TSelf InvertStrassen<TSelf, TValue>(IMatrix2D<TSelf, TValue> matrix) where TSelf : IMatrix2D<TSelf, TValue> where TValue : INumber<TValue>
        => default;
}
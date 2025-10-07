namespace Ion.Numeral;

public enum MatrixInverseFormula
{
    /// <summary>
    /// <para><b>Analytic Solution (Cramer's Rule)</b></para>
    /// <para>If determinant is non-zero, <see cref="IMatrix"/> is <i>invertible</i> (or <see cref="Matrix2DProperty.NonSingular"/>). Otherwise, it's <see cref="Matrix2DProperty.Singular"/>.</para>
    /// <para>Useful for smaller matrices. For larger, there is more complexity.</para>
    /// <see href="https://en.wikipedia.org/wiki/Invertible_matrix#Inversion_of_3_%C3%97_3_matrices"/>
    /// </summary>
    Analytic,
    /// <summary>
    /// <b>Blockwise Inversion</b><br/>
    /// A technique used to invert large matrices by dividing them into smaller blocks and inverting each block separately. Useful when dealing with sparse matrices or matrices with a specific structure.
    /// </summary>
    Blockwise,
    /// <summary>
    /// <b>Cayley-Hamilton Method</b><br/>
    /// A technique for computing the inverse of a square matrix using its characteristic polynomial.
    /// </summary>
    Cayley,
    /// <summary>
    /// <b>Cholesky Decomposition</b><br/>
    /// A method for computing the inverse of a Hermitian positive-definite (HPD) matrix.
    /// </summary>
    Cholesky,
    /// <summary>
    /// <b>Drazin</b><br/>
    /// A type of generalized inverse of a square matrix, named after <i>Michael P. Drazin</i>. It is a matrix inverse-like object derived from a given square matrix <b>A</b>. The Drazin inverse, denoted as <b>A^D</b>, satisfies three fundamental properties:
    /// <list type="bullet">
    /// <item><b>Commutativity</b></item>
    /// <br/>A^(k+1)A^D = A^k
    /// <item><b>Idempotence</b></item>
    /// <br/>A^DAA^D = A^D
    /// <item><b>Nilpotency</b></item>
    /// <br/>AA^D = A^DA
    /// </list>
    /// <br/>Where <b>k</b> is the Drazin index of <b>A</b>, defined as the smallest nonnegative integer such that rank(A^(k+1)) = rank(A^k).
    /// </summary>
    Drazin,
    /// <summary>
    /// <b>Eigen Decomposition</b><br/>
    /// A technique used to compute the inverse of a square matrix <b>A</b> using the eigenvalues and eigenvectors of <b>A</b>. This approach is particularly useful when <b>A</b> is large and sparse, as it can be more efficient than traditional methods like <see cref="LU"/> or <see cref="Cholesky"/>.
    /// </summary>
    Eigen,
    /// <summary>
    /// <b>Gaussian Elimination</b><br/>
    /// An augmented matrix is created with the left side being the matrix to invert and the right side being an identity matrix. The left side is then converted into an identity matrix, which causes the right side to become the inverse.
    /// </summary>
    Gaussian,
    /// <summary>
    /// <b>LU Decomposition (LU Factorization/Lower-Upper Decomposition)</b><br/>
    /// Factors a matrix as the product of a lower triangular matrix and an upper triangular matrix. The product sometimes includes a permutation matrix.
    /// </summary>
    /// <remarks>Most commonly used.</remarks>
    LU,
    /// <summary>
    /// <b>Moore-Penrose (Pseudoinverse/Generalized Inverse)</b><br/>
    /// The most widely known generalization of inversion, independently described by <i>E. H. Moore</i> in 1920, <i>Arne Bjerhammar</i> in 1951, and <i>Roger Penrose</i> in 1955.
    /// </summary>
    Moore,
    /// <summary>
    /// <b>Neumann Series</b><br/>
    /// A method for approximating the inverse of a matrix using an infinite series expansion. Ued to reduce computational complexity of matrix inversion, particularly in large-scale systems, such as massive MIMO (Multiple-Input Multiple-Output) systems.
    /// </summary>
    Neumann,
    /// <summary>
    /// <b>Pseudo</b><br/>
    /// A generalization of traditional inversion, applicable to both singular and rectangular matrices. It extends the concept of inversion to cases where a traditional inverse does not exist or is not unique.
    /// </summary>
    Pseudo,
    /// <summary>
    /// <b>QR Decomposition (QR/QU Factorization)</b><br/>
    /// A decomposition of a matrix <b>A</b> into a product <b>A</b> = <b>QR</b> of an orthonormal matrix <b>Q</b> and an upper triangular matrix <b>R</b>. Often used to solve the linear least squares (LLS) problem and is the basis for a particular eigenvalue algorithm, the QR algorithm.
    /// </summary>
    QR,
    /// <summary>
    /// <b>Strassen formula</b><br/>
    /// An algorithm for matrix multiplication named after <i>Volker Strassen</i>. It is faster than standard matrix multiplication algorithm for large matrices, with better asymptotic complexity, although the naive algorithm is often better for smaller matrices.
    /// </summary>
    Strassen,
}
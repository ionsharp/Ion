using System;

namespace Ion.Numeral;

public class MatrixColumnMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have same number of columns as {1}", bName));

public class MatrixColumnAndRowMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have same number of columns and rows as {1}", bName));

public class MatrixColumnRowMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have same number of columns as {1} has rows", bName));

public class MatrixNoInverse(string aName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} doesn't have an inverse"));

public class MatrixNot2x2(string aName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have 2 columns and rows"));

public class MatrixNot3x3(string aName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have 3 columns and rows"));

public class MatrixNot4x4(string aName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have 4 columns and rows"));

public class MatrixNotCompatible(string aName = null, string bName = null)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} is not compatible with {1}", bName, $"{nameof(Matrix<double>)} is not compatible."));

public class MatrixNotImplemented(string aName, Exception e)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} is not implemented"), e);

public class MatrixNotSquare(string aName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must be square"));

public class MatrixNotStandardizable(string aName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must be square in order to be standardized"));

public class MatrixRowMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have same number of rows as {1}", bName));

public class MatrixRowColumnMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have same number of rows as {1} has columns", bName));

public class MatrixVectorMismatch(string aName)
    : Exception(Throw.Message(nameof(Matrix<double>), aName, "{0} must have same number of columns as length of vector"));
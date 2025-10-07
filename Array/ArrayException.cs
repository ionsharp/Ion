using System;

namespace Ion;

public class ArrayLengthMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Array), aName, "must have same length as array '{bName}'.", bName));

public class ArrayLengthZero(string aName)
    : Exception(Throw.Message(nameof(Array), aName, "must not have length of 0"));

public class ArrayNotImplemented(string aName, Exception e = null)
    : Exception(Throw.Message(nameof(Array), aName, "isn't implemented."), e);

public class ArrayJaggedColumnRowMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Array), aName, "must have same number of columns as array '{bName}' has rows.", bName));

public class ArrayJaggedColumnAndRowMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Array), aName, "must have same number of columns and rows as array '{bName}'.", bName));

public class ArrayJaggedColumnMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Array), aName, "must have same number of columns as array '{bName}'.", bName));

public class ArrayJaggedLengthMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Array), aName, "must have same length as array '{bName}'.", bName));

public class ArrayJaggedNotUniform(string aName)
    : Exception(Throw.Message(nameof(Array), aName, "has row with different columns."));

public class ArrayJaggedRowMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Array), aName, "must have same number of rows as array '{bName}'.", bName));

public class ArrayJaggedRowColumnMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Array), aName, "must have same number of rows as array '{bName}' has columns.", bName));

public class ArrayJaggedUnexpectedDimensions(string aName)
    : Exception(Throw.Message(nameof(Array), aName, "has row with unexpected columns."));

public class ArrayJaggedZeroColumns(string aName)
    : Exception(Throw.Message(nameof(Array), aName, "has row with 0 columns."));

public class ArrayJaggedZeroRows(string aName)
    : Exception(Throw.Message(nameof(Array), aName, "has 0 rows."));

public class ArrayJaggedZeroSlices(string aName)
    : Exception(Throw.Message(nameof(Array), aName, "has 0 slices."));
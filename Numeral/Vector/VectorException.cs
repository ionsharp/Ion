using System;

namespace Ion.Numeral;

public class VectorLengthMismatch(string aName, string bName = null)
    : Exception(Throw.Message(nameof(Vector), aName, "must have same length as", bName));

public class VectorLengthZero(string aName)
    : Exception(Throw.Message(nameof(Vector), aName, "must not have length of 0"));

public class VectorNotConstructible(string aName)
    : Exception(Throw.Message(nameof(Vector), aName, "cannot be created without at least 1 element."));

public class VectorNotImplemented(string aName, Exception e)
    : Exception(Throw.Message(nameof(Vector), aName, "is not implemented"), e);

public class VectorNotParsable(string aName)
    : Exception(Throw.Message(nameof(Vector), aName, "cannot be parsed"));

public class VectorInvalidIndex(string aName)
    : Exception(Throw.Message(nameof(Vector), aName, "is out of range"));
using System;
using System.IO;

namespace Ion.Storage;

public class FileNotConvertible(string filePath, Exception e = null)
    : Exception($"{nameof(File)} '{filePath}' cannot be converted.", e);

public class FileNotDeserialized(string filePath, Exception e = null)
    : Exception($"{nameof(File)} '{filePath}' failed to deserialize.", e);

public class FileNotFound(string filePath, Exception e = null)
    : Exception($"{nameof(File)} '{filePath}' was not found.", e);

public class FileNotSerialized(string filePath, Exception e = null)
    : Exception($"{nameof(File)} '{filePath}' failed to serialize.", e);

public class FileNotSupported(string filePath, Exception e = null)
    : Exception($"{nameof(File)} '{filePath}' is not supported.", e);

public class FileNotValid(string filePath, Exception e = null)
    : Exception($"{nameof(File)} '{filePath}' is invalid or corrupt.", e);

public class FileNotValidExtension(string characters, Exception e = null)
    : Exception($"{nameof(File)} extension contains invalid characters '{characters}'.", e);

public class FileNotValidName(string characters, Exception e = null)
    : Exception($"{nameof(File)} name contains invalid characters '{characters}'.", e);
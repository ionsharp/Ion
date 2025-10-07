using System;
using System.IO;

namespace Ion.Storage;

public class FolderEmpty(string folderPath, Exception e = null)
    : Exception($"{nameof(Directory)} '{folderPath}' doesn't contain any files.", e);

public class FolderNotFound(string folderPath, Exception e = null)
    : Exception($"{nameof(Directory)} '{folderPath}' was not found.", e);
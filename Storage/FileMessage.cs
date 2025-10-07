using Ion.Analysis;

namespace Ion.Storage;

public sealed record class MessageFolderAccess(string folderPath)
    : Message($"Accessing the folder '{folderPath}'.");

public sealed record class MessageFileAccess(string filePath)
    : Message($"Accessing the file '{filePath}'.");

public sealed record class MessageFileConvert(string oldFilePath, string newFilePath)
    : Message($"Converting '{oldFilePath}' to '{newFilePath}'.");

public sealed record class MessageFileCopy(string oldFilePath, string newFilePath)
    : Message($"Copying '{oldFilePath}' to '{newFilePath}'.");

public sealed record class MessageFileDelete(string oldFilePath)
    : Message($"Deleting '{oldFilePath}'.");

public sealed record class MessageFileMove(string oldFilePath, string newFilePath)
    : Message($"Moving '{oldFilePath}' to '{newFilePath}'.");

public sealed record class MessageFileRename(string oldFilePath, string newFilePath)
    : Message($"Renaming '{oldFilePath}' to '{newFilePath}'.");
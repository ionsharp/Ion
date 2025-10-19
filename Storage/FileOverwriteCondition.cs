using System;

namespace Ion.Storage;

public enum FileOverwriteCondition
{
    IfNewer,
    IfSizeDifferent,
    IfNewerOrSizeDifferent,
    Always
}
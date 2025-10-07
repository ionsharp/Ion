using System;

namespace Ion.Storage;

[Serializable]
public enum FileOverwriteCondition
{
    IfNewer,
    IfSizeDifferent,
    IfNewerOrSizeDifferent,
    Always
}
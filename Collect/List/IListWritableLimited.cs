using Ion.Analysis;
using Ion.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IListWritable{T}"/> with a <see cref="ListWritableLimit"/>.
/// </summary>
/// <remarks>
/// Implements <see cref="IListLimited{T}"/>.
/// </remarks>
public interface IListWritableLimited<T> : IListWritable<T>, IListLimited<T>
{
    new public ListWritableLimit Limit { get; }

    ListLimit IListLimited<T>.Limit => new(Limit.Count, Limit.Action == ListWritableLimitAction.ClearAndArchive ? default : (ListLimitAction)(int)Limit);
}

/// <summary>
/// Extends <see cref="IListWritableLimited{T}"/>.
/// </summary>
[Extend(typeof(IListWritableLimited<>))]
public static class XListWritableLimited
{
    private static Result Archive<T>([NotNull]IListWritableLimited<T> i)
    {
        var items = new List<object>();
        for (int index = 0, count = i.Count; index < count; index++)
        {
            if (items.Count == i.Limit.Count)
            {
                var filePath = FilePath.CloneName(i.FilePath, FilePath.DefaultCloneFormat, System.IO.File.Exists);

                /// If one fails, they all fail!
                if (i.Serialize(filePath, items) is Error error)
                    return error;

                for (var x = 0; x < i.Limit.Count; x++)
                    i.RemoveAt(0);

                index = -1; count -= i.Limit.Count;
                items.Clear();
                continue;
            }
            items.Add(i[index]);
        }
        return new Success();
    }

    /// <summary>
    /// Assert <see cref="ListWritableLimit"/> of <see cref="IListWritableLimited{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Result AssertLimit<T>(this IListWritableLimited<T> i)
    {
        Throw.IfNull(i, nameof(i));
        if (i.Count > i.Limit.Count)
        {
            if (i.Limit.Count > 0)
            {
                switch (i.Limit.Action)
                {
                    case ListWritableLimitAction.Clear:
                        i.Clear();
                        break;
                    case ListWritableLimitAction.ClearAndArchive:
                        return Archive(i);
                    case ListWritableLimitAction.RemoveFirst:
                        i.RemoveAt(0);
                        break;
                    case ListWritableLimitAction.RemoveLast:
                        i.RemoveAt(i.Count - 1);
                        break;
                }
            }
            return true;
        }
        return false;
    }
}
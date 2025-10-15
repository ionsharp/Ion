using Ion.Analysis;
using Ion.Analysis;
using Ion.Core;
using Ion.Text;
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.IO;
using System.Text.Json;
using System.Text.Json;

namespace Ion.Collect;

/// <summary>An <see cref="IList{T}"/> that unifies <see cref="IListObservable{T}"/> and <see cref="IListWritable{T}"/>.</summary>
public class ListObservableWritable<T> : ListObservable<T>, IListWritable<T>, IListWritableLimited<T>
{
    /// <see cref="Region.Property"/>

    public string FileExtension
    {
        get; set
        {
            Throw.IfNull(value, nameof(value));

            field = value;
            FilePath = GetFilePath();
        }
    }

    public string FileName
    {
        get; set
        {
            Throw.IfNull(value, nameof(value));

            field = value;
            FilePath = GetFilePath();
        }
    }

    public string FilePath
    {
        get; set
        {
            field = value;
            Throw.If<ArgumentException>(field.Any(i => Path.GetInvalidPathChars().Contains(i)), nameof(FilePath));
        }
    }

    /// <summary>
    /// Preserve unreadable file by renaming it to something else. 
    /// </summary>
    public bool FilePreserve { get; set; } = true;

    public string FolderPath
    {
        get; set
        {
            Throw.IfNull(value, nameof(value));

            field = value;
            FilePath = GetFilePath();
        }
    }

    public Encoding FileEncoding { get; set; } = Encoding.ASCII;

    public JsonSerializerOptions FileOptions { get; set; } = new JsonSerializerOptions();

    ListLimit IListLimited<T>.Limit => new(Limit.Count, Limit.Action == ListWritableLimitAction.ClearAndArchive ? default : (ListLimitAction)(int)Limit);

    /// <inheritdoc cref="ListWritableLimit"/>
    public ListWritableLimit Limit
    {
        get; set
        {
            field = value;
            this.AssertLimit();
        }
    }
    = ListWritableLimit.Default;

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc/>
    public ListObservableWritable() : base() { }

    /// <inheritdoc/>
    public ListObservableWritable(params T[] i) : base(i) { }

    /// <inheritdoc/>
    public ListObservableWritable(IEnumerable<T> i) : base(i) { }

    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public ListObservableWritable(string filePath)
    {
        Throw.IfNull(filePath, nameof(filePath));

        FolderPath = Path.GetDirectoryName(filePath);

        FileName = Path.GetFileNameWithoutExtension(filePath);
        FileExtension = Path.GetExtension(filePath)[1..];
    }

    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public ListObservableWritable(string folderPath, string fileName, string fileExtension) : this([])
    {
        Throw.IfNull(folderPath, nameof(folderPath));

        Throw.IfNull(fileName, nameof(fileName));
        Throw.IfNull(fileExtension, nameof(fileExtension));

        FolderPath = folderPath;

        FileName = fileName;
        FileExtension = fileExtension;
    }

    private string GetFilePath() => $@"{FolderPath}\{FileName}.{FileExtension}";

    protected override void OnAdding(ListAddingEventArgs e)
    {
        base.OnAdding(e);
        this.AssertLimit();
    }

    /// <see cref="IListWritable"/>

    Result IListWritable.Load() => this.Load();

    Result IListWritable.Save() => this.Save();

}

/// <inheritdoc/>
public class ListObservableWritable : ListObservableWritable<Object>
{
    /// <inheritdoc/>
    public ListObservableWritable() : base() { }

    /// <inheritdoc/>
    public ListObservableWritable(params Object[] i) : base(i) { }

    /// <inheritdoc/>
    public ListObservableWritable(IEnumerable<Object> i) : base(i) { }
}
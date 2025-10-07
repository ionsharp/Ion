using Ion.Analysis;
using Ion.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Ion.Collect;

/// <inheritdoc cref="IListWritable{T}"/>
public class ListWritable<T> : ListOf<T>, IListWritable<T>, IListWritableLimited<T>
{
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

    /// <see cref="Region.Property"/>

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
    public ListWritable() : base() { }

    /// <inheritdoc/>
    public ListWritable(params T[] i) : base(i) { }

    /// <inheritdoc/>
    public ListWritable(IEnumerable<T> i) : base(i) { }

    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public ListWritable(string filePath)
    {
        Throw.IfNull(filePath, nameof(filePath));

        FolderPath = Path.GetDirectoryName(filePath);

        FileName = Path.GetFileNameWithoutExtension(filePath);
        FileExtension = Path.GetExtension(filePath)[1..];
    }

    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public ListWritable(string folderPath, string fileName, string fileExtension) : this([])
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
using Ion.Analysis;
using Ion.Analysis;
using Ion.Core;
using Ion.Serialization;
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

    /// <inheritdoc cref="IListWritable.FileEncoding"/>
    public Encoding FileEncoding { get; set; } = IListWritable.DefaultEncoding;

    /// <inheritdoc cref="IListWritable.FileExtension"/>
    public string FileExtension
    {
        get; set
        {
            Throw.IfNull(value, nameof(value));

            field = value;
            FilePath = this.GetFilePath();
        }
    }

    /// <inheritdoc cref="IListWritable.FileName"/>
    public string FileName
    {
        get; set
        {
            Throw.IfNull(value, nameof(value));

            field = value;
            FilePath = this.GetFilePath();
        }
    }

    /// <inheritdoc cref="IListWritable.FilePath"/>
    public string FilePath
    {
        get; private set
        {
            field = value;
            Throw.If<ArgumentException>(field.Any(i => Path.GetInvalidPathChars().Contains(i)), nameof(FilePath));
        }
    }

    /// <inheritdoc cref="IListWritable.FilePreserve"/>
    public bool FilePreserve { get; set; } = true;

    /// <inheritdoc cref="IListWritable.FolderPath"/>
    public string FolderPath
    {
        get; set
        {
            Throw.IfNull(value, nameof(value));

            field = value;
            FilePath = this.GetFilePath();
        }
    }

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

    /// <inheritdoc cref="IListWritable.SerializationType"/>
    public SerializationType SerializationType { get; set; } = IListWritable.DefaultSerializationType;

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc/>
    public ListObservableWritable() : base() { }

    /// <inheritdoc/>
    public ListObservableWritable(params T[] i) : base(i) { }

    /// <inheritdoc/>
    public ListObservableWritable(IEnumerable<T> i) : base(i) { }

    /// <see cref="Region.Method"/>

    /// <inheritdoc/>
    protected override void OnAdding(ListAddingEventArgs e)
    {
        base.OnAdding(e);
        this.AssertLimit();
    }

    /// <see cref="IListWritable"/>

    /// <inheritdoc cref="IListWritable.Load"/>
    Result IListWritable.Load() => this.Load();

    /// <inheritdoc cref="IListWritable.Save"/>
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
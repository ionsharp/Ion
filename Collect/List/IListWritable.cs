using Ion.Analysis;
using Ion.Serialization;
using Ion.Storage;
using Ion.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using static Ion.Analysis.Success;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IList"/> that saves and loads data to and from file.
/// </summary>
public interface IListWritable
{
    public const Encoding DefaultEncoding = Encoding.UTF8;

    public const SerializationType DefaultSerializationType = SerializationType.JSON;

    public const string Description = "A list that saves and loads data to and from file.";

    public Encoding FileEncoding { get; set; }

    public string FileExtension { get; set; }

    public string FileName { get; set; }
    
    public string FilePath { get; }

    /// <summary>
    /// Preserve unreadable file by renaming it to something else. 
    /// </summary>
    public bool FilePreserve { get; set; }

    public string FolderPath { get; set; }
     
    public SerializationType SerializationType { get; set; }

    Result Load();

    Result Save();
}

/// <inheritdoc/>
public interface IListWritable<T> : IListWritable, IList<T>, IListChanged<T>;

/// <summary>
/// Extends <see cref="IListWritable"/>.
/// </summary>
[Extend(typeof(IListWritable))]
[Extend(typeof(IListWritable<>))]
public static partial class XListWritable
{
    public static string GetFilePath(this IListWritable i) => $@"{i.FolderPath}\{i.FileName}.{i.FileExtension}";

    /// <exception cref="ArgumentNullException"/>
    public static void SetFile(this IListWritable i, string filePath)
    {
        Throw.IfNull(filePath, nameof(filePath));

        i.FolderPath = Path.GetDirectoryName(filePath);

        i.FileName = Path.GetFileNameWithoutExtension(filePath);
        i.FileExtension = Path.GetExtension(filePath)[1..];
    }

    /// <exception cref="ArgumentNullException"/>
    public static void SetFile(this IListWritable i, string folderPath, string fileName, string fileExtension)
    {
        Throw.IfNull(folderPath, nameof(folderPath));

        Throw.IfNull(fileName, nameof(fileName));
        Throw.IfNull(fileExtension, nameof(fileExtension));

        i.FolderPath = folderPath;

        i.FileName = fileName;
        i.FileExtension = fileExtension;
    }

    ///

    public static Result Deserialize<T>(this IListWritable<T> i, string filePath, out object data)
        => FileSerializer.Deserialize(filePath, out data, i.SerializationType, i.FileEncoding);

    public static Result Serialize<T>(this IListWritable<T> i, object data)
        => i.Serialize(i.FilePath, data);

    public static Result Serialize<T>(this IListWritable<T> i, string filePath, object data)
        => FileSerializer.Serialize(filePath, data, i.SerializationType, i.FileEncoding);

    [NotTested]
    public static Result Load<T>(this IListWritable<T> i)
    {
        /// Get the data from the file
        var result = i.Deserialize(i.FilePath, out object items);

        /// If successful, remove current items and add new items
        if (result && items is IEnumerable<T> j)
        {
            i.Clear();
            j.ForEach(i.Add);
        }

        /// If not successful, should the file be preserved?
        else if (i.FilePreserve)
        {
            /// If the file exists, serialization failed
            if (File.Exists(i.FilePath))
            {
                /// Rename the unreadable file so we can save new data
                Try.Do(() => File.Move(i.FilePath, FilePath.CloneName(i.FilePath, FilePath.DefaultCloneFormat, i => File.Exists(i))));
            }

            /// Serialization failed because the file doesn't exist!
            else { }
        }
        return result;
    }

    [NotTested]
    public static Result Save<T>(this IListWritable<T> i) => i.Serialize(i);
}
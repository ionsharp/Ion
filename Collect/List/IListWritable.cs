using Ion.Analysis;
using Ion.Storage;
using Ion.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IList"/> that loads and saves data to file.
/// </summary>
public interface IListWritable
{
    public const string Description = "A list that can load and save data to a file.";

    public Encoding FileEncoding { get; set; }

    public string FileExtension { get; set; }

    public string FileName { get; set; }

    public JsonSerializerOptions FileOptions { get; set; }
    
    public string FilePath { get; }

    public bool FilePreserve { get; set; }

    Result Load();

    Result Save();
}

/// <summary>
/// An <see cref="IList{T}"/> that loads and saves data to file.
/// </summary>
public interface IListWritable<T> : IListWritable, IList<T>, IListChanged<T>;

/// <summary>
/// Extends <see cref="IListWritable{T}"/>.
/// </summary>
public static class XListWritable
{
    public static Result Deserialize<T>(this IListWritable<T> i, string filePath, out object data)
    {
        try
        {
            string text = File.ReadAllText(filePath, i.FileEncoding.New());
            data = JsonSerializer.Deserialize(text, typeof(IEnumerable<T>), i.FileOptions);

            return new Success();
        }
        catch (Exception e)
        {
            data = Enumerable.Empty<T>();
            var error = new Error(e);

            Log.Write(error);
            return error;
        }
    }

    public static Result Serialize<T>(this IListWritable<T> i, object data) => i.Serialize(i.FilePath, data);

    public static Result Serialize<T>(this IListWritable<T> i, string filePath, object data)
    {
        Result result = null;
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            string text = JsonSerializer.Serialize(data, i.FileOptions);

            File.WriteAllText(filePath, text, i.FileEncoding.New());
            result = new Success();
        }
        catch (Exception e)
        {
            result = new Error(e);
            Log.Write(result);
        }
        return result;
    }

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

    public static Result Save<T>(this IListWritable<T> i) => i.Serialize(i);
}
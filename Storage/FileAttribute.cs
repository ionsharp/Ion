using System;

namespace Ion.Storage;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class FileAttribute(string name = "", string extension = "") : Attribute
{
    public string Extension { get; set; } = extension;

    public string Name { get; set; } = name;
}
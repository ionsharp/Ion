using Ion.Analysis;
using System;
using System.Collections.Generic;

namespace Ion.Core;

/// <inheritdoc cref="ISerialize"/>
public abstract record class Serializable() : Model(), ISerialize
{
    /// <see cref="Region.Event"/>

    public event EventHandler<EventArgs> Deserialized;

    public event EventHandler<EventArgs> Serialized;

    public event EventHandler<EventArgs> Serializing;

    /// <see cref="Region.Property"/>

    public bool AutoSerialize { get => Get(true); set => Set(value); }

    protected virtual IReadOnlyCollection<string> AutoSerializePropertyNames => [];

    public string FileExtension { get => Get<string>(); set => Set(value); }

    public string FileName { get => Get<string>(); set => Set(value); }

    public string FolderPath { get => Get<string>(); set => Set(value); }

    public string FilePath => $@"{FolderPath}\{FileName}.{FileExtension}";

    /// <see cref="Region.Method"/>

    protected override void OnConstructed()
    {
        base.OnConstructed();
        OnDeserialized();
    }

    public override void OnSetProperty(PropertySetEventArgs e)
    {
        base.OnSetProperty(e);
        if (AutoSerializePropertyNames?.Contains(e.PropertyName) == true)
            Serialize();

        switch (e.PropertyName)
        {
            case nameof(AutoSerialize):
                AutoSerialize.If(() => Serialize());
                break;
            case nameof(FileExtension): case nameof(FileName): case nameof(FolderPath):
                XModel.Reset(this, () => FilePath);
                break;
        }

    }

    protected virtual void OnDeserialized() => Deserialized?.Invoke(this, EventArgs.Empty);

    protected virtual void OnSerializing() => Serializing?.Invoke(this, EventArgs.Empty);

    protected virtual void OnSerialized() => Serialized?.Invoke(this, EventArgs.Empty);

    protected abstract void Serialize(string filePath, object data);

    public Result Serialize()
    {
        OnSerializing();
        var result = Try.Do(() => Serialize(FilePath, this));
        OnSerialized();

        return result;
    }
}
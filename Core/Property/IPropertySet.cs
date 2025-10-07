using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ion.Core;

/// <inheritdoc cref="INotifyPropertyChanged"/>
///<remarks>Implements <see cref="INotifyPropertyChanged"/>.</remarks>
public interface IPropertySet : INotifyPropertyChanged
{
    event PropertySetEventHandler PropertySet;

    Dictionary<string, object> SerializedProperties { get; set; }

    Dictionary<string, object> NonSerializedProperties { get; set; }

    /// <summary>Occurs when a property is set.</summary>
    void OnSetProperty(PropertySetEventArgs e);

    /// <summary>Occurs before a property is set.</summary>
    void OnSettingProperty(PropertySettingEventArgs e);
}

public class PropertySetEventArgs(string propertyName, object oldValue, object newValue) : EventArgs()
{
    public readonly string PropertyName = propertyName;

    public readonly object OldValue = oldValue;

    public object NewValue { get; set; } = newValue;
}

public delegate void PropertySetEventHandler(IPropertySet sender, PropertySetEventArgs e);

public class PropertySettingEventArgs(string propertyName, object oldValue, object newValue) : PropertySetEventArgs(propertyName, oldValue, newValue)
{
    public bool Cancel { get; set; }
}
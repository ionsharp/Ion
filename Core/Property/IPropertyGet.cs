using System;

namespace Ion.Core;

public interface IPropertyGet
{
    void OnGetProperty(PropertyGetEventData e);
}

public class PropertyGetEventData(string PropertyName, object Value) : EventArgs()
{
    public readonly string PropertyName = PropertyName;

    public readonly object Value = Value;
}
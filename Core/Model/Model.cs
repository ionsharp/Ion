using Ion.Collect;
using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Ion.Core;

///<remarks>Implements <see cref="IModel"/>.</remarks>
/// <inheritdoc cref="IModel"/>
public abstract record class Model : object, IModel
{
    protected DictionarySafe<string, ICommand> Commands { get => Get<DictionarySafe<string, ICommand>>(null, false); private set => Set(value, false); }

    ///<see cref="Region.Constructor"/>

    protected Model() : base() => OnConstructed();

    ///<see cref="Region.Method"/>

    protected virtual void OnConstructed()
    {
        (this as IPropertySet).NonSerializedProperties ??= [];
        Commands = [];
    }

    public virtual int CompareTo(object a) => 0;

    ///

    public T Get<T>(T defaultValue = default, bool serialize = true, [CallerMemberName] string propertyName = "")
        => XModel.Get(this, defaultValue, serialize, propertyName);

    public A Get<A, B>(A defaultValue, IConvert<A, B> convert, bool serialize = true, [CallerMemberName] string propertyName = "")
        => XModel.Get(this, defaultValue, convert, serialize, propertyName);

    ///

    public bool Set<T>(T newValue, bool serialize = true, bool handle = false, [CallerMemberName] string propertyName = "")
        => XModel.Set(this, newValue, serialize, handle, propertyName);

    public bool Set<T>(Expression<Func<T>> propertyName, T value, bool handle = false, bool serialize = true)
        => XModel.Set(this, propertyName, value, handle, serialize);

    public bool Set<A, B>(A newValue, IConvert<A, B> convert, bool serialize = true, bool handle = false, [CallerMemberName] string propertyName = "")
        => XModel.Set(this, newValue, convert, serialize, handle, propertyName);

    ///

    public void Reset<T>(params Expression<Func<T>>[] propertyNames)
        => XModel.Reset(this, propertyNames);

    ///<see cref="IChange"/>

    [XmlIgnore]
    public virtual bool IsChanged { get => Get(false, false); set => Set(value, false); }

    ///<see cref="ICloneable"/>

    /// <summary>Get a copy of the current instance.</summary>
    /// <returns>A shallow copy of the current instance (see <see cref="object.MemberwiseClone"/>).</returns>
    object ICloneable.Clone() => MemberwiseClone();

    /// <see cref="IFormattable"/>

    public override sealed string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public virtual string ToString(string format, IFormatProvider provider) => default;

    ///<see cref="IPropertyGet"/>

    public virtual void OnGetProperty(PropertyGetEventData e) { }

    ///<see cref="IPropertySet"/>

    [field: NonSerialized]
    protected event PropertyChangedEventHandler PropertyChanged;
    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged { add => PropertyChanged += value; remove => PropertyChanged -= value; }

    [field: NonSerialized]
    public event PropertySetEventHandler PropertySet;

    [field: NonSerialized, XmlIgnore]
    Dictionary<string, object> IPropertySet.NonSerializedProperties { get; set; } = [];

    [XmlIgnore]
    Dictionary<string, object> IPropertySet.SerializedProperties { get; set; } = [];

    public virtual void OnSetProperty(PropertySetEventArgs e) { PropertyChanged?.Invoke(this, new(e.PropertyName)); PropertySet?.Invoke(this, e); }

    public virtual void OnSettingProperty(PropertySettingEventArgs e) { }
}

/// <inheritdoc/>
public record class Model<T> : Model
{
    public virtual T Value { get => Get<T>(default); set => Set(value); }

    public Model(T Value = default) : base() => this.Value = Value;
}
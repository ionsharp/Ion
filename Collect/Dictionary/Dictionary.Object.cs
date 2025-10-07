using Ion.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Ion.Collect;

public class ObjectDictionary<Key> : Dictionary<Key, object>, IChange, IPropertyGet, IPropertySet
{
    /// <see cref="Region.Delegate"/>
    #region

    public delegate void EntryChangedHandler(object sender, Key key, object value);

    #endregion

    /// <see cref="Region.Event"/>
    #region

    public event EntryChangedHandler EntryChanged;

    #endregion

    /// <see cref="Region.Property.Indexor"/>
    #region

    [IndexerName("Item")]
    new public object this[Key key]
    {
        get => GetValue(key);
        set => SetValue(key, value);
    }

    #endregion

    /// <see cref="Region.Constructor"/>
    #region

    public ObjectDictionary() : base() => OnConstructed();

    #endregion

    /// <see cref="Region.Method"/>
    #region

    protected object GetValue(Key key)
    {
        if (!Equals(key, default(Key)))
        {
            if (ContainsKey(key))
                return base[key];
        }
        return null;
    }

    protected void SetValue(Key key, object value)
    {
        if (!ContainsKey(key))
            Add(key, null);

        base[key] = value;
        this.Reset($"Item[]");

        OnEntryChanged(key, value);
    }

    protected virtual void OnConstructed() => (this as IPropertySet).NonSerializedProperties ??= [];

    protected virtual void OnEntryChanged(Key key, object value)
        => EntryChanged?.Invoke(this, key, value);

    #endregion

    /// <see cref="IChange"/>
    #region

    [XmlIgnore]
    public virtual bool IsChanged { get => this.Get(false, false); set => this.Set(value, false); }

    #endregion

    /// <see cref="IPropertyGet"/>
    #region

    public virtual void OnGetProperty(PropertyGetEventData e) { }

    #endregion

    /// <see cref="IPropertySet"/>
    #region

    [field: NonSerialized]
    protected event PropertyChangedEventHandler PropertyChanged;
    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged { add => PropertyChanged += value; remove => PropertyChanged -= value; }

    [field: NonSerialized]
    public event PropertySetEventHandler PropertySet;

    [field: NonSerialized]
    [XmlIgnore]
    Dictionary<string, object> IPropertySet.NonSerializedProperties { get; set; } = [];

    [XmlIgnore]
    Dictionary<string, object> IPropertySet.SerializedProperties { get; set; } = [];

    public virtual void OnSetProperty(PropertySetEventArgs e) { PropertyChanged?.Invoke(this, new(e.PropertyName)); PropertySet?.Invoke(this, e); }

    public virtual void OnSettingProperty(PropertySettingEventArgs e) { }

    #endregion
}

public class ObjectDictionary<Selector, Key> : ObjectDictionary<Key>
{
    public object this[Expression<Func<Selector, object>> input]
    {
        get => GetValue(GetKey(input));
        set => SetValue(GetKey(input), value);
    }

    public ObjectDictionary() : base() { }

    static Key GetKey<X, Y>(Expression<Func<X, Y>> input) where X : Selector
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Body is MemberExpression body)
            return body.Member.Name.To<Key>();

        var result = input.Body.ToString();
        result = result[(result.IndexOf('.') + 1)..].TrimEnd([')']);
        return result.To<Key>();
    }

    public X GetValue<X>(Expression<Func<Selector, X>> input) => GetValue(GetKey(input)).As<X>();

    public Y GetValue<X, Y>(Expression<Func<X, Y>> input) where X : Selector => GetValue(GetKey(input)).As<Y>();

    public void SetValue<X>(Expression<Func<Selector, X>> input, X value) => SetValue(GetKey(input), value);

    public void SetValue<X, Y>(Expression<Func<X, Y>> input, Y value) where X : Selector => SetValue(GetKey(input), value);
}

public class ObjectDictionary() : ObjectDictionary<string>();
using Ion.Reflect;
using System.Reflection;

namespace Ion.Core;

public class PropertyBinding : Binding<IPropertySet>
{
    /// <see cref="Region.Field"/>

    private readonly Handle handle = false;

    public string SourceName { get; }

    public string TargetName { get; }

    public PropertyBindingScheme Scheme { get; }

    /// <see cref="Region.Constructor"/>

    public PropertyBinding(BindMode mode, string sourceName, IPropertySet source, string targetName, IPropertySet target, PropertyBindingScheme scheme = PropertyBindingScheme.KeepNotNull) : base(mode, source, target)
    {
        SourceName = sourceName;
        TargetName = targetName;

        Scheme = scheme;
    }

    /// <see cref="Region.Method.Private"/>

    private void OnSourcePropertyChanged(object sender, PropertySetEventArgs e) => handle.DoInternal(() =>
    {
        if (e.PropertyName == SourceName)
            Instance.SetPropertyValue(Target, TargetName, e.NewValue);
    });

    private void OnTargetPropertyChanged(object sender, PropertySetEventArgs e) => handle.DoInternal(() =>
    {
        if (e.PropertyName == TargetName)
            Instance.SetPropertyValue(Source, SourceName, e.NewValue);
    });

    /// <see cref="Region.Method.Internal"/>

    public override void Subscribe()
    {
        switch (Scheme)
        {
            case PropertyBindingScheme.KeepNotNull:
                var vSource = Instance.GetPropertyValue(Source, SourceName);
                if (vSource is not null)
                    Instance.SetPropertyValue(Target, TargetName, vSource);

                else
                {
                    var vTarget = Instance.GetPropertyValue(Target, TargetName);
                    if (vTarget is not null)
                        Instance.SetPropertyValue(Source, SourceName, vTarget);
                }
                break;
            case PropertyBindingScheme.KeepSource:
                Instance.SetPropertyValue(Target, TargetName, Instance.GetPropertyValue(Source, SourceName));
                break;
            case PropertyBindingScheme.KeepTarget:
                Instance.SetPropertyValue(Source, SourceName, Instance.GetPropertyValue(Target, TargetName));
                break;
        }
        switch (Mode)
        {
            case BindMode.OneWay:
                Source.PropertySet += OnSourceSet;
                break;
            case BindMode.OneWayToSource:
                Target.PropertySet += OnTargetSet;
                break;
            case BindMode.TwoWay:
                Source.PropertySet += OnSourceSet;
                Target.PropertySet += OnTargetSet;
                break;
        }
    }

    private void OnTargetSet(IPropertySet sender, PropertySetEventArgs e) => throw new System.NotImplementedException();
    private void OnSourceSet(IPropertySet sender, PropertySetEventArgs e) => throw new System.NotImplementedException();

    public override void Unsubscribe()
    {
        Source.PropertySet -= OnSourceSet;
        Target.PropertySet -= OnTargetSet;
    }
}
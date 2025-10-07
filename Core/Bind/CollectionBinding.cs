using Ion.Collect;
using System.Collections.Specialized;

namespace Ion.Core;

/// <summary>
/// A <see cref="Binding{T}"/> between two <see cref="IListObservable{T}"/>.
/// </summary>
public class CollectionBinding<T> : Binding<IListObservable<T>>
{
    private readonly Handle handle = false;

    public CollectionBindingScheme Scheme { get; }

    public CollectionBinding(BindMode mode, IListObservable<T> source, IListObservable<T> target, CollectionBindingScheme scheme = CollectionBindingScheme.Combine) : base(mode, source, target)
    {
        Scheme = scheme;
    }

    [NotComplete]
    private void OnSourceChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        handle.DoInternal(() =>
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    e.NewItems.ForEach<T>(i => Target.Add(i));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    e.OldItems.ForEach<T>(i => Target.Remove(i));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    e.NewItems.ForEach<T>(i => Target[Target.IndexOf(e.OldItems[0])] = i);
                    break;
                case NotifyCollectionChangedAction.Move: /// To do
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Target.Clear();
                    Target.AddRange(Source);
                    break;
            }
        });
    }

    [NotComplete]
    private void OnTargetChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        handle.DoInternal(() =>
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    e.NewItems.ForEach<T>(i => Source.Add(i));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    e.OldItems.ForEach<T>(i => Source.Remove(i));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    e.NewItems.ForEach<T>(i => Source[Source.IndexOf(e.OldItems[0])] = i);
                    break;
                case NotifyCollectionChangedAction.Move: /// To do
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Source.Clear();
                    Source.AddRange(Target);
                    break;
            }
        });
    }

    public override void Subscribe()
    {
        switch (Scheme)
        {
            case CollectionBindingScheme.Combine:
                Source.AddRange(Target);

                Target.Clear();
                Target.AddRange(Source);
                break;
            case CollectionBindingScheme.KeepSource:
                Target.Clear();
                Target.AddRange(Source);
                break;
            case CollectionBindingScheme.KeepTarget:
                Source.Clear();
                Source.AddRange(Target);
                break;
            case CollectionBindingScheme.KeepNeither:
                Source.Clear();
                Target.Clear();
                break;
            case CollectionBindingScheme.KeepGreater:
                if (Source.Count is int a && Target.Count is int b && (a != 0 || b != 0))
                {
                    if (a > b)
                    {
                        Target.Clear();
                        Target.AddRange(Source);
                    }
                    else
                    {
                        Source.Clear();
                        Source.AddRange(Target);
                    }
                }
                break;
        }
        switch (Mode)
        {
            case BindMode.OneWay:
                Source.CollectionChanged += OnSourceChanged;
                break;
            case BindMode.OneWayToSource:
                Target.CollectionChanged += OnSourceChanged;
                break;
            case BindMode.TwoWay:
                Source.CollectionChanged += OnSourceChanged;
                Target.CollectionChanged += OnTargetChanged;
                break;
        }
    }

    public override void Unsubscribe()
    {
        Source.CollectionChanged -= OnSourceChanged;
        Target.CollectionChanged -= OnTargetChanged;
    }
}
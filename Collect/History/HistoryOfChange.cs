using Ion.Reflect;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ion.Collect;

public class HistoryOfChange() : History<HistoryChange>()
{
    public void Add<TOwner, TValue>(IEnumerable<TOwner> items, string memberName, Func<TOwner, TValue> get)
    {
        var result = new List<ValueChangeOfProperty>();

        items.ForEach(i =>
        {
            var oldValue = Instance.GetPropertyValue(i, memberName);
            var newValue = get(i);

            result.Add(new(i, memberName, oldValue, newValue));
            Instance.SetPropertyValue(i, memberName, newValue);
        });

        Add(new HistoryChangePropertyMultiple([.. result]));
    }

    public void Redo() => _ = Redo(i =>
    {
        if (i is HistoryChangeProperty a)
            Instance.SetPropertyValue(a.Change.Source, a.Change.Name, a.Change.NewValue);

        if (i is HistoryChangePropertyMultiple b)
            b.Changes.ForEach(j => Instance.SetPropertyValue(j.Source, j.Name, j.NewValue));
    });

    public void Undo() => _ = Undo(i =>
    {
        if (i is HistoryChangeProperty a)
            Instance.SetPropertyValue(a.Change.Source, a.Change.Name, a.Change.OldValue);

        if (i is HistoryChangePropertyMultiple b)
            b.Changes.ForEach(j => Instance.SetPropertyValue(j.Source, j.Name, j.OldValue));
    });
}
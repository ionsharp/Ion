using System;
using System.Collections.Generic;

namespace Ion.Collect;

public class History<T>() : ListObservable<T>()
{
    /// <see cref="Region.Field"/>

    private readonly Stack<T> _Redo = new();

    /// <see cref="Region.Method"/>

    private T Peek()
    {
        T result = default;
        foreach (var i in this)
            result = i;

        return result;
    }

    private T Pop()
    {
        if (Count > 0)
        {
            var result = this.Last<T>();
            _ = Remove(result);
            return result;
        }
        return default;
    }

    ///

    new public void Add(T item)
    {
        if (Peek()?.Equals(item) != true)
        {
            base.Add(item);
            if (!this.AssertLimit())
                _Redo.Clear();
        }
    }

    new public void Clear()
    {
        _Redo.Clear();
        base.Clear();
    }

    ///

    public bool CanRedo() => _Redo.Count > 0;

    public bool CanUndo() => Count > 0;

    ///

    public bool Redo(Action<T> action)
    {
        if (CanRedo())
        {
            var i = _Redo.Pop();
            base.Add(i);
            action?.Invoke(i);
            return true;
        }
        return false;
    }

    public bool Undo(Action<T> action)
    {
        if (CanUndo())
        {
            var i = Pop();
            _Redo.Push(i);
            action?.Invoke(i);
            return true;
        }
        return false;
    }
}

public class History() : History<Object>();
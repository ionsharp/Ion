using System.Collections.Generic;

namespace Ion.Collect;

public class ListLimited<T> : ListOf<T>, IListLimited<T>
{
    /// <inheritdoc cref="ListLimit"/>
    public ListLimit Limit
    {
        get; set
        {
            field = value;
            this.AssertLimit();
        }
    }
    = ListLimit.Default;

    /// <inheritdoc/>
    public ListLimited() : base() { }

    /// <inheritdoc/>
    public ListLimited(params T[] i) : base(i) { }

    /// <inheritdoc/>
    public ListLimited(IEnumerable<T> i) : base(i) { }

    protected override void OnAdding(ListAddingEventArgs e)
    {
        base.OnAdding(e);
        this.AssertLimit();
    }
}
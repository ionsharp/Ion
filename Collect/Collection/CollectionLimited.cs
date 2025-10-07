using System.Collections.Generic;

namespace Ion.Collect;

public class CollectionLimited<T> : CollectionOf<T>, ICollectionLimited<T>
{
    /// <inheritdoc cref="CollectionLimit"/>
    public CollectionLimit Limit
    {
        get; set
        {
            field = value;
            this.AssertLimit();
        }
    }
    = CollectionLimit.Default;

    /// <inheritdoc/>
    public CollectionLimited() : base() { }

    /// <inheritdoc/>
    public CollectionLimited(params T[] i) : base(i) { }

    /// <inheritdoc/>
    public CollectionLimited(IEnumerable<T> i) : base(i) { }

    protected override void OnAdding(CollectionAddingEventArgs e)
    {
        base.OnAdding(e);
        this.AssertLimit();
    }
}
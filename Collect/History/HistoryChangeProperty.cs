namespace Ion.Collect;

public abstract record class HistoryChangeProperty(ValueChangeOfProperty Change) : HistoryChange()
{
    public override string Name => $"Changed '{Change.Source.GetType().Name}.{Change.Name}'";

    public readonly ValueChangeOfProperty Change = Change;
}
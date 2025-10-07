namespace Ion.Collect;

public record class HistoryChangePropertyMultiple(ValueChangeOfProperty[] Changes) : HistoryChange()
{
    public override string Name => $"Changed '{Changes.First<ValueChangeOfProperty>().Source.GetType().Name}.{Changes.First<ValueChangeOfProperty>().Name}' (multiple)";

    public readonly ValueChangeOfProperty[] Changes = Changes;
}
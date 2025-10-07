namespace Ion.Numeral;

/// <summary>
/// An <see cref="IVector"/> that has a related generic equivalent (if generic, <see cref="{Alias}"/> is itself).
/// </summary>
public interface IVectorAlias<Alias, Value> : IVector<Value> where Alias : IVector<Value>;
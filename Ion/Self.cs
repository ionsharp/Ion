namespace Ion;

/// <summary>
/// An <see cref="object"/> that references itself.
/// </summary>
public interface ISelf<TSelf> where TSelf : ISelf<TSelf>;
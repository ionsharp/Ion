namespace Ion.Core;

/// <summary>
/// The direction of data flow in a binding.
/// </summary>
/// <remarks>See <b><see cref="Windows.Data.BindingMode"/></b>.</remarks>
public enum BindMode
{
    OneWay, OneWayToSource, TwoWay,
}
using Ion.Input;

namespace Ion.Core;

/// <summary>
/// An <see cref="object"/> that can be checked, unchecked, or indeterminate.
/// </summary>
public interface ICheck
{
    public event CheckEventHandler Checked;

    public bool? IsChecked { get; set; }
}
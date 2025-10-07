namespace Ion.Core;

/// <summary>
/// The timing of binding source updates.
/// </summary>
/// <remarks>
/// See <b><see cref="Windows.Data.UpdateSourceTrigger"/></b>.
/// </remarks>
public enum BindTrigger
{
    /// <summary>
    /// The default <see cref="BindTrigger"/> value of the binding target property. 
    /// The default value for most dependency properties is <see cref="PropertyChanged"/>, while 
    /// the System.Windows.Controls.TextBox.Text property has a default value of <see cref="LostFocus"/>.
    /// </summary>
    Default,
    /// <summary>Updates the binding source immediately whenever the binding target property changes.</summary>
    PropertyChanged,
    /// <summary>Updates the binding source whenever the binding target element loses focus.</summary>
    LostFocus,
    /// <summary>Updates the binding source only when you call the System.Windows.Data.BindingExpression.UpdateSource method.</summary>
    Explicit
}
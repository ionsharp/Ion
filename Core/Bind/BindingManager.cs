using Ion.Collect;
using System;

namespace Ion.Core;

/// <summary>
/// A manager for <see cref="Binding{T}"/>
/// </summary>
public static class BindingManager
{
    private static readonly CollectionOf<IBinding> Bindings = [];

    /// <summary>
    /// Set the given <see cref="IBinding"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void Set(IBinding binding)
    {
        Throw.IfNull(binding, nameof(binding));

        Bindings.Add(binding);
        binding.Subscribe();
    }

    /// <summary>
    /// Unset the given <see cref="IBinding"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void Unset(IBinding binding)
    {
        Throw.IfNull(binding, nameof(binding));

        binding.Unsubscribe();
        Bindings.Remove(binding);
    }
}
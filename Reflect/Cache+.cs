using Ion;
using Ion.Collect;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion.Reflect;

/// <summary>
/// A <see cref="Cache"/> that stores <see cref="{Instance}"/> by <see cref="{Key}"/>.
/// </summary>
public class Cache<TKey, TInstance>(bool Autoget = true, Func<TKey, TInstance> Autogetter = null) : DictionarySafe<TKey, TInstance>
{
    /// <summary>
    /// Create <see cref="{Instance}"/> of data automatically upon access when doesn't exist.
    /// </summary>
    public bool Autoget { get; set; } = Autoget;

    /// <summary>
    /// Action used to create <see cref="{Instance}"/> of data automatically upon access when doesn't exist.
    /// </summary>
    public Func<TKey, TInstance> Autogetter { get; set; } = Autogetter;

    /// <summary>Action used to create <see cref="{Instance}"/> of data automatically upon access when doesn't exist.</summary>
    /// <remarks><b>Only used when <see cref="Autogetter"/> also doesn't exist, but must always exist itself.</b></remarks>
    public virtual Func<TKey, TInstance> AutogetterDefault => i => default;

    public override TInstance this[TKey i]
    {
        get
        {
            if (TryGetValue(i, out TInstance value))
                return value;

            if (Autoget)
            {
                ///Custom automatic creation
                if (Autogetter is not null)
                    Add(i, Autogetter(i));

                ///Default automatic creation
                else
                {
                    Throw.IfNull(AutogetterDefault, nameof(AutogetterDefault));
                    Add(i, AutogetterDefault(i));
                }
                return this[i];
            }

            return default;
        }
        set => this.SetOrAdd(i, value);
    }

    public Cache(Func<TKey, TInstance> Autogetter) : this(true, Autogetter) { }

    public TInstance FirstOrDefault(Predicate<TInstance> where) => this.FirstOrDefault(i => where(i.Value)).Value;

    public IEnumerable<TInstance> Select(Func<TInstance, TInstance> where) => this.Select(i => where(i.Value));
}
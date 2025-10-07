using Ion;
using Ion.Collect;
using Ion.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion.Reflect;

public class CacheByTypeList(bool Autoget = true, Func<Type, object> Autogetter = null) : ListObservable<CacheListItem>
{
    /// <summary>
    /// Create <see cref="CacheListItem"/> automatically upon access when doesn't exist.
    /// </summary>
    public bool Autoget { get; set; } = Autoget;

    /// <summary>
    /// Action used to create <see cref="CacheListItem"/> automatically upon access when doesn't exist.
    /// </summary>
    public Func<Type, object> Autogetter { get; set; } = Autogetter;

    /// <summary>Action used to create <see cref="CacheListItem"/> automatically upon access when doesn't exist.</summary>
    /// <remarks><b>Only used when <see cref="Autogetter"/> also doesn't exist, but must always exist itself.</b></remarks>
    public virtual Func<Type, object> AutogetterDefault => i => i.Create<object>();

    /// <summary>
    /// Get <see cref="CacheListItem"/> by <see cref="Type"/>.
    /// </summary>
    public CacheListItem this[Type type]
    {
        get
        {
            if (this.FirstOrDefault(i => i.Type == type) is CacheListItem firstResult)
                return firstResult;

            object value = null;
            if (Autoget)
            {
                ///Custom automatic creation
                if (Autogetter is not null)
                    value = Autogetter(type);

                ///Default automatic creation
                else
                {
                    Throw.IfNull(AutogetterDefault, nameof(AutogetterDefault));
                    value = AutogetterDefault(type);
                }

                var result = new CacheListItem(type, [value]);
                Add(result);

                return result;
            }

            return default;
        }
    }

    /// <summary>
    /// Get new instance of <see cref="CacheByTypeList"/>.
    /// </summary>
    public CacheByTypeList(Func<Type, object> Autogetter) : this(true, Autogetter) { }

    /// <summary>
    /// Add given <see cref="IEnumerable"/> by each distinct <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public void Add(IEnumerable<object> i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArgumentException>(!i.Any(), nameof(i));
        i.Select(j => j.GetType()).Distinct().ForEach(j =>
        {
            this.Remove(x => x.Type == j);

            var k = i.Where<object>(x => x.GetType() == j);
            Add(j, k);
        });
    }

    /// <summary>
    /// Add with given <see cref="IEnumerable"/> and <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public void Add(Type type, IEnumerable<object> value)
    {
        Throw.IfNull(type, nameof(type));
        Throw.IfNull(value, nameof(value));
        Throw.If<ArgumentException>(!value.Any(), nameof(value));
        Add(new CacheListItem(type, value));
    }

    /// <summary>
    /// Add given <see cref="object"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public void Add<T>(object value) => Add(typeof(T), [value]);

    /// <summary>
    /// Add given <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public void Add<T>(IEnumerable<object> values) => Add(typeof(T), values);

    /// <summary>
    /// Get if contains <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public bool Contains(Type type)
    {
        Throw.IfNull(type, nameof(type));
        return GetItem(type) is not null;
    }

    /// <summary>
    /// Get if contains <see cref="Type"/>.
    /// </summary>
    public bool Contains<T>() => Contains(typeof(T));

    /// <summary>
    /// Get <see cref="CacheListItem"/> by <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public CacheListItem GetItem(Type type)
    {
        Throw.IfNull(type, nameof(type));
        return this[type];
    }

    /// <summary>
    /// Get <see cref="CacheListItem"/> by <see cref="Type"/>.
    /// </summary>
    public CacheListItem GetItem<T>() => GetItem(typeof(T));

    /// <summary>
    /// Get first value by <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public object GetValue(Type type)
    {
        Throw.IfNull(type, nameof(type));
        return this[type].Value.First();
    }

    /// <summary>
    /// Get first value by <see cref="Type"/>.
    /// </summary>
    public T GetValue<T>() => (T)GetValue(typeof(T));

    /// <summary>
    /// Get values by <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public IEnumerable<object> GetValues(Type type)
    {
        Throw.IfNull(type, nameof(type));
        return this[type].Value;
    }

    /// <summary>
    /// Get values by <see cref="Type"/>.
    /// </summary>
    public IEnumerable<T> GetValues<T>() => GetValues(typeof(T)).Cast<T>();

    /// <summary>
    /// Remove by <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public void Remove(Type type)
    {
        Throw.IfNull(type, nameof(type));
        this.Remove(i => i.Type == type);
    }

    /// <summary>
    /// Remove by <see cref="Type"/>.
    /// </summary>
    public void Remove<T>() => Remove(typeof(T));
}

public record class CacheListItem(Type Type, IEnumerable<object> Value) : Model
{
    public Type Type { get; set; } = Type;

    public IEnumerable<object> Value { get; set; } = Value;
}
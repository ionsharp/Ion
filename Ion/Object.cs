using Ion.Numeral;
using Ion.Text;
using System;

namespace Ion;

[Extend<Boolean>]
public static partial class XObject
{
    public static bool If(this bool i, Void If, Void Else = null) { if (i) If(); else Else?.Invoke(); return i; }

    public static void IfNot(this bool i, Void If, Void Else = null) { if (!i) If(); else Else?.Invoke(); }
}

/// <summary>
/// Extends <see cref="Object"/>.
/// </summary>
[Extend<Object>]
public static partial class XObject
{
    /// As <see cref="Type"/>
    #region

    /// <summary>
    /// Get <see langword="as"/> <see cref="Type"/>.
    /// </summary>
    public static T As<T>(this object i) => i is T j ? j : default;

    #endregion

    /// Do <see cref="Void{T}"/>[]
    #region

    /// <summary>
    /// Do <see cref="Void"/> with <see cref="object"/>.
    /// </summary>
    /// <exception cref="ArrayLengthZero"/>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks><see cref="object"/> can be <see langword="null"/>.</remarks>
    public static void Do<T>(this T i, params Void<T>[] actions)
    {
        Throw.IfNull(actions, nameof(actions));
        Throw.If<ArrayLengthZero>(actions.Length == 0, nameof(actions));
        actions.ForEach(j => j(i));
    }

    #endregion

    /// If
    #region

    /// <summary>
    /// Do <see cref="Void"/> with <see cref="object"/> if given <see cref="Condition"/> is satisfied.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayLengthZero"/>
    /// <remarks><see cref="object"/> can be <see langword="null"/>.</remarks>
    public static void If<T>(this T i, Condition<T> condition, params Void<T>[] actions)
    {
        Throw.IfNull(condition, nameof(condition));
        Throw.IfNull(actions, nameof(actions));
        Throw.If<ArrayLengthZero>(actions.Length == 0, nameof(actions));
        if (condition(i)) actions.ForEach(j => j(i));
    }

    public static bool If<T>(this T[] i, Kind kind, Condition<T> condition, Void If = null, Void Else = null)
    {
        var result = kind switch { Kind.Any => false, _ => true };
        Array1D.Do(i.Length, j => result = kind switch
        {
            Kind.All => !condition(i[j]) ? false : result,
            Kind.Any => condition(i[j]) ? true : result,
            _ => condition(i[j]) ? false : result
        });
        return result.If(If, Else);
    }

    #endregion

    /// If  ≈ <see cref="Type"/>
    #region

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void If<T>(this object i, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is T) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void If<T>(this object i, Void If, Void<object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is T) If(); else Else?.Invoke(i);
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void If<T>(this object i, Void<T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is T j) If(j); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void If<T>(this object i, Void<T> If, Void<object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is T j) If(j); else Else?.Invoke(i);
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/> (and of <see cref="Type"/>) and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void If<T>(this object i, Condition<T> and, Void If, Void Else = null)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is T j && and(j)) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/> (and of <see cref="Type"/>) and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void If<T>(this object i, Condition<T> and, Void If, Void<object> Else)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is T j && and(j)) If(); else Else?.Invoke(i);
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/> (and of <see cref="Type"/>) and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void If<T>(this object i, Condition<T> and, Void<T> If, Void Else = null)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is T j && and(j)) If(j); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/> (and of <see cref="Type"/>) and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void If<T>(this object i, Condition<T> and, Void<T> If, Void<object> Else)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is T j && and(j)) If(j); else Else?.Invoke(i);
    }

    /// <summary>
    /// Get <see cref="object"/> if of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfGet<T>(this object i, Func<object, T> Else = null)
    {
        if (i is T j) return j; else return Else is null ? default : Else(i);
    }

    /// <summary>
    /// Get <see cref="object"/> if of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfGet<T>(this object i, Func<T, T> If, Func<object, T> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is T j) return If(j); else return Else is null ? default : Else(i);
    }

    /// <summary>
    /// Get <see cref="object"/> if of <see cref="Type"/> and satisfies condition.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfGet<T>(this object i, Condition<T> and, Func<object, T> Else = null)
    {
        Throw.IfNull(and, nameof(and));
        if (i is T j && and(j)) return j; else return Else is null ? default : Else(i);
    }

    /// <summary>
    /// Get <see cref="object"/> if of <see cref="Type"/> and satisfies condition.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfGet<T>(this object i, Condition<T> and, Func<T, T> If, Func<object, T> Else = null)
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is T j && and(j)) return If(j); else return Else is null ? default : Else(i);
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is of old <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfGet<Old, New>(this object i, Func<Old, New> If, Func<New> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is Old j) return If(j); else return Else is null ? default : Else();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is of old <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfGet<Old, New>(this object i, Func<Old, New> If, Func<object, New> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is Old j) return If(j); else return Else is null ? default : Else(i);
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is of old <see cref="Type"/> and satisfies condition.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfGet<Old, New>(this object i, Condition<Old> and, Func<Old, New> If, Func<object, New> Else = null)
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is Old j && and(j)) return If(j); else return Else is null ? default : Else(i);
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is of old <see cref="Type"/> and satisfies condition.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfGet<Old, New>(this object i, Condition<Old> and, Func<Old, New> If, Func<New> Else)
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is Old j && and(j)) return If(j); else return Else is null ? default : Else();
    }

    #endregion

    /// If !≈ <see cref="Type"/>
    #region

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNot<T>(this object i, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not T) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNot<T>(this object i, Void If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not T j) If(); else Else?.Invoke(j);
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNot<T>(this object i, Void<object> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not T) If(i); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNot<T>(this object i, Void<object> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not T j) If(i); else Else?.Invoke(j);
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNot<T>(this object i, Condition<object> and, Void If, Void Else = null)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and(i)) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNot<T>(this object i, Condition<object> and, Void If, Void<T> Else)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and(i)) If(); else if (i is T j) Else?.Invoke(j);
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNot<T>(this object i, Condition<object> and, Void<object> If, Void Else = null)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and(i)) If(i); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNot<T>(this object i, Condition<object> and, Void<object> If, Void<T> Else)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and(i)) If(i); else if (i is T j) Else?.Invoke(j);
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Func<T> If, Func<T> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not T) return If(); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Func<T> If, Func<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not T) return If(); else if (i is T j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Func<object, T> If, Func<T> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not T) return If(i); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Func<object, T> If, Func<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not T) return If(i); else if (i is T j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Condition and, Func<T> If, Func<T> Else = null)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and()) return If(); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Condition and, Func<T> If, Func<T, T> Else)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and()) return If(); else if (i is T j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Condition and, Func<object, T> If, Func<T> Else = null)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and()) return If(i); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Condition and, Func<object, T> If, Func<T, T> Else)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and()) return If(i); else if (i is T j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Condition<object> and, Func<T> If, Func<T> Else = null)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and(i)) return If(); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Condition<object> and, Func<T> If, Func<T, T> Else)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and(i)) return If(); else if (i is T j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Condition<object> and, Func<object, T> If, Func<T> Else = null)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and(i)) return If(i); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of <see cref="Type"/> if given <see cref="object"/> is not of <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotGet<T>(this object i, Condition<object> and, Func<object, T> If, Func<T, T> Else)
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not T && and(i)) return If(i); else if (i is T j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Func<New> If, Func<New> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not Old) return If(); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Func<New> If, Func<Old, New> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not Old) return If(); else if (i is Old j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Func<object, New> If, Func<New> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not Old) return If(i); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Func<object, New> If, Func<Old, New> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (i is not Old) return If(i); else if (i is Old j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Condition and, Func<New> If, Func<New> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        Throw.IfNull(and, nameof(and));
        if (i is not Old) return If(); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Condition and, Func<New> If, Func<Old, New> Else)
    {
        Throw.IfNull(If, nameof(If));
        Throw.IfNull(and, nameof(and));
        if (i is not Old) return If(); else if (i is Old j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Condition and, Func<object, New> If, Func<New> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        Throw.IfNull(and, nameof(and));
        if (i is not Old) return If(i); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Condition and, Func<object, New> If, Func<Old, New> Else)
    {
        Throw.IfNull(If, nameof(If));
        Throw.IfNull(and, nameof(and));
        if (i is not Old) return If(i); else if (i is Old j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Condition<object> and, Func<New> If, Func<New> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        Throw.IfNull(and, nameof(and));
        if (i is not Old) return If(); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Condition<object> and, Func<New> If, Func<Old, New> Else)
    {
        Throw.IfNull(If, nameof(If));
        Throw.IfNull(and, nameof(and));
        if (i is not Old) return If(); else if (i is Old j && Else is not null) return Else.Invoke(j); else return default;
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Condition<object> and, Func<object, New> If, Func<New> Else = null)
    {
        Throw.IfNull(If, nameof(If));
        Throw.IfNull(and, nameof(and));
        if (i is not Old) return If(i); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> is not of old <see cref="Type"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotGet<Old, New>(this object i, Condition<object> and, Func<object, New> If, Func<Old, New> Else)
    {
        Throw.IfNull(If, nameof(If));
        Throw.IfNull(and, nameof(and));
        if (i is not Old) return If(i); else if (i is Old j && Else is not null) return Else.Invoke(j); else return default;
    }

    #endregion

    /// If != <see cref="null"/>
    #region

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNotNull<T>(this T i, Void If, Void Else = null) where T : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is not null) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNotNull<T>(this T i, Void<T> If, Void Else = null) where T : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is not null) If(i); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNotNull<T>(this T i, Condition and, Void If, Void Else = null) where T : class
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not null && and()) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNotNull<T>(this T i, Condition and, Void<T> If, Void Else = null) where T : class
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not null && and()) If(i); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNotNull<T>(this T i, Condition<T> and, Void If, Void Else = null) where T : class
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not null && and(i)) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNotNull<T>(this T i, Condition<T> and, Void<T> If, Void Else = null) where T : class
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is not null && and(i)) If(i); else Else?.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> if not <see langword="null"/>.
    /// </summary>
    public static T IfNotNullGet<T>(this T i, Func<T> Else = null) where T : class
    {
        if (i is not null) return i; else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> if not <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotNullGet<T>(this T i, Func<T, T> If, Func<T> Else = null) where T : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is not null) return i; else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> if not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotNullGet<T>(this T i, Condition and, Func<T> Else = null) where T : class
    {
        Throw.IfNull(and, nameof(and));
        if (i is not null) return i; else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> if not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotNullGet<T>(this T i, Condition and, Func<T, T> If, Func<T> Else = null) where T : class
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is not null) return i; else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> if not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotNullGet<T>(this T i, Condition<T> and, Func<T> Else = null) where T : class
    {
        Throw.IfNull(and, nameof(and));
        if (i is not null) return i; else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> if not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNotNullGet<T>(this T i, Condition<T> and, Func<T, T> If, Func<T> Else = null) where T : class
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is not null) return i; else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if <see cref="object"/> of old <see cref="Type"/> is not <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotNullGet<Old, New>(this Old i, Func<Old, New> If, Func<New> Else = null) where Old : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is not null) return If(i); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if <see cref="object"/> of old <see cref="Type"/> is not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotNullGet<Old, New>(this Old i, Condition and, Func<Old, New> If, Func<New> Else = null) where Old : class
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is not null && and()) return If(i); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get <see cref="object"/> of new <see cref="Type"/> if <see cref="object"/> of old <see cref="Type"/> is not <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNotNullGet<Old, New>(this Old i, Condition<Old> and, Func<Old, New> If, Func<New> Else = null) where Old : class
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is not null && and(i)) return If(i); else return Else is null ? default : Else.Invoke();
    }

    #endregion

    /// If == <see cref="null"/>
    #region

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(this T i, Void If, Void Else = null) where T : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is null) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(this T i, Void If, Void<object> Else) where T : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is null) If(); else Else?.Invoke(i);
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(this T i, Condition and, Void If, Void Else = null) where T : class
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is null && and()) If(); else Else?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> if <see cref="object"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(this T i, Condition and, Void If, Void<object> Else) where T : class
    {
        Throw.IfNull([and, If], [nameof(and), nameof(If)]);
        if (i is null && and()) If(); else Else?.Invoke(i);
    }

    /// <summary>
    /// Get an <see cref="object"/> if given <see cref="object"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNullGet<T>(this T i, Func<T> If, Func<T, T> Else = null) where T : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is null) return If(); else return Else is null ? default : Else.Invoke(i);
    }

    /// <summary>
    /// Get an <see cref="object"/> if given <see cref="object"/> is <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T IfNullGet<T>(this T i, Condition and, Func<T> If, Func<T, T> Else = null) where T : class
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is null && and()) return If(); else return Else is null ? default : Else.Invoke(i);
    }

    /// <summary>
    /// Get an <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> of old <see cref="Type"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNullGet<Old, New>(this Old i, Func<New> If, Func<New> Else = null) where Old : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is null) return If(); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get an <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> of old <see cref="Type"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNullGet<Old, New>(this Old i, Func<New> If, Func<Old, New> Else = null) where Old : class
    {
        Throw.IfNull(If, nameof(If));
        if (i is null) return If(); else return Else is null ? default : Else.Invoke(i);
    }

    /// <summary>
    /// Get an <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> of old <see cref="Type"/> is <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNullGet<Old, New>(this Old i, Condition and, Func<New> If, Func<New> Else = null) where Old : class
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is null && and()) return If(); else return Else is null ? default : Else.Invoke();
    }

    /// <summary>
    /// Get an <see cref="object"/> of new <see cref="Type"/> if given <see cref="object"/> of old <see cref="Type"/> is <see langword="null"/> and satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static New IfNullGet<Old, New>(this Old i, Condition and, Func<New> If, Func<Old, New> Else = null) where Old : class
    {
        Throw.IfNull(and, nameof(and));
        Throw.IfNull(If, nameof(If));
        if (i is null && and()) return If(); else return Else is null ? default : Else.Invoke(i);
    }

    #endregion

    /// If (==)
    #region

    /// <summary>
    /// Do given <see cref="Void"/> if both <see cref="object"/> are equal.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfEqual(this object i, object j, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual(this object i, object j, Void If, Void<object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual(this object i, object j, Void If, Void<object, object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else(i, j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual(this object i, object j, Void<object> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual(this object i, object j, Void<object> If, Void<object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual(this object i, object j, Void<object> If, Void<object, object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else(i, j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual(this object i, object j, Void<object, object> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual(this object i, object j, Void<object, object> If, Void<object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual(this object i, object j, Void<object, object> If, Void<object, object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else(i, j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else(i, (T)j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void<T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void<T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void<T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else(i, (T)j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void<T, object> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void<T, object> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, object j, Void<T, object> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else(i, (T)j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else((T)i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else((T)i, j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void<T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If((T)i); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void<T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If((T)i); else Else((T)i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void<T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If((T)i); else Else((T)i, j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void<object, T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void<object, T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else((T)i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this object i, T j, Void<object, T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else((T)i, j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(); else Else(i, j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void<T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void<T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void<T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i); else Else(i, j);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void<T, T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else();
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void<T, T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else(i);
    }

    /// <inheritdoc cref="IfEqual(object, object, Void, Void)"/>
    public static void IfEqual<T>(this T i, T j, Void<T, T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (Equals(i, j)) If(i, j); else Else(i, j);
    }

    #endregion

    /// If (!=)
    #region

    /// <summary>
    /// Do given <see cref="Void"/> if both <see cref="object"/> are not equal.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNotEqual(this object i, object j, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual(this object i, object j, Void If, Void<object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual(this object i, object j, Void If, Void<object, object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else(i, j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual(this object i, object j, Void<object> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual(this object i, object j, Void<object> If, Void<object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual(this object i, object j, Void<object> If, Void<object, object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else(i, j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual(this object i, object j, Void<object, object> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual(this object i, object j, Void<object, object> If, Void<object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual(this object i, object j, Void<object, object> If, Void<object, object> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else(i, j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else(i, (T)j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void<T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void<T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void<T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else(i, (T)j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void<T, object> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void<T, object> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, object j, Void<T, object> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else(i, (T)j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else((T)i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else((T)i, j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void<T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If((T)i); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void<T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If((T)i); else Else((T)i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void<T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If((T)i); else Else((T)i, j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void<object, T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void<object, T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else((T)i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this object i, T j, Void<object, T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else((T)i, j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(); else Else(i, j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void<T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void<T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void<T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i); else Else(i, j);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void<T, T> If, Void Else = null)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else();
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void<T, T> If, Void<T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else(i);
    }

    /// <inheritdoc cref="IfNotEqual(object, object, Void, Void)"/>
    public static void IfNotEqual<T>(this T i, T j, Void<T, T> If, Void<T, T> Else)
    {
        Throw.IfNull(If, nameof(If));
        if (!Equals(i, j)) If(i, j); else Else(i, j);
    }

    #endregion

    /// To
    #region

    /// <summary>
    /// Get <see cref="T"/> from <see cref="object"/>.
    /// </summary>
    /// <exception cref="InvalidCastException"/>
    public static T To<T>(this object i) => (T)i;

    /// <summary>
    /// Get <see cref="TNew"/> from <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew To<TOld, TNew>(this TOld i, Func<TOld, TNew> j)
    {
        Throw.IfNull(j, nameof(j));
        return j(i);
    }

    #endregion

    /// To <see cref="Array"/>
    #region

    /// <summary>
    /// Get <see cref="T"/>[] with given <b>length</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLess"/>/>
    public static T[] ToArray<T>(this T i, int length)
        => i.ToArray<T>(length, j => j);

    /// <summary>
    /// Get <see cref="T"/>[] with given <b>length</b> and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsLess"/>/>
    public static T[] ToArray<T>(this T i, int length, Func<T, T> value)
        => i.ToArray<T>(length, (_, j) => value(j));

    /// <summary>
    /// Get <see cref="T"/>[] with given <b>length</b> and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsLess"/>/>
    public static T[] ToArray<T>(this T i, int length, Func<int, T, T> value)
    {
        Throw.IfNull(value, nameof(value));
        Throw.IfLess(length, 1, nameof(length));
        return Array1D.Get(length, j => value(j, i));
    }

    /// <summary>
    /// Get <see cref="TNew"/>[] with given <b>length</b> and <b>value</b> from <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsLess"/>/>
    public static TNew[] ToArray<TOld, TNew>(this TOld i, int length, Func<TOld, TNew> value)
        => i.ToArray(length , (_, j) => value(j));

    /// <summary>
    /// Get <see cref="TNew"/>[] with given <b>length</b> and <b>value</b> from <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsLess"/>/>
    public static TNew[] ToArray<TOld, TNew>(this TOld i, int length, Func<int, TOld, TNew> value)
    {
        Throw.IfNull(value, nameof(value));
        Throw.IfLess(length, 1, nameof(length));
        return Array1D.Get(length, j => value(j, i));
    }

    #endregion

    /// To <see cref="string"/>
    #region

    /// <summary>
    /// Get <see cref="object"/> as <see cref="string"/> with given <see cref="Casing"/>.
    /// </summary>
    public static string ToString(this object i, Casing casing)
    {
        var j = $"{i}";
        return casing switch { Casing.Capitalized => j.Capitalize(), Casing.Lower => j.ToLower(), Casing.Upper => j.ToUpper(), _ => j };
    }

    #endregion
}
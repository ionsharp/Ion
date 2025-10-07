using Ion.Numeral;
using Ion.Reflect;
using System;
using System.Numerics;

namespace Ion;

/// <summary>
/// <see cref="Throw"/> an <see cref="Exception"/>.
/// </summary>
[Private]
public static class Throw
{
    /// <see cref="Region.Field"/>

    private const string DefaultMessage = "{0}.";

    private const string DefaultName = "{0} '{1}'";

    public static string Message(string name, string aName, string message)
        => string.Format(DefaultMessage, string.Format(message, string.Format(DefaultName, name, aName)));

    public static string Message(string name, string aName, string message, string bName, string messageDefault = "")
        => (aName is null || bName is null) && messageDefault is not null ? messageDefault : string.Format(DefaultMessage, string.Format(message, string.Format(DefaultName, name, aName), string.Format(DefaultName, name, bName)));

    /// <see cref="Region.Method.Private"/>

    private static void _If(bool condition, Void action)  { if (condition) { action(); } else { } }

    private static bool _If<T>(T[] i, Kind kind, Condition<T> condition = null)
    {
        var result = kind switch { Kind.Any => false, _ => true };
        condition = condition is null ? x => x is null : condition;
        for (var j = 0; j < i.Length; j++) { result = kind switch { Kind.All => !condition(i[j]) ? false : result, Kind.Any => condition(i[j]) ? true : result, _ => condition(i[j]) ? false : result }; }
        return result;
    }

    private static void _If<T>(T[] i, Kind kind, Condition<T> condition, Void action) => _If(_If(i, kind, condition), action);

    /// <see cref="Exception"/>

    /// <summary><see cref="Throw"/> if <b>condition</b> is <see langword="true"/>.</summary>
    /// <exception cref="Exception"/>
    public static void If(bool condition, string message = null, Exception e = null)
        => _If(condition, () => throw new Exception(message, e));

    /// <summary><see cref="Throw"/> if <b>condition</b> is <see langword="true"/>.</summary>
    /// <exception cref="Exception"/>
    public static void If<T>(bool condition) where T : Exception, new()
        => _If(condition, () => throw new T());

    /// <summary><see cref="Throw"/> if <b>condition</b> is <see langword="true"/>.</summary>
    /// <exception cref="Exception"/>
    public static void If<T>(bool condition, params object[] parameters) where T : Exception
        => _If(condition, () => throw typeof(T).Create<T>(parameters));

    /// <summary><see cref="Throw"/> if <b>condition</b> is <see langword="true"/>.</summary>
    /// <exception cref="Exception"/>
    public static void If(Condition condition, string message = null, Exception e = null)
        => _If(condition(), () => throw new Exception(message, e));

    /// <summary><see cref="Throw"/> if <b>condition</b> is <see langword="true"/>.</summary>
    /// <exception cref="Exception"/>
    public static void If<T>(Condition condition) where T : Exception, new()
        => _If(condition(), () => throw new T());

    /// <summary><see cref="Throw"/> if <b>condition</b> is <see langword="true"/>.</summary>
    /// <exception cref="Exception"/>
    public static void If<T>(Condition condition, params object[] parameters) where T : Exception
        => _If(condition(), () => throw typeof(T).Create<T>(parameters));

    /// <summary><see cref="Throw"/> if <b>i</b> = <b>j</b>.</summary>
    /// <exception cref="EqualException"/>
    public static void IfEqual(object i, object j)
        => If<EqualException>(Equals(i, j));

    /// <summary><see cref="Throw"/> if <b>i</b> = <b>j</b>.</summary>
    /// <exception cref="EqualException"/>
    public static void IfEqual(object i, object j, string message, Exception e = null)
        => If<EqualException>(Equals(i, j), message, e);

    /// <summary><see cref="Throw"/> if <b>i</b> = <b>j</b>.</summary>
    /// <exception cref="EqualException"/>
    public static void IfEqual(object i, object j, string iName, string jName, Exception e = null)
        => If<EqualException>(Equals(i, j), iName, jName, e);

    /// <summary><see cref="Throw"/> if <b>i</b> = <b>j</b>.</summary>
    /// <exception cref="Exception"/>
    public static void IfEqual<T>(object i, object j, params object[] parameters)
        where T : Exception => If<T>(Equals(i, j), parameters);

    /// <summary><see cref="Throw"/> if <b>i</b> != <b>j</b>.</summary>
    /// <exception cref="NotEqualException"/>
    public static void IfNotEqual(object i, object j)
        => If<NotEqualException>(!Equals(i, j));

    /// <summary><see cref="Throw"/> if <b>i</b> != <b>j</b>.</summary>
    /// <exception cref="NotEqualException"/>
    public static void IfNotEqual(object i, object j, string message, Exception e = null)
        => If<NotEqualException>(!Equals(i, j), message, e);

    /// <summary><see cref="Throw"/> if <b>i</b> != <b>j</b>.</summary>
    /// <exception cref="NotEqualException"/>
    public static void IfNotEqual(object i, object j, string iName, string jName, Exception e = null)
        => If<NotEqualException>(!Equals(i, j), iName, jName, e);

    /// <summary><see cref="Throw"/> if <b>i</b> != <b>b</b>.</summary>
    /// <exception cref="Exception"/>
    public static void IfNotEqual<T>(object i, object j, params object[] parameters)
        where T : Exception => If<T>(!Equals(i, j), parameters);

    /// <see cref="ArgumentNullException"/>

    /// <summary><see cref="Throw"/> if <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull(object i, string name = null, string message = null)
        => _If(i is null, () => throw new ArgumentNullException(name, message));

    /// <summary><see cref="Throw"/> if <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull(object i, string message, Exception e)
        => _If(i is null, () => throw new ArgumentNullException(message, e));

    /// <summary><see cref="Throw"/> if any, all, or none are <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull(object[] i, string[] name, Kind kind = Kind.Any)
    {
        if (_If(i, kind)) throw new ArgumentNullException(name[0], $"Object '{name[0]}' is null.");
    }

    /// <summary><see cref="Throw"/> if <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(object i) 
        where T : ArgumentNullException, new()
        => _If(i is null, () => throw new T());

    /// <summary><see cref="Throw"/> if <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(object i, string name = null, string message = null) 
        where T : ArgumentNullException
        => _If(i is null, () => typeof(T).Create<T>(name, message));

    /// <summary><see cref="Throw"/> if <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(object i, string message, Exception e)
        where T : ArgumentNullException
        => _If(i is null, () => typeof(T).Create<T>(message, e));

    /// <summary><see cref="Throw"/> if <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(object i, params object[] parameters) 
        where T : ArgumentNullException
        => _If(i is null, () => typeof(T).Create<T>(parameters));

    /// <summary><see cref="Throw"/> if any, all, or none are <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(object[] i, Kind kind = Kind.Any) where T : ArgumentNullException, new()
    {
        if (_If(i, kind)) throw new T();
    }

    /// <summary><see cref="Throw"/> if any, all, or none are <see langword="null"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNull<T>(object[] i, string[] name, Kind kind = Kind.Any, params object[] parameters) where T : ArgumentNullException
    {
        if (_If(i, kind)) throw typeof(T).Create<T>(i, name, parameters);
    }

    /// <summary><see cref="Throw"/> if <see cref="string.IsNullOrEmpty(string?)"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNullOrEmpty(object i, string message = null, Exception e = null)
        => _If(i is null || (i is string j && j.IsEmpty()), () => throw new ArgumentNullException(message, e));

    /// <summary><see cref="Throw"/> if <see cref="string.IsNullOrEmpty(string?)"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNullOrEmpty<T>(object i) where T : ArgumentNullException, new()
        => _If(i is null || (i is string j && j.IsEmpty()), () => throw new T());

    /// <summary><see cref="Throw"/> if <see cref="string.IsNullOrEmpty(string?)"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNullOrEmpty<T>(object i, string message, Exception e = null) where T : ArgumentNullException, new()
        => _If(i is null || (i is string j && j.IsEmpty()), () => typeof(T).Create<T>(message, e));

    /// <summary><see cref="Throw"/> if <see cref="string.IsNullOrWhiteSpace(string?)"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNullOrWhite(object i, string message = null, Exception e = null)
        => _If(i is null || (i is string j && j.IsWhite()), () => throw new ArgumentNullException(message, e));

    /// <summary><see cref="Throw"/> if <see cref="string.IsNullOrWhiteSpace(string?)"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNullOrWhite<T>(object i) where T : ArgumentNullException, new()
        => _If(i is null || (i is string j && j.IsWhite()), () => throw new T());

    /// <summary><see cref="Throw"/> if <see cref="string.IsNullOrWhiteSpace(string?)"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public static void IfNullOrWhite<T>(object i, string message, Exception e = null) where T : ArgumentNullException, new()
        => _If(i is null || (i is string j && j.IsWhite()), () => typeof(T).Create<T>(message, e));

    /// <see cref="NumberArgumentIs"/>
    #region

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsCanonical{T}(T)"/></summary>
    /// <exception cref="ArgumentIsCanonical"/>
    public static void IfCanonical<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsCanonical(), () => throw new ArgumentIsCanonical(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsCanonical{T}(T)"/></summary>
    /// <exception cref="ArgumentIsCanonical"/>
    public static void IfCanonical<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsCanonical(), () => throw new ArgumentIsCanonical(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsComplex{T}(T)"/></summary>
    /// <exception cref="ArgumentIsComplex"/>
    public static void IfComplex<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsComplex(), () => throw new ArgumentIsComplex(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsComplex{T}(T)"/></summary>
    /// <exception cref="ArgumentIsComplex"/>
    public static void IfComplex<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsComplex(), () => throw new ArgumentIsComplex(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsEven{T}(T)"/></summary>
    /// <exception cref="ArgumentIsEven"/>
    public static void IfEven<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsEven(), () => throw new ArgumentIsEven(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsEven{T}(T)"/></summary>
    /// <exception cref="ArgumentIsEven"/>
    public static void IfEven<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsEven(), () => throw new ArgumentIsEven(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsFinite{T}(T)"/></summary>
    /// <exception cref="ArgumentIsFinite"/>
    public static void IfFinite<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsFinite(), () => throw new ArgumentIsFinite(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsFinite{T}(T)"/></summary>
    /// <exception cref="ArgumentIsFinite"/>
    public static void IfFinite<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsFinite(), () => throw new ArgumentIsFinite(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsGreater{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsGreater"/>
    public static void IfGreater<T>(T i, T j, string name)
        where T : INumber<T>
        => _If(i.IsGreater(j), () => throw new ArgumentIsGreater(name, i, j));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsGreater{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsGreater"/>
    public static void IfGreater<T>(Kind kind, T[] i, T j, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsGreater(j), () => throw new ArgumentIsGreater(name, i, j, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsGreaterOrEqual{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsGreaterOrEqual"/>
    public static void IfGreaterOrEqual<T>(T i, T j, string name)
        where T : INumber<T>
        => _If(i.IsGreaterOrEqual(j), () => throw new ArgumentIsGreaterOrEqual(name, i, j));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsGreaterOrEqual{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsGreaterOrEqual"/>
    public static void IfGreaterOrEqual<T>(Kind kind, T[] i, T j, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsGreaterOrEqual(j), () => throw new ArgumentIsGreaterOrEqual(name, i, j, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsImaginary{T}(T)"/></summary>
    /// <exception cref="ArgumentIsImaginary"/>
    public static void IfImaginary<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsImaginary(), () => throw new ArgumentIsImaginary(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsImaginary{T}(T)"/></summary>
    /// <exception cref="ArgumentIsImaginary"/>
    public static void IfImaginary<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsImaginary(), () => throw new ArgumentIsImaginary(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsInfinity"/>
    public static void IfInfinity<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsInfinity(), () => throw new ArgumentIsInfinity(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsInfinity"/>
    public static void IfInfinity<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsInfinity(), () => throw new ArgumentIsInfinity(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsInteger{T}(T)"/></summary>
    /// <exception cref="ArgumentIsInteger"/>
    public static void IfInteger<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsInteger(), () => throw new ArgumentIsInteger(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsInteger{T}(T)"/></summary>
    /// <exception cref="ArgumentIsInteger"/>
    public static void IfInteger<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsInteger(), () => throw new ArgumentIsInteger(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsLess{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsLess"/>
    public static void IfLess<T>(T i, T j, string name)
        where T : INumber<T>
        => _If(i.IsLess(j), () => throw new ArgumentIsLess(name, i, j));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsLess{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsLess"/>
    public static void IfLess<T>(Kind kind, T[] i, T j, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsLess(j), () => throw new ArgumentIsLess(name, i, j, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsLessOrEqual{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    public static void IfLessOrEqual<T>(T i, T j, string name)
        where T : INumber<T>
        => _If(i.IsLessOrEqual(j), () => throw new ArgumentIsLessOrEqual(name, i, j));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsLessOrEqual{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    public static void IfLessOrEqual<T>(Kind kind, T[] i, T j, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsLessOrEqual(j), () => throw new ArgumentIsLessOrEqual(name, i, j, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNegative{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNegative"/>
    public static void IfNegative<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNegative(), () => throw new ArgumentIsNegative(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNegative{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNegative"/>
    public static void IfNegative<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsNegative(), () => throw new ArgumentIsNegative(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNegativeInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNegativeInfinity"/>
    public static void IfNegativeInfinity<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNegativeInfinity(), () => throw new ArgumentIsNegativeInfinity(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNegativeInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNegativeInfinity"/>
    public static void IfNegativeInfinity<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsNegativeInfinity(), () => throw new ArgumentIsNegativeInfinity(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNegativeOne{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNegativeOne"/>
    public static void IfNegativeOne<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNegativeOne(), () => throw new ArgumentIsNegativeOne(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNegativeOne{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNegativeOne"/>
    public static void IfNegativeOne<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsNegativeOne(), () => throw new ArgumentIsNegativeOne(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNegativeOrZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNegativeOrZero"/>
    public static void IfNegativeOrZero<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNegativeOrZero(), () => throw new ArgumentIsNegativeOrZero(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNegativeOrZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNegativeOrZero"/>
    public static void IfNegativeOrZero<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsNegativeOrZero(), () => throw new ArgumentIsNegativeOrZero(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNormal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNormal"/>
    public static void IfNormal<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNormal(), () => throw new ArgumentIsNormal(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNormal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNormal"/>
    public static void IfNormal<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsNormal(), () => throw new ArgumentIsNormal(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNormalized{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNormalized"/>
    public static void IfNormalized<T>(T i, string name)
        where T : IMinMaxValue<T>, INumber<T>
        => _If(i.IsNormalized(), () => throw new ArgumentIsNormalized(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNormalized{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNormalized"/>
    public static void IfNormalized<T>(Kind kind, T[] i, string[] name)
        where T : IMinMaxValue<T>, INumber<T>
        => _If(i, kind, j => j.IsNormalized(), () => throw new ArgumentIsNormalized(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNaN{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNumber"/>
    public static void IfNumber<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsNaN(), () => throw new ArgumentIsNumber(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNaN{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNumber"/>
    public static void IfNumber<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsNaN(), () => throw new ArgumentIsNumber(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsOdd{T}(T)"/></summary>
    /// <exception cref="ArgumentIsOdd"/>
    public static void IfOdd<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsOdd(), () => throw new ArgumentIsOdd(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsOdd{T}(T)"/></summary>
    /// <exception cref="ArgumentIsOdd"/>
    public static void IfOdd<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsOdd(), () => throw new ArgumentIsOdd(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.OfRange{T}(T, T, T)"/></summary>
    /// <exception cref="ArgumentIsOfRange"/>
    public static void IfOfRange<T>(T i, T minimum, T maximum, string name)
        where T : IMinMaxValue<T>, INumber<T>
        => _If(i.OfRange(minimum, maximum), () => throw new ArgumentIsOfRange(name, i, minimum, maximum));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.OfRange{T}(T, T, T)"/></summary>
    /// <exception cref="ArgumentIsOfRange"/>
    public static void IfOfRange<T>(Kind kind, T[] i, T minimum, T maximum, string[] name)
        where T : IMinMaxValue<T>, INumber<T>
        => _If(i, kind, j => j.OfRange(minimum, maximum), () => throw new ArgumentIsOfRange(name, i, minimum, maximum, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsPositive{T}(T)"/></summary>
    /// <exception cref="ArgumentIsPositive"/>
    public static void IfPositive<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsPositive(), () => throw new ArgumentIsPositive(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsPositive{T}(T)"/></summary>
    /// <exception cref="ArgumentIsPositive"/>
    public static void IfPositive<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsPositive(), () => throw new ArgumentIsPositive(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsPositiveInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsPositiveInfinity"/>
    public static void IfPositiveInfinity<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsPositiveInfinity(), () => throw new ArgumentIsPositiveInfinity(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsPositiveInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsPositiveInfinity"/>
    public static void IfPositiveInfinity<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsPositiveInfinity(), () => throw new ArgumentIsPositiveInfinity(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsPositiveOne{T}(T)"/></summary>
    /// <exception cref="ArgumentIsPositiveOne"/>
    public static void IfPositiveOne<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsPositiveOne(), () => throw new ArgumentIsPositiveOne(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsPositiveOne{T}(T)"/></summary>
    /// <exception cref="ArgumentIsPositiveOne"/>
    public static void IfPositiveOne<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsPositiveOne(), () => throw new ArgumentIsPositiveOne(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsPositiveOrZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsPositiveOrZero"/>
    public static void IfPositiveOrZero<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsPositiveOrZero(), () => throw new ArgumentIsPositiveOrZero(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsPositiveOrZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsPositiveOrZero"/>
    public static void IfPositiveOrZero<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsPositiveOrZero(), () => throw new ArgumentIsPositiveOrZero(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsReal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsReal"/>
    public static void IfReal<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsReal(), () => throw new ArgumentIsReal(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsReal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsReal"/>
    public static void IfReal<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsReal(), () => throw new ArgumentIsReal(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsSubnormal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsSubnormal"/>
    public static void IfSubnormal<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsSubnormal(), () => throw new ArgumentIsSubnormal(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsSubnormal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsSubnormal"/>
    public static void IfSubnormal<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsSubnormal(), () => throw new ArgumentIsSubnormal(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsZero"/>
    public static void IfZero<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsZero(), () => throw new ArgumentIsZero(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsZero"/>
    public static void IfZero<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsZero(), () => throw new ArgumentIsZero(name, kind));

    #endregion

    /// <see cref="NumberArgumentIsNot"/>
    #region

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsCanonical{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotCanonical"/>
    public static void IfNotCanonical<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsCanonical(), () => throw new ArgumentIsNotCanonical(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsCanonical{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotCanonical"/>
    public static void IfNotCanonical<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsCanonical(), () => throw new ArgumentIsNotCanonical(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsComplex{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotComplex"/>
    public static void IfNotComplex<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsComplex(), () => throw new ArgumentIsNotComplex(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsComplex{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotComplex"/>
    public static void IfNotComplex<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsComplex(), () => throw new ArgumentIsNotComplex(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsEven{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotEven"/>
    public static void IfNotEven<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsEven(), () => throw new ArgumentIsNotEven(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsEven{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotEven"/>
    public static void IfNotEven<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsEven(), () => throw new ArgumentIsNotEven(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsFinite{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotFinite"/>
    public static void IfNotFinite<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsFinite(), () => throw new ArgumentIsNotFinite(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsFinite{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotFinite"/>
    public static void IfNotFinite<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsFinite(), () => throw new ArgumentIsNotFinite(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsGreater{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsNotGreater"/>
    public static void IfNotGreater<T>(T i, T j, string name)
        where T : INumber<T>
        => _If(i.IsGreater(j), () => throw new ArgumentIsNotGreater(name, i, j));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsGreater{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsNotGreater"/>
    public static void IfNotGreater<T>(Kind kind, T[] i, T j, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsGreater(j), () => throw new ArgumentIsNotGreater(name, i, j, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsGreaterOrEqual{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsNotGreaterOrEqual"/>
    public static void IfNotGreaterOrEqual<T>(T i, T j, string name)
        where T : INumber<T>
        => _If(i.IsGreaterOrEqual(j), () => throw new ArgumentIsNotGreaterOrEqual(name, i, j));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsGreaterOrEqual{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsNotGreaterOrEqual"/>
    public static void IfNotGreaterOrEqual<T>(Kind kind, T[] i, T j, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsGreaterOrEqual(j), () => throw new ArgumentIsNotGreaterOrEqual(name, i, j, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsImaginary{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotImaginary"/>
    public static void IfNotImaginary<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsImaginary(), () => throw new ArgumentIsNotImaginary(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsImaginary{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotImaginary"/>
    public static void IfNotImaginary<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsImaginary(), () => throw new ArgumentIsNotImaginary(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotInfinity"/>
    public static void IfNotInfinity<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsInfinity(), () => throw new ArgumentIsNotInfinity(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotInfinity"/>
    public static void IfNotInfinity<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsInfinity(), () => throw new ArgumentIsNotInfinity(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsInteger{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotInteger"/>
    public static void IfNotInteger<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsInteger(), () => throw new ArgumentIsNotInteger(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsInteger{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotInteger"/>
    public static void IfNotInteger<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsInteger(), () => throw new ArgumentIsNotInteger(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsLess{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsNotLess"/>
    public static void IfNotLess<T>(T i, T j, string name)
        where T : INumber<T>
        => _If(i.IsLess(j), () => throw new ArgumentIsNotLess(name, i, j));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsLess{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsNotLess"/>
    public static void IfNotLess<T>(Kind kind, T[] i, T j, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsLess(j), () => throw new ArgumentIsNotLess(name, i, j, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsLessOrEqual{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsNotLessOrEqual"/>
    public static void IfNotLessOrEqual<T>(T i, T j, string name)
        where T : INumber<T>
        => _If(i.IsLessOrEqual(j), () => throw new ArgumentIsNotLessOrEqual(name, i, j));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsLessOrEqual{T}(T, T)"/></summary>
    /// <exception cref="ArgumentIsNotLessOrEqual"/>
    public static void IfNotLessOrEqual<T>(Kind kind, T[] i, T j, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsLessOrEqual(j), () => throw new ArgumentIsNotLessOrEqual(name, i, j, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNegative{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNegative"/>
    public static void IfNotNegative<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNegative(), () => throw new ArgumentIsNotNegative(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNegative{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNegative"/>
    public static void IfNotNegative<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsNegative(), () => throw new ArgumentIsNotNegative(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNegativeInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNegativeInfinity"/>
    public static void IfNotNegativeInfinity<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNegativeInfinity(), () => throw new ArgumentIsNotNegativeInfinity(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNegativeInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNegativeInfinity"/>
    public static void IfNotNegativeInfinity<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsNegativeInfinity(), () => throw new ArgumentIsNotNegativeInfinity(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNegativeOne{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNegativeOne"/>
    public static void IfNotNegativeOne<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNegativeOne(), () => throw new ArgumentIsNotNegativeOne(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNegativeOne{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNegativeOne"/>
    public static void IfNotNegativeOne<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsNegativeOne(), () => throw new ArgumentIsNotNegativeOne(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNegativeOrZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNegativeOrZero"/>
    public static void IfNotNegativeOrZero<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNegativeOrZero(), () => throw new ArgumentIsNotNegativeOrZero(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNegativeOrZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNegativeOrZero"/>
    public static void IfNotNegativeOrZero<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsNegativeOrZero(), () => throw new ArgumentIsNotNegativeOrZero(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNormal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNormal"/>
    public static void IfNotNormal<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNormal(), () => throw new ArgumentIsNotNormal(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNormal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNormal"/>
    public static void IfNotNormal<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsNormal(), () => throw new ArgumentIsNotNormal(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNormalized{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNormalized"/>
    public static void IfNotNormalized<T>(T i, string name)
        where T : IMinMaxValue<T>, INumber<T>
        => _If(i.IsNormalized(), () => throw new ArgumentIsNotNormalized(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsNormalized{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNormalized"/>
    public static void IfNotNormalized<T>(Kind kind, T[] i, string[] name)
        where T : IMinMaxValue<T>, INumber<T>
        => _If(i, kind, j => !j.IsNormalized(), () => throw new ArgumentIsNotNormalized(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNaN{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNumber"/>
    public static void IfNotNumber<T>(T i, string name)
        where T : INumber<T>
        => _If(i.IsNaN(), () => throw new ArgumentIsNotNumber(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="true"/>...</para><inheritdoc cref="XNumber.IsNaN{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotNumber"/>
    public static void IfNotNumber<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => j.IsNaN(), () => throw new ArgumentIsNotNumber(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsOdd{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotOdd"/>
    public static void IfNotOdd<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsOdd(), () => throw new ArgumentIsNotOdd(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsOdd{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotOdd"/>
    public static void IfNotOdd<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsOdd(), () => throw new ArgumentIsNotOdd(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.OfRange{T}(T, T, T)"/></summary>
    /// <exception cref="ArgumentIsNotOfRange"/>
    public static void IfNotOfRange<T>(T i, T minimum, T maximum, string name)
        where T : IMinMaxValue<T>, INumber<T>
        => _If(!i.OfRange(minimum, maximum), () => throw new ArgumentIsNotOfRange(name, i, minimum, maximum));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.OfRange{T}(T, T, T)"/></summary>
    /// <exception cref="ArgumentIsNotOfRange"/>
    public static void IfNotOfRange<T>(Kind kind, T[] i, T minimum, T maximum, string[] name)
        where T : IMinMaxValue<T>, INumber<T>
        => _If(i, kind, j => !j.OfRange(minimum, maximum), () => throw new ArgumentIsNotOfRange(name, i, minimum, maximum, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsPositive{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotPositive"/>
    public static void IfNotPositive<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsPositive(), () => throw new ArgumentIsNotPositive(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsPositive{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotPositive"/>
    public static void IfNotPositive<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsPositive(), () => throw new ArgumentIsNotPositive(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsPositiveInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotPositiveInfinity"/>
    public static void IfNotPositiveInfinity<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsPositiveInfinity(), () => throw new ArgumentIsNotPositiveInfinity(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsPositiveInfinity{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotPositiveInfinity"/>
    public static void IfNotPositiveInfinity<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsPositiveInfinity(), () => throw new ArgumentIsNotPositiveInfinity(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsPositiveOne{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotPositiveOne"/>
    public static void IfNotPositiveOne<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsPositiveOne(), () => throw new ArgumentIsNotPositiveOne(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsPositiveOne{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotPositiveOne"/>
    public static void IfNotPositiveOne<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsPositiveOne(), () => throw new ArgumentIsNotPositiveOne(name, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsPositiveOrZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotPositiveOrZero"/>
    public static void IfNotPositiveOrZero<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsPositiveOrZero(), () => throw new ArgumentIsNotPositiveOrZero(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsPositiveOrZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotPositiveOrZero"/>
    public static void IfNotPositiveOrZero<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsPositiveOrZero(), () => throw new ArgumentIsNotPositiveOrZero(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsReal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotReal"/>
    public static void IfNotReal<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsReal(), () => throw new ArgumentIsNotReal(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsReal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotReal"/>
    public static void IfNotReal<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsReal(), () => throw new ArgumentIsNotReal(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsSubnormal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotSubnormal"/>
    public static void IfNotSubnormal<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsSubnormal(), () => throw new ArgumentIsNotSubnormal(name, i));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsSubnormal{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotSubnormal"/>
    public static void IfNotSubnormal<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsSubnormal(), () => throw new ArgumentIsNotSubnormal(name, i, kind));

    /// <summary><para><see cref="Throw"/> if <see langword="false"/>...</para><inheritdoc cref="XNumber.IsZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotZero"/>
    public static void IfNotZero<T>(T i, string name)
        where T : INumber<T>
        => _If(!i.IsZero(), () => throw new ArgumentIsNotZero(name));

    /// <summary><para><see cref="Throw"/> if any, all, or none are <see langword="false"/>...</para><inheritdoc cref="XNumber.IsZero{T}(T)"/></summary>
    /// <exception cref="ArgumentIsNotZero"/>
    public static void IfNotZero<T>(Kind kind, T[] i, string[] name)
        where T : INumber<T>
        => _If(i, kind, j => !j.IsZero(), () => throw new ArgumentIsNotZero(name, kind));

    #endregion
}
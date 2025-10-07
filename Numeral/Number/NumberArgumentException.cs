using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Ion.Numeral;

public enum NumberArgumentType { Is, IsNot }

public abstract class NumberArgumentException(Kind? kind, in object name, in object value, string message, NumberArgumentType type)
    : ArgumentException($"{name}", GetMessage(kind, name, value, message, type switch { NumberArgumentType.Is => ConditionIf, _ => ConditionIfNot }))
{
    /// <see cref="Region.Field"/>
    #region

    private const string ArgumentFormat = "{0} {1}";

    private const string ConditionIf = "must not be";

    private const string ConditionIfNot = "must be";

    private const string NameDelimiter = ", ";

    private const string NameFormat = "'{0}'";

    private const string ValueDelimiter = ", ";

    private const string ValueFormat = "'{0}'";

    private const string ValueFormatAggregate = "(" + ValueFormat + ")";

    private const string ValueFormatMultiple = "({0})";

    ///

    private const string _All = "All";

    private const string _Any = "One or more";

    private const string _Are = "are";

    private const string _Argument = "Argument";

    private const string _Arguments = "arguments";

    private const string _ArgumentsOf = "of arguments";

    private const string _No = "No";

    private const string _None = "None";

    private const string _Space = " ";

    ///

    public const string _Canonical = "";

    public const string _Complex = "complex";

    public const string _Even = "even";

    public const string _Finite = "finite";

    public const string _Greater = "greater than (>)";

    public const string _GreaterOrEqual = "greater than or equal to (>=)";

    public const string _Imaginary = "imaginary";

    public const string _Infinity = "infinity (∞)";

    public const string _Integer = "integer";

    public const string _Less = "less than (<)";

    public const string _LessOrEqual = "less than or equal to (<=)";

    public const string _Negative = "negative (-)";

    public const string _NegativeInfinity = "negative infinity (-∞)";

    public const string _NegativeOne = "negative one (-1)";

    public const string _NegativeOrZero = "negative or zero (<= 0)";

    public const string _Normal = "normal";

    public const string _Normalized = "normalized (of range [0, 1])";

    public const string _Number = "number";

    public const string _Odd = "odd";

    public const string _OfRange = "of range [{0}, {1}]";

    public const string _Positive = "positive";

    public const string _PositiveInfinity = "positive infinity (+∞)";

    public const string _PositiveOne = "positive one (+1)";

    public const string _PositiveOrZero = "positive or zero (>= 0)";

    public const string _Real = "real";

    public const string _Subnormal = "subnormal";

    public const string _Zero = "zero (0)";

    #endregion

    /// <see cref="Region.Method"/>
    #region

    private static string GetName([NotNull] in Array i) => i.ToString(NameDelimiter, j => NameFormat.F(j));

    private static string GetMessage(Kind? kind, in object name, object value, string message, string messagePrefix)
    {
        var result = new StringBuilder();
        /// Assume (x) arguments
        if (kind is not null)
        {
            var prefix = kind switch { Kind.All => _All, Kind.Any => _Any, Kind.None => _None };

            /// (x) names where (x) >= 1
            if (name is not null)
            {
                /// (X) names where (X) > 1
                if (name is Array aNames && aNames.Length > 1)
                {
                    result.Append(prefix + _Space + _ArgumentsOf + _Space);

                    /// (x) names
                    /// (0) values

                    /// "All of arguments 'name', ... are zero."
                    /// "One or more of arguments 'name', ... are zero."
                    /// "None of arguments 'name', ... are zero."

                    if (value is null)
                        result.Append(GetName(aNames));

                    /// (x) names
                    /// (y) values

                    else if (value is Array aValues && aValues.Length > 1)
                    {
                        /// (x) names
                        /// (y) values where y >= x

                        /// "All of arguments 'name' ('value'), ... are zero."
                        /// "One or more of arguments 'name' ('value'), ... are zero."
                        /// "None of arguments 'name' ('value'), ... are zero."

                        /// Each argument (x) has value (y). Ignore leftover values!
                        if (aNames.Length <= aValues.Length)
                            result.Append(aNames.ToString(NameDelimiter, (i, j) => NameFormat.F(j) + _Space + ValueFormatAggregate.F(aValues.GetValue(i))));

                        /// (x) names
                        /// (y) values where y < x and y > 1

                        /// "All of arguments 'name', ... are zero."
                        /// "One or more of arguments 'name', ... are zero."
                        /// "None of arguments 'name', ... are zero."

                        /// Each argument (x) does not have value (y).
                        /// Assume developer mistake. Ignore all values!
                        else result.Append(GetName(aNames));
                    }

                    /// (x) names
                    /// (1) values

                    /// Assume (x) names = (1) value

                    /// "All of arguments 'name' ('value'), ... are zero."
                    /// "One or more of arguments 'name' ('value'), ... are zero."
                    /// "None of arguments 'name' ('value'), ... are zero."
                    else result.Append(aNames.ToString(NameDelimiter, i => NameFormat.F(i) + _Space + ValueFormatAggregate.F(value)));

                    result.Append(_Space + _Are + _Space);
                }
                /// (1) name
                else
                {
                    result.Append(_Argument + _Space + NameFormat.F(name) + _Space);

                    /// (1) name
                    /// (0) values

                    /// "Argument 'name' must [not] be zero."
                    if (value is null) { }

                    /// (1) name
                    /// (1) values

                    /// "Argument 'name' ('value') must [not] be zero."
                    else result.Append(ValueFormatAggregate.F(value) + _Space);
                    result.Append(messagePrefix + _Space);
                }
            }

            /// (0) names
            else
            {
                result.Append(kind switch { Kind.None => _No, _ => prefix });
                result.Append(_Space + _Arguments + _Space);

                /// (0) names
                /// (0) values

                /// "All arguments are zero."
                /// "One or more arguments are zero."
                /// "No arguments are zero."

                if (value is null) { }

                /// (0) names
                /// (x) values where (x) > 1

                /// "All arguments ('value', ...) are zero."
                /// "One or more arguments ('value', ...) are zero."
                /// "No arguments ('value', ...) are zero."
                else if (value is Array aValues && aValues.Length > 1)
                    result.Append(ValueFormatMultiple.F(aValues.ToString(ValueDelimiter, i => ValueFormat.F(i))));

                /// (0) names
                /// (1) value

                /// "All arguments ('value') are zero."
                /// "One or more arguments ('value') are zero."
                /// "No arguments ('value') are zero."
                else result.Append(ValueFormatAggregate.F(value));

                result.Append(_Are + _Space);
            }

            result.Append(message);
        }
        /// Assume (1) argument
        else
        {
            result.Append(_Argument + _Space);

            /// (0) names
            if (name is null)
            {
                /// (0) names
                /// (0) values

                /// "Argument must [not] be zero."
                if (value is null) { }

                /// (0) names
                /// (1) values

                /// "Argument ('value') must [not] be zero."
                else result.Append(ValueFormatAggregate.F(value));
            }
            /// (1) names
            else
            {
                /// (1) names
                /// (0) values

                /// "Argument 'name' must [not] be zero."
                if (value is null)
                    result.Append(NameFormat.F(name));

                /// (1) names
                /// (1) values

                /// "Argument 'name' ('value') must [not] be zero."
                else result.Append(ArgumentFormat.F(NameFormat.F(name), ValueFormatAggregate.F(value)));
            }

            result.Append(_Space + messagePrefix + _Space + message);
        }
        return result.ToString();
    }

    #endregion
}

public abstract class NumberArgumentIsNot(Kind? kind, object name, object value, string message)
    : NumberArgumentException(kind, name, value, message, NumberArgumentType.Is);

/// <see cref="NumberArgumentIsNot"/>
#region

public class ArgumentIsCanonical(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Canonical);

public class ArgumentIsComplex(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Complex);

public class ArgumentIsEven(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Even);

public class ArgumentIsFinite(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Finite);

public class ArgumentIsGreater(object name, object value, object b, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Greater.F(b));

public class ArgumentIsGreaterOrEqual(object name, object value, object b, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _GreaterOrEqual.F(b));

public class ArgumentIsImaginary(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Imaginary);

public class ArgumentIsInfinity(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Infinity);

public class ArgumentIsInteger(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Integer);

public class ArgumentIsLess(object name, object value, object b, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Less.F(b));

public class ArgumentIsLessOrEqual(object name, object value, object b, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _LessOrEqual.F(b));

public class ArgumentIsNegative(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Negative);

public class ArgumentIsNegativeInfinity(object name, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, null, _NegativeInfinity);

public class ArgumentIsNegativeOne(object name, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, null, _NegativeOne);

public class ArgumentIsNegativeOrZero(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _NegativeOrZero);

public class ArgumentIsNormal(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Normal);

public class ArgumentIsNormalized(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Normalized);

public class ArgumentIsNumber(object name, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, null, _Number);

public class ArgumentIsOdd(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Odd);

public class ArgumentIsOfRange(object name, object value, object minimum, object maximum, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _OfRange.F(minimum, maximum));

public class ArgumentIsPositive(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Positive);

public class ArgumentIsPositiveInfinity(object name, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, null, _PositiveInfinity);

public class ArgumentIsPositiveOne(object name, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, null, _PositiveOne);

public class ArgumentIsPositiveOrZero(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _PositiveOrZero);

public class ArgumentIsReal(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Real);

public class ArgumentIsSubnormal(object name, object value, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, value, _Subnormal);

public class ArgumentIsZero(object name, Kind? kind = null)
    : NumberArgumentIsNot(kind, name, null, _Zero);

#endregion

public abstract class NumberArgumentIs(Kind? kind, object name, object value, string message)
    : NumberArgumentException(kind, name, value, message, NumberArgumentType.IsNot);

/// <see cref="NumberArgumentIs"/>
#region

public class ArgumentIsNotCanonical(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Canonical);

public class ArgumentIsNotComplex(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Complex);

public class ArgumentIsNotEven(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Even);

public class ArgumentIsNotFinite(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Finite);

public class ArgumentIsNotGreater(object name, object value, object b, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Greater);

public class ArgumentIsNotGreaterOrEqual(object name, object value, object b, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _GreaterOrEqual);

public class ArgumentIsNotImaginary(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Imaginary);

public class ArgumentIsNotInfinity(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Infinity);

public class ArgumentIsNotInteger(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Integer);

public class ArgumentIsNotLess(object name, object value, object b, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Less);

public class ArgumentIsNotLessOrEqual(object name, object value, object b, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _LessOrEqual);

public class ArgumentIsNotNegative(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Negative);

public class ArgumentIsNotNegativeInfinity(object name, Kind? kind = null)
    : NumberArgumentIs(kind, name, null, _NegativeInfinity);

public class ArgumentIsNotNegativeOne(object name, Kind? kind = null)
    : NumberArgumentIs(kind, name, null, _NegativeOne);

public class ArgumentIsNotNegativeOrZero(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _NegativeOrZero);

public class ArgumentIsNotNormal(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Normal);

public class ArgumentIsNotNormalized(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Normalized);

public class ArgumentIsNotNumber(object name, Kind? kind = null)
    : NumberArgumentIs(kind, name, null, _Number);

public class ArgumentIsNotOdd(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Odd);

public class ArgumentIsNotOfRange(object name, object value, object minimum, object maximum, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _OfRange.F(minimum, maximum));

public class ArgumentIsNotPositive(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Positive);

public class ArgumentIsNotPositiveInfinity(object name, Kind? kind = null)
    : NumberArgumentIs(kind, name, null, _PositiveInfinity);

public class ArgumentIsNotPositiveOne(object name, Kind? kind = null)
    : NumberArgumentIs(kind, name, null, _PositiveOne);

public class ArgumentIsNotPositiveOrZero(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _PositiveOrZero);

public class ArgumentIsNotReal(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Real);

public class ArgumentIsNotSubnormal(object name, object value, Kind? kind = null)
    : NumberArgumentIs(kind, name, value, _Subnormal);

public class ArgumentIsNotZero(object name, Kind? kind = null)
    : NumberArgumentIs(kind, name, null, _Zero);

#endregion
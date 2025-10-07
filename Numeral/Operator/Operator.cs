using System;

namespace Ion.Numeral;

/// <summary>
/// An operator used to perform <see cref="Math"/> between two numbers.
/// </summary>
public enum Operator
{
    /// <summary>(+) Addition.</summary>
    Add,
    /// <summary>(/) Division.</summary>
    Divide,
    /// <summary>(%) Modulus.</summary>
    Modulo,
    /// <summary>(*) Multiplication.</summary>
    Multiply,
    /// <summary>(-) Subtraction.</summary>
    Subtract
}
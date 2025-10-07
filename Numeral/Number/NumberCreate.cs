using System.Numerics;

namespace Ion.Numeral;

/// <summary>
/// How to get instance of <see cref="INumber{TSelf}"/> from another type.
/// </summary>
/// <remarks>
/// <see cref="Default"/> = <see cref="Saturated"/>
/// <para><b>Output <see cref="INumber{TSelf}"/> must define conversion from input <see cref="INumber{TSelf}"/>.</b></para>
/// <list type="bullet">
/// <item><see cref="INumberBase{TSelf}.CreateChecked{TOther}(TOther)"/></item>
/// <item><see cref="INumberBase{TSelf}.CreateSaturating{TOther}(TOther)"/></item>
/// <item><see cref="INumberBase{TSelf}.CreateTruncating{TOther}(TOther)"/></item>
/// </list>
/// </remarks>
public enum NumberCreate
{
    /// <summary>
    /// See <see cref="Saturated"/>.
    /// </summary>
    Default,
    /// <inheritdoc cref="INumberBase{TSelf}.CreateChecked{TOther}(TOther)"/>
    Checked,
    /// <inheritdoc cref="INumberBase{TSelf}.CreateSaturating{TOther}(TOther)"/>
    Saturated,
    /// <inheritdoc cref="INumberBase{TSelf}.CreateTruncating{TOther}(TOther)"/>
    Truncated
}
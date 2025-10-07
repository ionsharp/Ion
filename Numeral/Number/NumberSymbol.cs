namespace Ion.Numeral;

///✔ ✖

/// <summary>Mathematical concepts and the relevant symbols.</summary>
/// <remarks>https://www.rapidtables.com/math/symbols/_Math_Symbols.html</remarks>
public static class NumberSymbol
{
    public enum Algebra
    {
        /// ⌈⌉ 
        Ceiling,
        /// ÷
        Divide,
        /// /
        DivideSlash,
        /// ∆  
        Delta,
        /// ⌊⌋ 
        Floor,
        /// ¼  
        Fraction1By4 = 0188,
        /// ½ 
        Fraction1By2 = 0189,
        /// ¾  
        Fraction3By4 = 0190,
        /// φ  
        GoldenRatio,
        /// ∞  
        Infinity,
        /// ×
        Multiply,
        /// ±
        PlusMinus,
        /// ∝ 
        ProportionalTo,
        /// ∑  
        Sigma,
        /// √
        SquareRoot,
    }

    public enum Currency
    {
        //¢ 	
        Cent = 0162,
        //€ 	
        Euro = 0128,
        //ƒ   
        Florin = 0131,
        //¤ 	
        Generic = 0164,
        //£ 	
        Pound = 0163,
        //¥ 	
        Yen = 0165,
    }

    public enum Equality
    {
        /// =
        Equal,
        /// ≠
        EqualNot,
        /// ≡  
        EquivalentTo,
        /// ~
        EqualSimilar,
        /// ≈
        EqualApproximate,
        /// >
        GreaterThan1,
        /// › 	
        GreaterThan2 = 0155,
        /// » 	
        GreaterThan3 = 0187,
        /// <
        LessThan1,
        /// ‹ 	
        LessThan2 = 0139,
        /// «	
        LessThan3 = 0171,
        /// ≥
        GreaterThanOrEqualTo,
        /// ≤
        LessThanOrEqualTo,
    }

    public enum Geometry
    {
        /// ∠
        Angle,
        /// ∟
        AngleRight,
        /// °
        Degree,
        /// grad
        Gradian,
        /// ∥
        Parallel,
        /// ⊥
        Perpendicular,
        /// π
        Pi,
        /// ∏
        PiCapital,
        /// rad
        Radian,
    }

    public enum Logic
    {
        /// ⋅
        And,
        /// ∀
        ForAll,
        /// ⇔
        Equals1,
        /// ↔
        Equals2,
        /// ⇒
        Implies1,
        /// ⇐
        Implies2,
        /// ⇑
        Implies3,
        /// ⇓
        Implies4,
        /// ∃
        ThereExists,
        /// ∄
        ThereDoesNotExist,
        /// ∴
        Therefore,
        /// ∵
        BecauseSince,
    }

    public enum Multiplication
    {
        /// ⋅
        Dot1,
        /// .
        Dot2,
        /// • 	
        Dot3 = 0149,
    }

    public enum SetTheory
    {
        /// <summary>
        /// <b>A × B</b>
        /// <para>The set of all ordered pairs(a, b) where a ∈ A and b ∈ B.</para>
        /// </summary>
        CartesianProduct,
        /// <summary>
        /// <b>A ∘ B</b>
        /// <para>The set of all elements in both A and B.</para>
        /// </summary>
        /// <remarks>String theory/algebra.</remarks>
        Concatenation,
        /// ∈
        ElementOf,
        /// ∉
        ElementOfNot,
        /// Ø
        Empty,
        /// <summary>
        /// <b>A ⋂ B</b>
        /// <para>The set of all elements that are common to both A and B.</para>
        /// </summary>
        Intersect,
        /// ⊆
        Subset,
        /// ⊄
        SubsetNot,
        /// ⊂
        SubsetStrict,
        /// ⊇
        Superset,
        /// ⊅
        SupersetNot,
        /// ⊃
        SupersetStrict,
        /// <summary>
        /// <b>A ⋃ B</b>
        /// <para>The set of all elements in either A or B.</para>
        /// </summary>
        Union,
    }

    public enum Subtraction
    {
        /// – 	
        Endash = 0150,
        /// — 	
        Emdash = 0151,
    }
}
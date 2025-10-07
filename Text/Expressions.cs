namespace Ion.Text;

public static class Expressions
{
    public const string DecimalNumber
        = @"^-?[0-9]*\.?[0-9]+$";

    public const string Guid
        = "^([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$";

    public const string Letter
        = "^[a-zA-Z]*$";

    public const string LetterOrNumber
        = "^[a-zA-Z0-9]*$";

    public const string Number
        = "^[0-9]*$";
}
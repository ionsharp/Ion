namespace Ion.Text;

public class Characters
{
    public const string All = Numbers + Lower + Special + Upper;

    public const string LettersAndNumbers = Numbers + Lower + Upper;

    public const string Numbers
        = "0123456789";

    public const string Lower
        = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>Characters that might be confused with one another are removed (64 characters).</summary>
    public const string Password = "!#%+23456789:=?@ABCDEFGHJKLMNPRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

    public const string Special
        = "!@#$%^&*()-=_+[]{};':\",./<>?`~\\|";

    public const string Upper
        = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
}
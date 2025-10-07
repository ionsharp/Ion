namespace Ion;

public interface IAttribute;

public interface IAttributeWithMessage : IAttribute
{
    string Message { get; }
}
namespace Ion.Reflect;

public interface ICreateFrom<T>
{
    object Create(T value);
}
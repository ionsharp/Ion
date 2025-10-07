namespace Ion.Core;

///<remarks>Implements <see cref="IViewModel"/>.</remarks>
public record class ViewModel() : Model(), IViewModel
{
    public virtual void Subscribe() { }

    public virtual void Unsubscribe() { }
}
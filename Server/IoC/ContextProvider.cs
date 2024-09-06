namespace Server.IoC;

public class ContextProvider<T> where T : new()
{
    private static readonly AsyncLocal<T> _asyncLocal = new ();

    public T GetContext()
    {
        if (_asyncLocal.Value is null)
        {
            _asyncLocal.Value = new T();
        }

        return _asyncLocal.Value;
    }
}

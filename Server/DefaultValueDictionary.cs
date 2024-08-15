namespace Server;

public class DefaultValueDictionary<TKey, TValue>(TValue defaultValue) where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _dict = new ();

    public TValue this[TKey key]
    {
        get => _dict.GetValueOrDefault(key, defaultValue);
        set => _dict[key] = value;
    }
}

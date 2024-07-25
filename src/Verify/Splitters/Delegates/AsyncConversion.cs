namespace VerifyTests;

public delegate Task<ConversionResult> AsyncConversion<in T>(T target, ConversionContext context);

public delegate Task<ConversionResult> AsyncConversion(object target, ConversionContext context);

public class ConversionContext(IReadOnlyDictionary<string, object> context, Func<object, StringBuilder> serialize) :
    IReadOnlyDictionary<string, object>
{
    public Func<object, StringBuilder> Serialize { get; } = serialize;

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
        context.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable) context).GetEnumerator();

    public int Count => context.Count;

    public bool ContainsKey(string key) =>
        context.ContainsKey(key);

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public bool TryGetValue(string key, [NotNullWhen(true)] out object? value) =>
#pragma warning restore CS8767
        context.TryGetValue(key, out value);

    public object this[string key] => context[key];

    public IEnumerable<string> Keys => context.Keys;

    public IEnumerable<object> Values => context.Values;
}
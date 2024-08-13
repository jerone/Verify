namespace VerifyTests;

public static partial class Recording
{
    public static IReadOnlyDictionary<string, IReadOnlyList<object>> ToDictionary(this IEnumerable<ToAppend> values)
    {
        var dictionary = new Dictionary<string, IReadOnlyList<object>>(StringComparer.OrdinalIgnoreCase);

        foreach (var value in values)
        {
            List<object> objects;
            if (dictionary.TryGetValue(value.Name, out var item))
            {
                objects = (List<object>) item;
            }
            else
            {
                dictionary[value.Name] = objects = [];
            }

            objects.Add(value.Data);
        }

        return dictionary;
    }
}
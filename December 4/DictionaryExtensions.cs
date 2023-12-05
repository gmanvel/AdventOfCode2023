namespace AdventOfCode2023.December_4;

internal static class DictionaryExtensions
{
    public static int AddOrIncrement(this Dictionary<int, int> dictionary, int key, int value = 1)
    {
        var hasBeenAdded = dictionary.TryAdd(key, value);
        if (hasBeenAdded is false)
        {
            var currentValue = dictionary[key];
            currentValue += value;
            dictionary[key] = currentValue;

            return currentValue;
        }

        return value;
    }

    public static int GetValueOrSelf(this Dictionary<int, int> dictionary, int key) =>
        dictionary.TryGetValue(key, out var value)
        ? value
        : key;
}
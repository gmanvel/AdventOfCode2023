using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public sealed class December1 : IPuzzle
{
    private static FrozenDictionary<string, string> s_lookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["one"]   = "1",
        ["two"]   = "2",
        ["three"] = "3",
        ["four"]  = "4",
        ["five"]  = "5",
        ["six"]   = "6",
        ["seven"] = "7",
        ["eight"] = "8",
        ["nine"]  = "9",

    }.ToFrozenDictionary();

    private static Regex s_numberWordsRegex = new ("(?=(1|one|2|two|3|three|4|four|5|five|6|six|7|seven|8|eight|9|nine))", RegexOptions.Compiled);

    public void Solve(object input)
    {
        if (input is not IEnumerable<string> lines)
            throw new ArgumentException(nameof(lines));

        Console.WriteLine(Calculate_2(lines));
    }

    private static int Convert(string line)
    {
        var matches = s_numberWordsRegex.Matches(line).ToList();

        if (matches.Count == 0)
            return 0;

        var firstOccurrence = matches.First().Groups.Values.First(v => !string.IsNullOrEmpty(v.Value));

        var firstDigit = firstOccurrence.Value;
        if (s_lookup.TryGetValue(firstOccurrence.Value, out var firstReplacement))
            firstDigit = firstReplacement;

        var lastOccurrence = matches.Last().Groups.Values.First(v => !string.IsNullOrEmpty(v.Value));

        var lastDigit = lastOccurrence.Value;
        if (s_lookup.TryGetValue(lastOccurrence.Value, out var lastReplacement))
            lastDigit = lastReplacement;
        
        return int.Parse($"{firstDigit}{lastDigit}");
    }

    private static int Calculate_2(IEnumerable<string> lines) =>
        lines
            .AsParallel()
            .Select(Convert)
            .Sum();

    private static int Calculate_1(IEnumerable<string> lines) =>
        lines
            .AsParallel()
            .Select(line =>
            {
                var firstNumberPosition = line.AsSpan().IndexOfAnyInRange('0', '9');
                var lastNumberPosition = line.AsSpan().LastIndexOfAnyInRange('0', '9');

                var number = $"{line[firstNumberPosition]}{line[lastNumberPosition]}";

                return int.Parse(number);
            })
            .Sum();
}
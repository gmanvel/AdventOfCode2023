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

    private static Regex s_numberWordsRegex = new ("(one|two|three|four|five|six|seven|eight|nine)", RegexOptions.Compiled);

    public void Solve(object input)
    {
        if (input is not IEnumerable<string> lines)
            throw new ArgumentException(nameof(lines));

        Console.WriteLine(GetAnswer(lines));
    }

    private static string Sanitize(string line)
    {
        foreach (Match match in s_numberWordsRegex.Matches(line))
        {
            if (!s_lookup.TryGetValue(match.Value, out var replacement))
                continue;

            line = line.Replace(match.Value, replacement);
        }

        return line;
    }

    private static int GetAnswer(IEnumerable<string> lines) =>
        lines
            .AsParallel()
            .Select(Sanitize)
            .Select(line =>
            {
                var firstNumberPosition = line.AsSpan().IndexOfAnyInRange('0', '9');
                var lastNumberPosition = line.AsSpan().LastIndexOfAnyInRange('0', '9');

                var number = $"{line[firstNumberPosition]}{line[lastNumberPosition]}";

                return int.Parse(number);
            })
            .Sum();
}
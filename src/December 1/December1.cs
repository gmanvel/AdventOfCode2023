using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public sealed class December1 : IPuzzle
{
    private static readonly FrozenDictionary<string, string> s_lookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
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

    private static readonly Regex s_wordsFromBeginning = new("(?=(one|two|three|four|five|six|seven|eight|nine))", RegexOptions.Compiled);
    private static readonly Regex s_wordsFromEnd = new("(?=(one|two|three|four|five|six|seven|eight|nine))", RegexOptions.Compiled|RegexOptions.RightToLeft);

    public void Solve(object input)
    {
        if (input is not IEnumerable<string> lines)
            throw new ArgumentException(nameof(lines));

        Console.WriteLine(Calculate_2(lines));
    }

    public static int Calculate_1(IEnumerable<string> lines) =>
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
    
    private static int Convert(string line)
    {
        // Take match index for word & number checking from the left -> right (from the beginning)
        // Find minimum match index and use that as first digit
        ReadOnlySpan<char> GetFirstDigit()
        {
            var matchFromBeginning = s_wordsFromBeginning.Match(line);

            var firstNumberPosition = line.AsSpan().IndexOfAnyInRange('0', '9');

            var matchFromBeginningIndex = matchFromBeginning?.Success ?? false ? matchFromBeginning.Index : int.MaxValue;
            var minIndex = Math.Min(matchFromBeginningIndex, firstNumberPosition);

            return char.IsDigit(line[minIndex]) ? line.AsSpan().Slice(minIndex, 1) : s_lookup[matchFromBeginning!.Groups[1].Value].AsSpan();
        }

        // Take match index for word & number checking from the right -> left (from the end)
        // Find maximum match index and use that as last digit
        ReadOnlySpan<char> GetLastDigit()
        {
            var matchFromEnd = s_wordsFromEnd.Match(line);

            var lastNumberPosition = line.AsSpan().LastIndexOfAnyInRange('0', '9');

            var matchFromEndIndex = matchFromEnd?.Success ?? false ? matchFromEnd.Index : int.MinValue;
            var maxIndex = Math.Max(matchFromEndIndex, lastNumberPosition);

            return char.IsDigit(line[maxIndex]) ? line.AsSpan().Slice(maxIndex, 1) : s_lookup[matchFromEnd!.Groups[1].Value].AsSpan();
        }

        return int.Parse($"{GetFirstDigit()}{GetLastDigit()}");
    }

    public static int Calculate_2(IEnumerable<string> lines) =>
        lines
            .AsParallel()
            .Select(Convert)
            .Sum();

    public static int Task1(IEnumerable<string> input)
    {
        var result = 0;
        foreach (var line in input)
        {
            var firstNum = line.First(x => int.TryParse(x.ToString(), out _));
            var lastNum = line.Last(x => int.TryParse(x.ToString(), out _));
            var sumChar = $"{firstNum}{lastNum}";
            var sum = int.Parse(sumChar);
            result += sum;
        }
        return result;
    }

    public static int Task2(IEnumerable<string> input) =>
        input
            .AsParallel()
            .Select(line =>
            {
                var searchResult = new Dictionary<int, int>();

                foreach (var k in s_numbers)
                {
                    var index = 0;
                    while ((index = line.IndexOf(k.Key, index, StringComparison.Ordinal)) != -1)
                    {
                        searchResult.Add(index++, k.Value);
                    }
                }

                var min = searchResult.Keys.Min();
                var max = searchResult.Keys.Max();
                var minValue = searchResult[min];
                var maxValue = searchResult[max];
                var resultingNumber = int.Parse($"{minValue}{maxValue}");
                return resultingNumber;
            })
            .Sum();

    private static FrozenDictionary<string, int> s_numbers = new Dictionary<string, int>()
    {
        { "one", 1 },
        { "1", 1 },
        { "two", 2 },
        { "2", 2 },
        { "three", 3 },
        { "3", 3 },
        { "four", 4 },
        { "4", 4 },
        { "five", 5 },
        { "5", 5 },
        { "six", 6 },
        { "6", 6 },
        { "seven", 7 },
        { "7", 7 },
        { "eight", 8 },
        { "8", 8 },
        { "nine", 9 },
        { "9", 9 }
    }.ToFrozenDictionary();

}
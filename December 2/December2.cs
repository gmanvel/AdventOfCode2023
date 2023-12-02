using System.Buffers;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.December_2;

public class December2 : IPuzzle
{
    private static readonly FrozenDictionary<string, int> s_maxAllowedAmount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        ["red"]   = 12,
        ["green"] = 13,
        ["blue"]  = 14
    }.ToFrozenDictionary();

    private static readonly Regex s_colorValueRegex = new(@"(\d+)\s+(red|blue|green)", RegexOptions.Compiled);
        
    public void Solve(object input)
    {
        if (input is not IEnumerable<string> values)
            throw new ArgumentException(nameof(input));
        
        Console.WriteLine(GetGameIdSum(values));
    }

    private static int GetGameIdSum(IEnumerable<string> values) =>
        values
            .AsParallel()
            .Select(GetGameIdOrZero)
            .Sum();
    
    private static int GetGameIdOrZero(string line)
    {
        var indexOfColon = line.IndexOf(':');

        var gameId = int.Parse(line.AsSpan().Slice(5, indexOfColon - 5));
        
        var data = line.AsSpan().Slice(indexOfColon);
        var semicolonCount = data.Count(';');

        var ranges = ArrayPool<Range>.Shared.Rent(semicolonCount + 1); 
    
        data.Split(ranges.AsSpan(), ';', StringSplitOptions.RemoveEmptyEntries);

        for (int index = 0; index <= semicolonCount; index++)
        {
            var range = ranges[index];

            var chunk = data[range];
        
            foreach (Match match in s_colorValueRegex.Matches(chunk.ToString()))
            {
                var number = int.Parse(match.Groups[1].ValueSpan);
                var color = match.Groups[2].Value;
                
                if (s_maxAllowedAmount.TryGetValue(color, out var maxAmount) &&
                    number > maxAmount)
                {
                    Console.WriteLine($"Game #{gameId}: {number} {color} > {maxAmount} max allowed.");
                    ArrayPool<Range>.Shared.Return(ranges);
                    return 0;
                }
            }
        }

        ArrayPool<Range>.Shared.Return(ranges);
        return gameId;
    }
}
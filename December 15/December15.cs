using System.Buffers;
using System.Collections.Frozen;
using System.Diagnostics;

namespace AdventOfCode2023.December_15;

public class December15 : IPuzzle
{
    private static readonly SearchValues<char> s_operations = SearchValues.Create("=-");

    public void SolvePart1(object input)
    {
        if (input is not string text)
            throw new ArgumentException(nameof(input));

        //  input contains any non-ASCII or control characters?
        Debug.Assert(text.AsSpan().IndexOfAnyExceptInRange((char)0x20, (char)0x7e) == -1);

        Console.WriteLine(
            text
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(step => step.Aggregate(0, (acc, c) => CalculateHash(c, acc)))
                .Sum());
    }

    public void SolvePart2(object input)
    {
        if (input is not string text)
            throw new ArgumentException(nameof(input));

        FrozenDictionary<int, Box> boxes =
            Enumerable
                .Range(0, 256)
                .ToFrozenDictionary(n => n, n => new Box(n));

        foreach (var step in text.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            var indexOfAny = step.AsSpan().IndexOfAny(s_operations);

            var label = step.Substring(0, indexOfAny);

            var hash = label.Aggregate(0, (acc, c) => CalculateHash(c, acc));

            var operation = step[indexOfAny];

            var box = boxes[hash];

            if (operation == '-')
            {
                box.RemoveLens(label);
            }
            else
            {
                box.AddLens(label, int.Parse(step.AsSpan(indexOfAny + 1)));
            }
        }

        Console.WriteLine(
            boxes.Values
                .Select(b => b.GetLensFocusPowerSum())
                .Sum());
    }

    private static int CalculateHash(char c, int currentValue)
    {
        var ascii = (int)c;

        currentValue += ascii;

        currentValue *= 17;

        currentValue = Math.DivRem(currentValue, 256).Remainder;

        return currentValue;
    }
}
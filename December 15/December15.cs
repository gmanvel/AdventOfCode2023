using System.Diagnostics;

namespace AdventOfCode2023.December_15;

public class December15 : IPuzzle
{
    public void SolvePart1(object input)
    {
        if (input is not string text)
            throw new ArgumentException(nameof(input));

        //  input contains any non-ASCII or control characters?
        Debug.Assert(text.AsSpan().IndexOfAnyExceptInRange((char)0x20, (char)0x7e) == -1);

        Console.WriteLine(
            text
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(input => input.Aggregate(0, (acc, c) => CalculateHash(c, acc)))
                .Sum());
    }

    public void SolvePart2(object input)
    {
        throw new NotImplementedException();
    }

    static int CalculateHash(char c, int currentValue)
    {
        var ascii = (int)c;

        currentValue += ascii;

        currentValue *= 17;

        currentValue = Math.DivRem(currentValue, 256).Remainder;

        return currentValue;
    }
}
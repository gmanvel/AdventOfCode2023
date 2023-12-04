namespace AdventOfCode2023.December_4;

public class December4 : IPuzzle
{
    public void SolvePart1(object input)
    {
        if (input is not IEnumerable<string> lines)
            throw new ArgumentException(nameof(lines));

        Console.WriteLine(
            lines
            .AsParallel()
            .Select(line =>
            {
                var indexOfColon = line.IndexOf(':');
                var data = line.AsSpan().Slice(indexOfColon + 1);

                var indexOfSeparator = data.IndexOf('|');

                var winningNumbers =
                    data.Slice(0, indexOfSeparator)
                        .ToString()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => int.Parse(s.Trim()))
                        .ToHashSet();

                var myNumbers =
                    data.Slice(indexOfSeparator + 1)
                        .ToString()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => int.Parse(s.Trim()))
                        .ToHashSet();

                var myWinningNumbers =
                    myNumbers
                        .Intersect(winningNumbers)
                        .Count();

                return myWinningNumbers == 0
                    ? 0
                    // 1,2,4,8,16,32...
                    // a(n) = a(1) x r^(n-1), where a(1) = 1, r = 2
                    : 1 * Math.Pow(2, myWinningNumbers - 1); 
            })
            .Sum()
            );
    }

    public void SolvePart2(object input)
    {
        if (input is not IEnumerable<string> lines)
            throw new ArgumentException(nameof(lines));

        var dictionary = new Dictionary<int, int>();

        foreach (var (line, index) in lines.Select((value, index) => (value, index)))
        {
            var cardNumber = index + 1;

            var indexOfColon = line.IndexOf(':');
            var data = line.AsSpan().Slice(indexOfColon + 1);

            var indexOfSeparator = data.IndexOf('|');

            var winningNumbers =
                data.Slice(0, indexOfSeparator)
                    .ToString()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(num => int.Parse(num.Trim()))
                    .ToHashSet();

            var myNumbers =
                data.Slice(indexOfSeparator + 1)
                    .ToString()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(num => int.Parse(num.Trim()))
                    .ToHashSet();

            var myWinningNumbers =
                myNumbers
                    .Intersect(winningNumbers)
                    .Count();

            var copies = dictionary.AddOrIncrement(cardNumber);

            if (myWinningNumbers == 0)
                continue;

            foreach (var nextCardNumber in Enumerable.Range(cardNumber + 1, myWinningNumbers))
            {
                dictionary.AddOrIncrement(nextCardNumber, copies);
            }
        }

        Console.WriteLine(dictionary.Values.Sum());
    }
}
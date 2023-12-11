namespace AdventOfCode2023.December_11;

public class December11 : IPuzzle
{
    public void SolvePart1(object input)
    {
        if (input is not string[] universe)
        {
            throw new ArgumentException(nameof(input));
        }

        var galaxyPositions =
            universe
                .SelectMany((line, index) =>
                {
                    var galaxies = new List<(int Row, int Column)>();

                    for (var i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '#')
                        {
                            galaxies.Add((index, i));
                        }
                    }

                    return galaxies;
                })
                .OrderBy(tuple => tuple.Row)
                .ToList();

        var columnsWithNoGalaxy =
            Enumerable.Range(0, universe[0].Length)
                .Except(galaxyPositions.Select(pos => pos.Column).Distinct())
                .ToList();

        var rowsWithNoGalaxy =
            Enumerable.Range(0, universe.Length)
                .Except(galaxyPositions.Select(pos => pos.Row).Distinct())
                .ToList();

        var pairs = GeneratePairs(galaxyPositions);

        Console.WriteLine(
            pairs
                .Select(pair =>
                {
                    var galaxy1 = pair.Item1;
                    var galaxy2 = pair.Item2;

                    var row1 = galaxy1.Item1;
                    var row2 = galaxy2.Item1;

                    var doubleRowSteps = rowsWithNoGalaxy.Count(row => Math.Min(row1, row2) < row && row < Math.Max(row1, row2));

                    long rowSteps = Math.Abs(row2 - row1) - doubleRowSteps + doubleRowSteps * 2;

                    var column1 = galaxy1.Item2;
                    var column2 = galaxy2.Item2;

                    var doubleColumnSteps = columnsWithNoGalaxy.Count(column => Math.Min(column1, column2) < column && column < Math.Max(column1, column2));

                    long columnSteps = Math.Abs(column2 - column1) - doubleColumnSteps + doubleColumnSteps * 2;

                    return rowSteps + columnSteps;
                })
                .Sum());
    }

    public void SolvePart2(object input)
    {
        if (input is not string[] universe)
        {
            throw new ArgumentException(nameof(input));
        }

        var galaxyPositions =
            universe
                .SelectMany((line, index) =>
                {
                    var galaxies = new List<(int Row, int Column)>();

                    for (var i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '#')
                        {
                            galaxies.Add((index, i));
                        }
                    }

                    return galaxies;
                })
                .OrderBy(tuple => tuple.Row)
                .ToList();

        var columnsWithNoGalaxy =
            Enumerable.Range(0, universe[0].Length)
                .Except(galaxyPositions.Select(pos => pos.Column).Distinct())
                .ToList();

        var rowsWithNoGalaxy =
            Enumerable.Range(0, universe.Length)
                .Except(galaxyPositions.Select(pos => pos.Row).Distinct())
                .ToList();

        var pairs = GeneratePairs(galaxyPositions);

        Console.WriteLine(
            pairs
                .Select(pair =>
                {
                    var galaxy1 = pair.Item1;
                    var galaxy2 = pair.Item2;

                    var row1 = galaxy1.Item1;
                    var row2 = galaxy2.Item1;

                    var doubleRowSteps = rowsWithNoGalaxy.Count(row => Math.Min(row1, row2) < row && row < Math.Max(row1, row2));

                    long rowSteps = Math.Abs(row2 - row1) - doubleRowSteps + doubleRowSteps * 1_000_000;

                    var column1 = galaxy1.Item2;
                    var column2 = galaxy2.Item2;

                    var doubleColumnSteps = columnsWithNoGalaxy.Count(column => Math.Min(column1, column2) < column && column < Math.Max(column1, column2));

                    long columnSteps = Math.Abs(column2 - column1) - doubleColumnSteps + doubleColumnSteps * 1_000_000;

                    return rowSteps + columnSteps;
                })
                .Sum());
    }

    static List<Tuple<(int, int), (int, int)>> GeneratePairs(List<(int, int)> numbers)
    {
        List<Tuple<(int, int), (int, int)>> pairs = new();

        for (int i = 0; i < numbers.Count; i++)
        {
            for (int j = i + 1; j < numbers.Count; j++)
            {
                pairs.Add(new Tuple<(int, int), (int, int)>(numbers[i], numbers[j]));
            }
        }

        return pairs;
    }
}
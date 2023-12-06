namespace AdventOfCode2023.December_6;

public class December6 : IPuzzle
{
    public void SolvePart1(object input)
    {
        if (input is not List<(long Time, long Distance)> pairs)
            throw new ArgumentException(nameof(input));

        Console.WriteLine(
            pairs
                .Select(timeDistancePairs =>
                {
                    long lastSeenDistance = 0;
                    HashSet<long> matches = new();
                    foreach (var number in Enumerable.Range(1, (int)timeDistancePairs.Time - 1))
                    {
                        var distance = (timeDistancePairs.Time - number) * number;

                        if (distance == lastSeenDistance)
                            return matches.Count * 2;

                        if (distance > timeDistancePairs.Distance)
                        {
                            lastSeenDistance = distance;
                            matches.Add(number);
                        }
                    }

                    return matches.Count;
                })
                .Aggregate((total, next) => total * next));
    }

    public void SolvePart2(object input)
    {
        if (input is not List<(long Time, long Distance)> pairs)
            throw new ArgumentException(nameof(input));

        Console.WriteLine(
            pairs
                .Select(timeDistancePairs =>
                {
                    long lastSeenDistance = 0;
                    HashSet<long> matches = new();
                    foreach (var number in Enumerable.Range(1, (int)timeDistancePairs.Time - 1))
                    {
                        var distance = (timeDistancePairs.Time - number) * number;

                        if (distance == lastSeenDistance)
                            return matches.Count * 2;

                        if (distance > timeDistancePairs.Distance)
                        {
                            lastSeenDistance = distance;
                            matches.Add(number);
                        }
                    }

                    return matches.Count;
                })
                .Sum());
    }
}
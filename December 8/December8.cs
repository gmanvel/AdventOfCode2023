using System.Collections.Frozen;

namespace AdventOfCode2023.December_8;

public class December8 : IPuzzle
{
    public void SolvePart1(object input)
    {
        if (input is not string[] inputData)
            throw new ArgumentException(nameof(input));

        var instructions = inputData[0];

        var map =
            inputData
                .Skip(2)
                .Select(line =>
                {
                    var key = line.AsSpan(0, 3).ToString();

                    var values = (line.AsSpan(7, 3).ToString(), line.AsSpan(12, 3).ToString());

                    return new KeyValuePair<string, (string, string)>(key, values);
                })
                .ToFrozenDictionary();

        long moves = 0;
        var position = map["AAA"];
        while (true)
        {
            for (int index = 0; index < instructions.Length; index++)
            {
                moves++;

                var nextDestination = GetDestination(instructions[index], position);

                if (nextDestination == "ZZZ")
                {
                    Console.WriteLine(moves);
                    return;
                }

                position = map[nextDestination];
            }
        }
    }

    public void SolvePart2(object input)
    {
        if (input is not string[] inputData)
            throw new ArgumentException(nameof(input));

        var instructions = inputData[0];

        var startDestinations = inputData.Skip(2).Where(i => i[2] == 'A').Select(line => line.AsSpan(0, 3).ToString()).ToList();

        var map =
            inputData
                .Skip(2)
                .Select(line =>
                {
                    var key = line.AsSpan(0, 3).ToString();

                    var values = (line.AsSpan(7, 3).ToString(), line.AsSpan(12, 3).ToString());

                    return new KeyValuePair<string, (string, string)>(key, values);
                })
                .ToFrozenDictionary();

        var positions = new[]
        {
            map[startDestinations[0]], 
            map[startDestinations[1]],
            map[startDestinations[2]], 
            map[startDestinations[3]], 
            map[startDestinations[4]],
            map[startDestinations[5]]
        };

        var moves =
            positions
                .AsParallel()
                .Select(pos =>
                {
                    long moves = 0;
                    var position = pos;
                    while (true)
                    {
                        for (int index = 0; index < instructions.Length; index++)
                        {
                            moves++;

                            var nextDestination = GetDestination(instructions[index], position);

                            if (nextDestination[2] == 'Z')
                            {
                                Console.WriteLine(moves);
                                return moves;
                            }

                            position = map[nextDestination];
                        }
                    }
                })
                .ToArray();

        long lcm = moves[0];
        for (int i = 1; i < moves.Length; i++)
        {
            lcm = CalculateLeastCommonMultiple(lcm, moves[i]);
        }

        Console.WriteLine(lcm.ToString("N0"));
    }
    
    private static long CalculateLeastCommonMultiple(long a, long b)
    {
        return a / CalculatedGreatestCommonDivisor(a, b) * b;
    }

    private static long CalculatedGreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static string GetDestination(char instruction, (string, string) map) =>
        instruction == 'L' ? map.Item1 : map.Item2;
}
using System.Collections.Frozen;
using System.Diagnostics;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 8",
        "Dec8_Input.txt"));

var instructions = //"LLR";
    input[0];

var aaa = input.Skip(2).Where(i => i[2] == 'A').Select(line => line.AsSpan(0, 3).ToString()).ToList();

//var zzz = input.Skip(2).Where(i => i[2] == 'Z').ToList();

var map =
    //new Dictionary<string, (string, string)>
    //{
    //    ["AAA"] = ("BBB", "BBB"),
    //    ["BBB"] = ("AAA", "ZZZ"),
    //    ["ZZZ"] = ("ZZZ", "ZZZ")
    //};
    input
        .Skip(2)
        .Select(line =>
        {
            var key = line.AsSpan(0, 3).ToString();

            var values = (line.AsSpan(7, 3).ToString(), line.AsSpan(12, 3).ToString());

            return new KeyValuePair<string, (string, string)>(key, values);
        })
        .ToFrozenDictionary();

var stopWatch = Stopwatch.StartNew();
long moves = 0;
var positions = new List<(string, string)>{ map[aaa[0]], map[aaa[1]], map[aaa[2]], map[aaa[3]], map[aaa[4]], map[aaa[5]] };
while (true)
{
    for (int index = 0; index < instructions.Length; index++)
    {
        moves++;

        var nextDestinations = new List<string>()
            {
              GetDestination(instructions[index], positions[0]),
              GetDestination(instructions[index], positions[1]),
              GetDestination(instructions[index], positions[2]),
              GetDestination(instructions[index], positions[3]),
              GetDestination(instructions[index], positions[4]),
              GetDestination(instructions[index], positions[5])
            };

        if (nextDestinations.All(d => d.EndsWith("Z")))
        {
            Console.WriteLine(moves);
            stopWatch.Stop();
            Console.WriteLine($"It took {stopWatch.Elapsed.TotalMinutes} minutes to calculate {moves} moves.");
            return;
        }

        positions = new List<(string, string)>
        {
            map[nextDestinations[0]],
            map[nextDestinations[1]],
            map[nextDestinations[2]],
            map[nextDestinations[3]],
            map[nextDestinations[4]],
            map[nextDestinations[5]],
        };

        Console.Clear();
        Console.WriteLine(moves);
    }
}

//long moves = 0;
//var position = map["AAA"];
//while (true)
//{
//    for (int index = 0; index < instructions.Length; index++)
//    {
//        moves++;

//        var nextDestination = GetDestination(instructions[index], position);

//        if (nextDestination == "ZZZ")
//        {
//            Console.WriteLine(moves);
//            return;
//        }

//        position = map[nextDestination];
//    }
//}

static string GetDestination(char instruction, (string, string) map) =>
    instruction == 'L' ? map.Item1 : map.Item2;
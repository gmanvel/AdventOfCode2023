using AdventOfCode2023;
using AdventOfCode2023.December_7;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 7",
        "Dec7_Input.txt"));

var inputData =
    input
        .Select(line =>
        {
            var parts = line.Split(' ');
            return (parts[0], int.Parse(parts[1]));
        })
        .ToList();

IPuzzle puzzle = new December7();
puzzle.SolvePart1(inputData);
puzzle.SolvePart2(inputData);
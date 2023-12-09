using AdventOfCode2023;
using AdventOfCode2023.December_9;

var inputStr = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 9",
        "Dec9_Input.txt"));

var input =
    inputStr
        .Select(line =>
            line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList())
        .ToList();

IPuzzle puzzle = new December9();
puzzle.SolvePart1(input);
puzzle.SolvePart2(input);
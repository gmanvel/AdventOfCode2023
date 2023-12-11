using AdventOfCode2023;
using AdventOfCode2023.December_11;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 11",
        "Dec11_Input.txt"));

IPuzzle puzzle = new December11();
puzzle.SolvePart1(input);
puzzle.SolvePart2(input);
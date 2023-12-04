using AdventOfCode2023;
using AdventOfCode2023.December_4;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 4",
        "Dec4_Input.txt"));

IPuzzle puzzle = new December4();
puzzle.SolvePart1(input);
puzzle.SolvePart2(input);
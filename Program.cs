using AdventOfCode2023;
using AdventOfCode2023.December_2;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 2",
        "Dec2_Input.txt"));

IPuzzle puzzle = new December2();
puzzle.SolvePart1(input);
puzzle.SolvePart2(input);
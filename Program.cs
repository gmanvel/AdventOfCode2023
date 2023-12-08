using AdventOfCode2023;
using AdventOfCode2023.December_8;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 8",
        "Dec8_Input.txt"));

IPuzzle puzzle = new December8();
puzzle.SolvePart1(input);
puzzle.SolvePart2(input);
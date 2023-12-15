using AdventOfCode2023;
using AdventOfCode2023.December_15;

var text = File.ReadAllText(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 15",
        "Dec15_Input.txt"));

IPuzzle puzzle = new December15();
puzzle.SolvePart1(text);
//puzzle.SolvePart2(input);
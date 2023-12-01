using AdventOfCode2023;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 1",
        "Dec1_Input.txt"));

IPuzzle puzzle = new December1();
puzzle.Solve(input);


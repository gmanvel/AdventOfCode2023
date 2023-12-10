using AdventOfCode2023;
using AdventOfCode2023.December_10;

var inputStr = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 10",
        "Dec10_Input.txt"));

char[,] pipes =
    new char[140, 140];

(int, int) startPosition = (0, 0);
for (var index = 0; index < inputStr.Length; index++)
{
    for (var jindex = 0; jindex < inputStr[index].Length; jindex++)
    {
        var pipe = inputStr[index][jindex];
        if (pipe == 'S')
        {
            startPosition = (index, jindex);
        }
        pipes[index, jindex] = inputStr[index][jindex];
    }
}

IPuzzle puzzle = new December10();
puzzle.SolvePart1(new Dec10Input(pipes, startPosition));
// puzzle.SolvePart2(input);
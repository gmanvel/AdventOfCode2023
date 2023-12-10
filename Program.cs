using System.Runtime.CompilerServices;
using AdventOfCode2023;
using AdventOfCode2023.December_10;
using AdventOfCode2023.December_9;

var inputStr = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 10",
        "Dec10_Input.txt"));

char[,] pipes = new char[140, 140];

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



var moves = 1;
IPipe currentPosition = new DownPipe((startPosition.Item1 + 1, startPosition.Item2));
    //(startPosition.Item1 + 1, startPosition.Item2);
    //startPosition.Down();

while(true)
{
    currentPosition = currentPosition.Next(pipes);
    moves++;

    if (currentPosition is DeadEnd or Start)
    {
        break;
    }
    
}

var moves2 = 1;
IPipe currentPosition2 = new LeftFPipe((startPosition.Item1, startPosition.Item2 - 1));
//(startPosition.Item1 + 1, startPosition.Item2);
//startPosition.Down();

while(true)
{
    currentPosition2 = currentPosition2.Next(pipes);
    moves2++;

    if (currentPosition2 is DeadEnd or Start)
    {
        break;
    }
    
}

Console.WriteLine(moves / 2);
Console.WriteLine(moves2 / 2);

// var countOfChars = inputStr.Select(line => line.Length);
//
// Console.WriteLine(string.Join(Environment.NewLine, countOfChars));
//
// Console.WriteLine($"Count of S: {inputStr.Count(line => line.Contains("S"))}");

// var input =
//     inputStr
//         .Select(line =>
//             line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
//                 .Select(int.Parse)
//                 .ToList())
//         .ToList();
//
// IPuzzle puzzle = new December9();
// puzzle.SolvePart1(input);
// puzzle.SolvePart2(input);
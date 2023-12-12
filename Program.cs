using AdventOfCode2023;
using AdventOfCode2023.December_11;

var input = @"
???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1
".Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

var d = input[0];

var indexOfSpace = d.IndexOf(' ');

var data = d.Substring(0, indexOfSpace);
var metadata = d.Substring(indexOfSpace + 1);

var groups = data.Split('.');
var groupLengths = metadata.Split(',').Select(int.Parse).ToArray();

if (groups.Length == groupLengths.Length)
{
    
}

Console.WriteLine();

//var input = File.ReadAllLines(
//    Path.Combine(
//        AppDomain.CurrentDomain.BaseDirectory,
//        "December 11",
//        "Dec11_Input.txt"));

//IPuzzle puzzle = new December11();
//puzzle.SolvePart1(input);
//puzzle.SolvePart2(input);
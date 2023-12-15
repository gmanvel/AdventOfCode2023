using AdventOfCode2023;
using AdventOfCode2023.December_11;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 11",
        "Dec11_Input.txt"));

//input = File.ReadAllLines(
//    Path.Combine(
//        AppDomain.CurrentDomain.BaseDirectory,
//        "December 14",
//        "Dec14_Input.txt"));

var comparer = new RockComparer();
var str = "...#.....OO.O.O.OO#O.#.#.O.OO......#.#..OO.OO#O.OO.#.O.#....OO###.O.O.#..OO..#.O..O..O....#O..O#OO#O";
var modified = new string(str.OrderByDescending(static _ => _, comparer).ToArray());

var list = new List<string>();
for (int i = 0; i < input.Length; i++)
{
    var column = new char[input.Length];
    
    for (int j = 0; j < input[i].Length; j++)
    {
        column[j] = input[j][i];
    }

    //Array.Sort(column, new RockComparer());
    var originalString = new string(column);
    column = column.OrderByDescending(static k => k, new RockComparer()).ToArray();

    list.Add(new string(column));
}

//var item = list.Where(l => l[99] == 'O').ToList();

var counter = 0;
for (var i = 0; i < list.Count; i++)
{
    for (int j = 0; j < list.Count; j++)
    {
        if (list[i][j] == 'O')
            counter += list.Count - j;
    }
}

Console.WriteLine(counter);

//var dish = new char[100, 100];

//for (int i = 0; i < input.Length; i++)
//{
//    for (int j = 0; j < input[i].Length; j++)
//    {
//        dish[i, i] = input[i][j];
//    }
//}

//var list = new List<string>();
//for (int i = 0; i < 100; i++)
//{
//    dish[0][1] = 'b';

//    list.Add(dish[i][0..100]);
//}

//List<char> inputD = 
//    //['.', '0', '.', '.', '.', '#', '0', '.', '.', '0'];
//    //['0', '0', '.', '0', '.', '0', '.', '.', '#', '#'];
//inputD = inputD.OrderByDescending(_ => _, new RockComparer()).ToList();

Console.WriteLine();


class RockComparer : IComparer<char>
{
    public int Compare(char x, char y)
    {
        if (x.Equals(y))
        {
            return 0;
        }

        if (x == '#' || y == '#')
            return 0;

        if (x == 'O' && y == '.')
        {
            return 1;
        }

        if (x == '.' && y == 'O')
        {
            return -1;
        }

        throw new InvalidOperationException();
    }
}
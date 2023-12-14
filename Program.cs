using System.Text.RegularExpressions;

var input = File.ReadAllText(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 13",
        "Dec13_Input.txt"));

//var input =
//    @"#.##..##.
//..#.##.#.
//##......#
//##......#
//..#.##.#.
//..##..##.
//#.#.##.#.

//#...##..#
//#....#..#
//..##..###
//#####.##.
//#####.##.
//..##..###
//#....#..#";

var inputData = Regex.Split(input, @"\r?\n\r?\n");

var counter = 0;

Console.WriteLine(
inputData
    //.AsParallel()
    .Select(d =>
    {
        Console.Clear();
        Console.WriteLine($"Processing {++counter} out of {inputData.Length}");

        var listOfRows = d.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
        .Select(row => row.Select(c => c))
        .ToList();

        List<List<char>> listOfColumns = new();

        var data = d.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        for (var i = 0; i < data[0].Length; i++)
        {
            var l = new List<char>();
            for (var j = 0; j < data.Length; j++)
            {
                l.Add(data[j][i]);
            }
            listOfColumns.Add(l);
        }

        var rows = 0;
        for (var i = 0; i < listOfRows.Count - 1; i++)
        {
            var row = listOfRows[i];
            var nextRow = listOfRows[i + 1];

            if (row.SequenceEqual(nextRow))
            {
                rows = i + 1;

                var left = i - 1;
                var right = i + 2;
                while (left >= 0 && right <= listOfRows.Count - 1)
                {
                    if (listOfRows[left].SequenceEqual(listOfRows[right]) == false)
                    {
                        rows = 0;
                        break;
                    }

                    left--;
                    right++;
                }

                if (rows != 0)
                    break;
            }
        }

        if(rows != 0)
            return rows * 100;

        var columns = 0;
        for (var i = 0; i < listOfColumns.Count - 1; i++)
        {
            var row = listOfColumns[i];
            var nextRow = listOfColumns[i + 1];

            if (row.SequenceEqual(nextRow))
            {
                columns = i + 1;

                var left = i - 1;
                var right = i + 2;
                while (left >= 0 && right <= listOfColumns.Count - 1)
                {
                    if (listOfColumns[left].SequenceEqual(listOfColumns[right]) == false)
                    {
                        columns = 0;
                        break;
                    }

                    left--;
                    right++;
                }

                if (columns != 0)
                    break;
            }
        }

        return columns;
    })
    .Sum());

//var input =
//    @"
//#...##..#
//#....#..#
//..##..###
//#####.##.
//#####.##.
//..##..###
//#....#..#";

//    @"
//#.##..##.
//..#.##.#.
//##......#
//##......#
//..#.##.#.
//..##..##.
//#.#.##.#.
//";

//var listOfRows =
//    input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
//        .Select(row => row.Select(c => c))
//        .ToList();

//List<List<char>> listOfColumns = new();

//var data = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

//for (var i = 0; i < data[0].Length; i++)
//{
//    var l = new List<char>();
//    for (var j = 0; j < data.Length; j++)
//    {
//        l.Add(data[j][i]);
//    }
//    listOfColumns.Add(l);
//}

//var columns = -1;
//for (var i = 0; i < listOfColumns.Count - 1; i++)
//{
//    var row = listOfColumns[i];
//    var nextRow = listOfColumns[i + 1];

//    if (row.SequenceEqual(nextRow))
//    {
//        if (i*2 != listOfColumns.Count &&
//            i*2 != listOfColumns.Count - 1)
//        {
//            break;
//        }
//        else
//        {
//            columns = i + 1;

//            var left = i - 1;
//            var right = i + 2;
//            while (left == 0 || right == listOfColumns.Count - 1)
//            {
//                if (listOfColumns[left].SequenceEqual(listOfColumns[right]) == false)
//                {
//                    columns = -1;
//                    break;
//                }
//            }
            
//            break;
//        }
//    }
//}

//var rows = -1;
//for (var i = 0; i < listOfRows.Count - 1; i++)
//{
//    var row = listOfRows[i];
//    var nextRow = listOfRows[i + 1];

//    if (row.SequenceEqual(nextRow))
//    {
//        if (i * 2 != listOfRows.Count &&
//            i * 2 != listOfRows.Count - 1)
//        {
//            break;
//        }
//        else
//        {
//            rows = i + 1;

//            var left = i - 1;
//            var right = i + 2;
//            while (left == 0 || right == listOfRows.Count - 1)
//            {
//                if (listOfRows[left].SequenceEqual(listOfRows[right]) == false)
//                {
//                    rows = -1;
//                    break;
//                }
//            }

//            break;
//        }
//    }
//}

//Console.WriteLine($"Col={columns}, Rows={rows}");
//char[,] map = new char[7, 9];

//var data = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

//for (int i = 0; i < data.Length; i++)
//{
//    for (var j = 0; j < data[i].Length; j++)
//    {
//        map[i, j] = data[i][j];
//    }
//}

//for (int i = 0; i < map.Length - 1; i++)
//{
//    var row = map[i];
//    var nextRow = ma
//}

Console.WriteLine();

//IPuzzle puzzle = new December11();
//puzzle.SolvePart1(input);
//puzzle.SolvePart2(input);
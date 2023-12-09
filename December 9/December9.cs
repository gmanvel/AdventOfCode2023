namespace AdventOfCode2023.December_9;

public class December9 : IPuzzle
{
    public void SolvePart1(object input)
    {
        if (input is not List<List<int>> inputData)
            throw new ArgumentException(nameof(input));

        Console.WriteLine(
            inputData
                .AsParallel()
                .Select(list =>
                {
                    var lists = new List<List<int>>
                    {
                        list
                    };

                    List<int> originalList = list;
                    while (true)
                    {
                        var nextList = GetNextList(originalList);
                        lists.Add(nextList);

                        if (nextList.All(item => item == nextList[0]))
                        {
                            break;
                        }

                        originalList = nextList;
                    }

                    return lists.Sum(l => l.Last());
                })
                .Sum());
    }

    public void SolvePart2(object input)
    {
        if (input is not List<List<int>> inputData)
            throw new ArgumentException(nameof(input));

        Console.WriteLine(
            inputData
                .AsParallel()
                .Select(list =>
                {
                    var lists = new List<List<int>>
                    {
                        list
                    };

                    List<int> originalList = list;
                    while (true)
                    {
                        var nextList = GetNextList(originalList);
                        lists.Add(nextList);

                        if (nextList.All(item => item == nextList[0]))
                        {
                            break;
                        }

                        originalList = nextList;
                    }

                    var count = lists.Count;
                    var firstEl = lists[count - 2][0] - lists[count - 1][0];
        
                    for (var index = lists.Count - 3; index >= 0; index--)
                    {
                        firstEl = lists[index][0] - firstEl;
                    }

                    return firstEl;
                })
                .Sum());
    }
    
    private static List<int> GetNextList(List<int> list)
    {
        var nextList = new List<int>();
        for (var index = 0; index < list.Count; index++)
        {
            if (index == list.Count - 1)
            {
                break;
            }
            
            nextList.Add(list[index + 1] - list[index]);
        }

        return nextList;
    }
}
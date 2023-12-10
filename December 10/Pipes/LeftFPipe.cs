namespace AdventOfCode2023.December_10.Pipes;

public class LeftFPipe((int, int) position) : IPipe
{
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (position.Item1 + 1, position.Item2);
        
        var nextPipe = pipes[position.Item1 + 1, position.Item2];

        return nextPipe switch
        {
            '|' => new DownPipe(nextPosition),
            'L' => new DownLPipe(nextPosition),
            'J' => new DownJPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }

    public (int, int) Position => position;
}
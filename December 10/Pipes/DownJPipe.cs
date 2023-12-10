namespace AdventOfCode2023.December_10.Pipes;

public class DownJPipe((int, int) position) : IPipe
{
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (position.Item1, position.Item2 - 1);
        
        var nextPipe = pipes[position.Item1, position.Item2 - 1];

        return nextPipe switch
        {
            '-' => new LeftHorizontalPipe(nextPosition),
            'L' => new LeftLPipe(nextPosition),
            'F' => new LeftFPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }

    public (int, int) Position => position;
}
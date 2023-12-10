namespace AdventOfCode2023.December_10.Pipes;

public class RightHorizontalPipe((int, int) position) : IPipe
{
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (position.Item1, position.Item2 + 1);
        
        var nextPipe = pipes[position.Item1, position.Item2 + 1];

        return nextPipe switch
        {
            '-' => new RightHorizontalPipe(nextPosition),
            '7' => new Right7Pipe(nextPosition),
            'J' => new RightJPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }

    public (int, int) Position => position;
}
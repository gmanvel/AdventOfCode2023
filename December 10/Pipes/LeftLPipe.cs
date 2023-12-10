namespace AdventOfCode2023.December_10.Pipes;

public class LeftLPipe((int, int) position) : IPipe
{
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (position.Item1 - 1, position.Item2);
        
        var nextPipe = pipes[position.Item1 - 1, position.Item2];

        return nextPipe switch
        {
            '|' => new UpPipe(nextPosition),
            '7' => new Up7Pipe(nextPosition),
            'F' => new UpFPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }

    public (int, int) Position => position;
}
namespace AdventOfCode2023.December_10.Pipes;

public class Start : IPipe
{
    public IPipe Next(char[,] pipes)
    {
        throw new NotImplementedException();
    }

    public (int, int) Position => throw new InvalidOperationException();
}
namespace AdventOfCode2023.December_10.Pipes;

public interface IPipe
{
    IPipe Next(char[,] pipes);

    (int, int) Position { get; }
}
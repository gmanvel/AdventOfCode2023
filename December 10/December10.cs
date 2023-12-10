using AdventOfCode2023.December_10.Pipes;

namespace AdventOfCode2023.December_10
{
    internal class December10 : IPuzzle
    {
        public void SolvePart1(object input)
        {
            if (input is not Dec10Input dec10Input)
                throw new ArgumentException(nameof(input));

            var (pipes, startPosition) = dec10Input;

            var moves = 1;
            // Of course this shouldn't be hard coded, but should be figured out from the input
            // What can I say, Sunday, lazy day :)
            IPipe currentPosition = new DownPipe((startPosition.Item1 + 1, startPosition.Item2));

            while (true)
            {
                currentPosition = currentPosition.Next(pipes);
                moves++;

                if (currentPosition is DeadEnd or Start)
                {
                    break;
                }

            }

            Console.WriteLine(moves/2);
        }

        public void SolvePart2(object input)
        {
            throw new NotImplementedException();
        }
    }

    internal record Dec10Input(char[,] Pipes, (int, int) StartPosition);
}

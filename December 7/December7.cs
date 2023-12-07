namespace AdventOfCode2023.December_7;

public class December7 : IPuzzle
{
    public void SolvePart1(object input)
    {
        if (input is not List<(string Hand, int Rank)> cardData)
            throw new ArgumentException(nameof(input));

        Console.WriteLine(
            cardData
                .Select(tuple => (Hand: new Hand(tuple.Hand), Rank: tuple.Rank))
                .OrderBy(tuple => tuple.Hand)
                .Select((value, index) => value.Rank * (index + 1))
                .Sum());
    }

    public void SolvePart2(object input)
    {
        if (input is not List<(string Hand, int Rank)> cardData)
            throw new ArgumentException(nameof(input));

        Console.WriteLine(
            cardData
                .Select(tuple => (Hand: new HandV2(tuple.Hand), Rank: tuple.Rank))
                .OrderBy(tuple => tuple.Hand)
                .Select((value, index) => value.Rank * (index + 1))
                .Sum());
    }
}
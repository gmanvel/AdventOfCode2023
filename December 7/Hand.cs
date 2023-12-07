using System.Collections.Frozen;
using System.Diagnostics;

namespace AdventOfCode2023.December_7;

[DebuggerDisplay("{_hand}, Type = {HandType}")]
public class Hand : IComparable<Hand>
{
    private static readonly FrozenDictionary<char, Card> s_cardCharValueMap = new Dictionary<char, Card>
    {
        ['2'] = Card.Two,
        ['3'] = Card.Three,
        ['4'] = Card.Four,
        ['5'] = Card.Five,
        ['6'] = Card.Six,
        ['7'] = Card.Seven,
        ['8'] = Card.Eight,
        ['9'] = Card.Nine,
        ['T'] = Card.Ten,
        ['J'] = Card.Jack,
        ['Q'] = Card.Queen,
        ['K'] = Card.King,
        ['A'] = Card.Ace
    }.ToFrozenDictionary();

    private readonly string _hand;

    public List<Card> Cards { get; }

    public HandType HandType { get; private set; }

    public Hand(string hand)
    {
        _hand = hand;

        Cards = new();

        ParseCards(hand);

        SetHandType(hand);
    }

    private void SetHandType(string hand)
    {
        var grouped = hand.GroupBy(h => h).Select(group => new
        {
            group.Key,
            Values = group.Count()
        }).ToList();

        if (grouped.Count == 1)
        {
            HandType = HandType.FiveOfKind;
        }
        else if (grouped.Count == 2)
        {
            // if 5 cards are grouped into 2 groups, then it's either
            // 4 & 1
            // 3 & 2
            if (grouped.Any(grp => grp.Values == 4))
                HandType = HandType.FourOfKind;

            if (grouped.Any(grp => grp.Values == 3))
                HandType = HandType.FullHouse;
        }
        else if (grouped.Count == 3)
        {
            // if 5 cards are grouped into 3 groups, then it's either
            // 3, 1, 1
            // 2, 2, 1
            if (grouped.Any(grp => grp.Values == 3))
                HandType = HandType.ThreeOfKind;

            if (grouped.Count(grp => grp.Values == 2) == 2)
                HandType = HandType.TwoPair;
        }
        else if (grouped.Count == 4)
            HandType = HandType.OnePair;
        else
            HandType = HandType.HighCard;
    }

    private void ParseCards(string hand)
    {
        for (var index = 0; index < hand.Length; index++)
        {
            Cards.Add(s_cardCharValueMap[hand[index]]);
        }
    }

    public int CompareTo(Hand? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        
        var sameHand = HandType.CompareTo(other.HandType);

        if (sameHand != 0)
            return sameHand;

        for (int index = 0; index < Cards.Count; index++)
        {
            var compareResult = Cards[index].CompareTo(other.Cards[index]);

            if (compareResult == 0)
                continue;

            return compareResult;
        }

        return 0;
    }
}
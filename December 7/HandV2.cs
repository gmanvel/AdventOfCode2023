using System.Collections.Frozen;
using System.Diagnostics;

namespace AdventOfCode2023.December_7;

[DebuggerDisplay("{_hand}, Type = {HandType}")]
public class HandV2 : IComparable<HandV2>
{
    private static readonly FrozenDictionary<char, CardV2> s_cardCharValueMap = new Dictionary<char, CardV2>
    {
        ['2'] = CardV2.Two,
        ['3'] = CardV2.Three,
        ['4'] = CardV2.Four,
        ['5'] = CardV2.Five,
        ['6'] = CardV2.Six,
        ['7'] = CardV2.Seven,
        ['8'] = CardV2.Eight,
        ['9'] = CardV2.Nine,
        ['T'] = CardV2.Ten,
        ['J'] = CardV2.Jack,
        ['Q'] = CardV2.Queen,
        ['K'] = CardV2.King,
        ['A'] = CardV2.Ace
    }.ToFrozenDictionary();

    private readonly string _hand;

    public List<CardV2> Cards { get; }

    public HandType HandType { get; private set; }

    public HandV2(string hand)
    {
        _hand = hand;

        Cards = new();

        ParseCards(hand);

        SetHandTypeConsideringJokerRule(hand);
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

    private void SetHandTypeConsideringJokerRule(string hand)
    {
        var countOfJs = hand.AsSpan().Count('J');

        if (countOfJs == 0)
            SetHandType(hand);
        else
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
                // we have 2 groups, one of which are all Js
                // we can replace them with the cards from another group
                // so all will be the same, hence FiveOfKind
                HandType = HandType.FiveOfKind;
            }
            else if (grouped.Count == 3)
            {
                // if 5 cards are grouped into 3 groups, then it's either
                // 3, 1, 1
                // 2, 2, 1
                var maxNonJsInGroup = grouped.Where(grp => grp.Key != 'J').Max(grp => grp.Values);

                var replaced = maxNonJsInGroup + countOfJs;

                // now we have 2 groups, one group has replaced cards
                if (replaced == 4)
                    HandType = HandType.FourOfKind;

                if (replaced == 3)
                    HandType = HandType.FullHouse;
            }
            else if (grouped.Count == 4)
            {
                // we have 4 groups, which means one of them has 2 cards, the rest are a single card
                // whether those 2 cards are Js, or some others, we can replace J with that type
                // in any case we're going to end up with a 3 of kind
                HandType = HandType.ThreeOfKind;
            }
            else
                // all cards are different so the best we can do is OnePair
                HandType = HandType.OnePair;
        }
    }

    private void ParseCards(string hand)
    {
        for (var index = 0; index < hand.Length; index++)
        {
            Cards.Add(s_cardCharValueMap[hand[index]]);
        }
    }

    public int CompareTo(HandV2? other)
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
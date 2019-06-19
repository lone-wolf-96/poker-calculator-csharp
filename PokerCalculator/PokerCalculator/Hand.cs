using System;
using System.Linq;

namespace PokerCalculator
{
    public sealed class Hand
    {
        const int HandSize = 5;

        public HandRank HandRank { get; }

        public Card[] Cards { get; }

        public Hand(Card[] cards)
        {
            this.Cards = cards;

            this.HandRank = Evaluate();
        }

        public static Card[] FromString(string input)
        {
            string[] parts = input.Split(" ");

            return parts.Select(part => Card.FromString(part)).ToArray();
        }

        public string ToStringName()
        {
            string[] cardNames = Cards.Select(card => card.ToStringName()).ToArray();

            return string.Join("\n", cardNames);
        }

        private HandRank Evaluate()
        {
            bool isFlush = Cards.Select(card => card.Suit.GetSuitValue()).Distinct().Count() == 1;

            int[] rankNumbers = Cards.Select(card => card.Rank.GetRankNumber()).ToArray();

            Array.Sort(rankNumbers);

            bool isStraight = IsStraight(rankNumbers);

            if (isFlush && isStraight)
            {
                if (IsRoyal(Cards))
                {
                    return HandRank.ROYAL_FLUSH;
                }

                return HandRank.STRAIGHT_FLUSH;
            }

            var frequencyDictionary = Utility.GetFrequencyDictionary(rankNumbers);

            if (frequencyDictionary.ContainsValue(4))
            {
                return HandRank.FOUR_OF_A_KIND;
            }

            bool isThreeOfAKind = frequencyDictionary.ContainsValue(3);

            bool isOnePair = frequencyDictionary.ContainsValue(2);

            if (isThreeOfAKind && isOnePair)
            {
                return HandRank.FULL_HOUSE;
            }
            if (isFlush)
            {
                return HandRank.FLUSH;
            }
            if (isStraight)
            {
                return HandRank.STRAIGHT;
            }
            if (isThreeOfAKind)
            {
                return HandRank.THREE_OF_A_KIND;
            }
            if (isOnePair)
            {
                if ((frequencyDictionary.Values.Count(val => val == 2)) == 2)
                {
                    return HandRank.TWO_PAIRS;
                }

                return HandRank.ONE_PAIR;
            }

            return HandRank.HIGH_CARD;
        }

        private bool IsStraight(int[] rankNumbers)
        {
            rankNumbers = Utility.ReplaceAceForOneIf(rankNumbers);

            int[] sequentialRanks = Enumerable.Range(rankNumbers[0], HandSize).ToArray();

            return Enumerable.SequenceEqual(sequentialRanks, rankNumbers);
        }

        private bool IsRoyal(Card[] cardsInHand)
        {
            int ten = Rank.TEN.GetRankNumber();

            int ace = Rank.ACE.GetRankNumber();

            return cardsInHand.All(card =>
            {
                int cardNumber = card.Rank.GetRankNumber();

                return ten <= cardNumber && cardNumber <= ace;
            });
        }

        public override string ToString()
        {
            string[] cardNames = Cards.Select(card => card.ToString()).ToArray();

            return string.Join(" ", cardNames);
        }
    }
}

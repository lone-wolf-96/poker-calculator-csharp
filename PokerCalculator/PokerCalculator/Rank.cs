using System;

namespace PokerCalculator
{
    public enum Rank
    {
        [RankAttribute(2, '2')] TWO, [RankAttribute(3, '3')] THREE,
        [RankAttribute(4, '4')] FOUR, [RankAttribute(5, '5')] FIVE,
        [RankAttribute(6, '6')] SIX, [RankAttribute(7, '7')] SEVEN,
        [RankAttribute(8, '8')] EIGHT, [RankAttribute(9, '9')] NINE,
        [RankAttribute(10, 'T')] TEN, [RankAttribute(11, 'J')] JACK,
        [RankAttribute(12, 'Q')] QUEEN, [RankAttribute(13, 'K')] KING,
        [RankAttribute(14, 'A')] ACE
    }

    internal static class Ranks
    {
        private static System.Collections.Generic.Dictionary<char, Rank> rankDictionary = FillRanks();

        private static System.Collections.Generic.Dictionary<char, Rank> FillRanks()
        {
            var values = Utility.GetEnumValues<Rank>();

            var newDictionary = new System.Collections.Generic.Dictionary<char, Rank>();

            foreach (var rank in values)
            {
                newDictionary.Add(rank.GetRankValue(), rank);
            }

            return newDictionary;
        }

        public static Rank GetRankByRankValue(char rankValue)
        {
            return rankDictionary[rankValue];
        }

        public static int GetRankNumber(this Rank rank)
        {
            return Utility.GetAttribute<RankAttribute, Rank>(rank).RankNumber;
        }

        public static char GetRankValue(this Rank rank)
        {
            return Utility.GetAttribute<RankAttribute, Rank>(rank).RankValue;
        }
    }

    internal sealed class RankAttribute : Attribute
    {
        internal RankAttribute(int rankNumber, char rankValue)
        {
            this.RankNumber = rankNumber;

            this.RankValue = rankValue;
        }

        public int RankNumber { get; private set; }

        public char RankValue { get; private set; }
    }
}

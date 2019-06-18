using System;

namespace PokerCalculator
{
    public enum Suit
    {
        [SuitAttribute('S')] SPADES,
        [SuitAttribute('H')] HEARTS,
        [SuitAttribute('C')] CLUBS,
        [SuitAttribute('D')] DIAMONDS
    }

    internal static class Suits
    {
        private static System.Collections.Generic.Dictionary<char, Suit> suitDictionary = FillSuits();

        private static System.Collections.Generic.Dictionary<char, Suit> FillSuits()
        {
            var values = Utility.GetEnumValues<Suit>();

            var newDictionary = new System.Collections.Generic.Dictionary<char, Suit>();

            foreach (var suit in values)
            {
                newDictionary.Add(suit.GetSuitValue(), suit);
            }

            return newDictionary;
        }

        public static Suit GetSuitBySuitValue(char suitValue)
        {
            return suitDictionary[suitValue];
        }

        public static char GetSuitValue(this Suit suit)
        {
            return Utility.GetAttribute<SuitAttribute, Suit>(suit).SuitValue;
        }
    }

    internal sealed class SuitAttribute : Attribute
    {
        internal SuitAttribute(char suitValue)
        {
            this.SuitValue = suitValue;
        }

        public char SuitValue { get; private set; }
    }
}

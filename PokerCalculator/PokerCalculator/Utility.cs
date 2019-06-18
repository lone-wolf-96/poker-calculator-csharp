using System;
using System.Linq;
using System.Reflection;

namespace PokerCalculator
{
    internal static class Utility
    {
        internal static int[] ReplaceAceForOneIf(int[] rankNumbers)
        {
            bool hasAceAndTwo = rankNumbers.Contains(14) && rankNumbers.Contains(2);

            if (hasAceAndTwo)
            {
                rankNumbers = rankNumbers.Select(rankN => rankN == 14 ? 1 : rankN).ToArray();

                Array.Sort(rankNumbers);
            }

            return rankNumbers;
        }

        internal static System.Collections.Generic.Dictionary<int, int> GetFrequencyDictionary(int[] rankNumbers)
        {
            return rankNumbers.GroupBy(rankN => rankN)
                .ToDictionary(rankN => rankN.Key, rankN => rankN.Count());
        }

        internal static T[] GetEnumValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        internal static T GetAttribute<T, U>(U selectedEnum) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(
                Utility.ForValue<U>(selectedEnum), typeof(T));
        }

        internal static MemberInfo ForValue<U>(U selectedEnum)
        {
            return typeof(U).GetField(System.Enum.GetName(typeof(U), selectedEnum));
        }
    }
}

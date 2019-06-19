using System;
using System.IO;
using System.Linq;

namespace PokerCalculator
{
    public sealed class Calculator
    {
        public int[] Winners { get; }

        public int Games { get; private set; }

        private String[] Messages { get; set; }

        public Calculator(string filePath)
        {
            Winners = new int[] { 0, 0, 0 };

            Games = 0;

            Calculate(filePath);
        }

        public bool PrintResults(string filePath)
        {
            try
            {
                string dateFormat = "Date and Time: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                using (StreamWriter steamWriterReport =
                    new StreamWriter(filePath + "poker_results_report.txt"))
                using (StreamWriter steamWriterResult =
                    new StreamWriter(filePath + "poker_results.txt"))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        steamWriterReport.WriteLine((i + 1) + ": " + Winners[i]);
                    }

                    String p1Percentage = (((float)Winners[0] / Games) * 100).ToString("F");

                    String p2Percentage = (((float)Winners[1] / Games) * 100).ToString("F");

                    steamWriterReport.WriteLine("4:");

                    steamWriterReport.WriteLine("--------- PLAYER 1 --------- | --------- PLAYER 2 ---------");

                    steamWriterReport.WriteLine("           " + p1Percentage + "%            |             " +
                    p2Percentage + "%          ");

                    steamWriterReport.WriteLine("---------------------------- | ----------------------------");

                    steamWriterReport.WriteLine("Total Games: " + Games);

                    foreach (string message in Messages)
                    {
                        steamWriterResult.WriteLine(message);
                    }

                    steamWriterReport.Write(dateFormat);

                    steamWriterResult.Write(dateFormat);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

                Console.WriteLine(e);

                return false;
            }
        }

        private void Calculate(string filePath)
        {
            string pokerData = GetPokerData(filePath);

            if (pokerData == null)
            {
                return;
            }

            string[] pokerArray = pokerData.Split("-");

            Games = pokerArray.Length;

            Messages = new string[Games];

            for (int i = 0; i < Games; i++)
            {
                String[] gameArray = pokerArray[i].Split(" ");

                int n = (gameArray.Length + 1) / 2;

                String handString1 = string.Join(" ", gameArray.Take(n));

                String handString2 = string.Join(" ", gameArray.Skip(n));

                Hand hand1 = new Hand(Hand.FromString(handString1));

                Hand hand2 = new Hand(Hand.FromString(handString2));

                int winnerIndex = CheckWinner(hand1, hand2);
                string winnerMessage = (winnerIndex < 2 ? "Winner " + (winnerIndex + 1) : "Draw");

                Messages[i] = ($"{ winnerMessage } : " +
                    hand1.HandRank.ToString("g").Replace('_', ' ') + " - " +
                    hand2.HandRank.ToString("g").Replace('_', ' '));

                Winners[winnerIndex]++;
            }
        }

        private string GetPokerData(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                return String.Join("-", lines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

                Console.WriteLine(e);

                return null;
            }
        }

        private int BreakTie(Hand hand1, Hand hand2)
        {
            HandRank rank = hand1.HandRank;

            if (rank == HandRank.ROYAL_FLUSH)
            {
                return 2;
            }

            var cardsInHand1 = hand1.Cards;

            var cardsInHand2 = hand2.Cards;

            int[] rankNumbers1 = cardsInHand1.Select(card => card.Rank.GetRankNumber()).ToArray();

            Array.Sort(rankNumbers1);

            int[] rankNumbers2 = cardsInHand2.Select(card => card.Rank.GetRankNumber()).ToArray();

            Array.Sort(rankNumbers2);

            if (rank == HandRank.STRAIGHT_FLUSH || rank == HandRank.STRAIGHT)
            {
                return BreakTieStraight(rankNumbers1, rankNumbers2);
            }

            if (rank == HandRank.FLUSH || rank == HandRank.HIGH_CARD)
            {
                return BreakTieHighCard(rankNumbers1, rankNumbers2);
            }

            int tieBreaker = BreakTieRestHelper(rank);

            if (tieBreaker == -1)
            {
                return 2;
            }

            return BreakTieRest(rankNumbers1, rankNumbers2, tieBreaker);
        }

        private int BreakTieRestHelper(HandRank rank)
        {
            switch (rank)
            {
                case HandRank.FOUR_OF_A_KIND:
                    return 4;

                case HandRank.FULL_HOUSE:
                case HandRank.THREE_OF_A_KIND:
                    return 3;

                case HandRank.TWO_PAIRS:
                case HandRank.ONE_PAIR:
                    return 2;

                default:
                    return -1;
            }
        }

        private int BreakTieRest(int[] rankNumbers1, int[] rankNumbers2, int tieBreaker)
        {
            var frequencyDictionary1 = Utility.GetFrequencyDictionary(rankNumbers1);

            var frequencyDictionary2 = Utility.GetFrequencyDictionary(rankNumbers2);

            int frequentEqualN1 = frequencyDictionary1.Keys.SingleOrDefault(key => frequencyDictionary1[key] == tieBreaker);

            int frequentEqualN2 = frequencyDictionary2.Keys.SingleOrDefault(key => frequencyDictionary2[key] == tieBreaker);

            int winners = CheckWinnerHelper(frequentEqualN1, frequentEqualN2);

            if (winners != 2)
            {
                return winners;
            }

            rankNumbers1 = rankNumbers1.Where(rankN => rankN != frequentEqualN1).ToArray();

            rankNumbers2 = rankNumbers2.Where(rankN => rankN != frequentEqualN2).ToArray();

            return BreakTieHighCard(rankNumbers1, rankNumbers2);
        }

        private int BreakTieStraight(int[] rankNumbers1, int[] rankNumbers2)
        {
            int maxRank1 = Utility.ReplaceAceForOneIf(rankNumbers1).Max();

            int maxRank2 = Utility.ReplaceAceForOneIf(rankNumbers2).Max();

            return CheckWinnerHelper(maxRank1, maxRank2);
        }

        private int BreakTieHighCard(int[] rankNumbers1, int[] rankNumbers2)
        {
            int winners = 2;

            for (int i = rankNumbers1.Length - 1; i >= 0; i--)
            {
                winners = CheckWinnerHelper(rankNumbers1[i], rankNumbers2[i]);

                if (winners != 2)
                {
                    return winners;
                }
            }

            return winners;
        }

        private int CheckWinner(Hand hand1, Hand hand2)
        {
            int winners = CheckWinnerHelper((int)hand1.HandRank, (int)hand2.HandRank);

            return winners != 2 ? winners : BreakTie(hand1, hand2);
        }

        private int CheckWinnerHelper(int num1, int num2)
        {
            int resultComparer = num1.CompareTo(num2);

            return (resultComparer + 2) % 3;
        }

        public override String ToString()
        {
            return "Total Games: " + Games + "\n" + "Player 1: " + Winners[0] + "\n" + "Player 2: " + Winners[1] + "\n"
                    + "Tie: " + Winners[2];
        }
    }
}

using System;
using System.IO;
using System.Linq;

namespace PokerCalculator
{
    public sealed class Calculator
    {
        public int[] Winners { get; }

        public int Games { get; private set; }

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
                using (StreamWriter steamWriter = new StreamWriter(filePath))
                {
                    steamWriter.WriteLine("Total Games: " + Games);

                    for (int i = 0; i < 3; i++)
                    {
                        steamWriter.WriteLine((i + 1) + ": " + Winners[i]);
                    }

                    String p1Percentage = (((float)Winners[0] / Games) * 100).ToString("F");

                    String p2Percentage = (((float)Winners[1] / Games) * 100).ToString("F");

                    steamWriter.WriteLine("4:");

                    steamWriter.WriteLine("--------- PLAYER 1 --------- | --------- PLAYER 2 ---------");

                    steamWriter.WriteLine("           " + p1Percentage + "%            |             " +
                    p2Percentage + "%          ");

                    steamWriter.WriteLine("---------------------------- | ----------------------------");

                    steamWriter.Write("Date and Time: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));

                    return true;
                }
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

            foreach (string game in pokerArray)
            {
                String[] gameArray = game.Split(" ");

                int n = (gameArray.Length + 1) / 2;

                String handString1 = string.Join(" ", gameArray.Take(n));

                String handString2 = string.Join(" ", gameArray.Skip(n));

                Hand hand1 = new Hand(Hand.FromString(handString1));

                Hand hand2 = new Hand(Hand.FromString(handString2));

                Winners[CheckWinner(hand1, hand2)]++;
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

        private int CheckWinner(Hand hand1, Hand hand2)
        {
            int winners = CheckWinnerHelper((int)hand1.HandRank, (int)hand2.HandRank);

            return winners != 2 ? winners : -100/* BreakTie(hand1, hand2) */;
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

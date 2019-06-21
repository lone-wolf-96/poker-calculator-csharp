using System;
using Xunit;
using PokerCalculator;

namespace PokerCalculatorTests
{
    public class CalculatorTest
    {
        // Try your own current directory
        private static readonly String FilePathSource = System.IO.Directory.GetCurrentDirectory() + "\\" + "pokerdata.txt";

        private static readonly Calculator Calculator = new Calculator(FilePathSource);

        [Fact]
        public void TestCalculator()
        {
            new Calculator(FilePathSource);
        }

        [Theory]
        [InlineData(new int[] { 376, 624, 0 })]
        public void TestGetWinners(int[] winners)
        {
            Assert.Equal(winners, Calculator.Winners);
        }

        [Theory]
        [InlineData(1000)]
        public void TestGetGames(int games)
        {
            Assert.Equal(games, Calculator.Games);
        }

        [Fact]
        public void TestPrintResults()
        {
            String filePathTarget = System.IO.Directory.GetCurrentDirectory() + "\\";

            Assert.True(Calculator.PrintResults(filePathTarget));
        }

        [Theory]
        [InlineData("Total Games: 1000\nPlayer 1: 376\nPlayer 2: 624\nTie: 0")]
        public void TestToString(string toString)
        {
            Assert.Equal(toString, Calculator.ToString());
        }
    }
}

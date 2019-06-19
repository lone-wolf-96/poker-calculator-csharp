using System;
using Xunit;
using PokerCalculator;

namespace PokerCalculatorTests
{
    public class CalculatorTest
    {
        // Try your own current directory
        private readonly String FilePathSource = System.IO.Directory.GetCurrentDirectory() + "\\" + "pokerdata.txt";

        [Fact]
        public void TestCalculator()
        {
            new Calculator(FilePathSource);
        }

        [Theory]
        [InlineData(new int[] { 376, 624, 0 })]
        public void TestGetWinners(int[] winners)
        {
            Assert.Equal(winners, new Calculator(FilePathSource).Winners);
        }

        [Theory]
        [InlineData(1000)]
        public void TestGetGames(int games)
        {
            Assert.Equal(games, new Calculator(FilePathSource).Games);
        }

        [Fact]
        public void TestPrintResults()
        {
            String filePathTarget = System.IO.Directory.GetCurrentDirectory() + "\\";

            Assert.True(new Calculator(FilePathSource).PrintResults(filePathTarget));
        }

        [Theory]
        [InlineData("Total Games: 1000\nPlayer 1: 376\nPlayer 2: 624\nTie: 0")]
        public void TestToString(string toString)
        {
            Assert.Equal(toString, new Calculator(FilePathSource).ToString());
        }
    }
}

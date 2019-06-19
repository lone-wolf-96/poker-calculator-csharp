using Xunit;
using PokerCalculator;

namespace PokerCalculatorTests
{
    public class CardTest
    {
        [Theory]
        [InlineData(Rank.JACK, Suit.SPADES)]
        [InlineData(Rank.ACE, Suit.DIAMONDS)]
        [InlineData(Rank.KING, Suit.CLUBS)]
        public void TestCard(Rank rank, Suit suit)
        {
            new Card(rank, suit);
        }

        [Theory]
        [InlineData(Rank.TWO, Suit.DIAMONDS)]
        [InlineData(Rank.FIVE, Suit.SPADES)]
        [InlineData(Rank.TEN, Suit.CLUBS)]
        [InlineData(Rank.SIX, Suit.HEARTS)]
        [InlineData(Rank.SEVEN, Suit.DIAMONDS)]
        public void TestGetRank(Rank expectedRank, Suit suit)
        {
            Assert.Equal(expectedRank, new Card(expectedRank, suit).Rank);
        }

        [Theory]
        [InlineData(Suit.DIAMONDS, Rank.SIX)]
        [InlineData(Suit.SPADES, Rank.ACE)]
        [InlineData(Suit.CLUBS, Rank.QUEEN)]
        [InlineData(Suit.HEARTS, Rank.THREE)]
        public void TestGetSuit(Suit expectedSuit, Rank rank)
        {
            Assert.Equal(expectedSuit, new Card(rank, expectedSuit).Suit);
        }

        [Theory]
        [InlineData("TC", Rank.TEN, Suit.CLUBS)]
        [InlineData("JH", Rank.JACK, Suit.HEARTS)]
        [InlineData("8D", Rank.EIGHT, Suit.DIAMONDS)]
        public void TestFromString(string cardString, Rank expectedRank, Suit expectedSuit)
        {
            Card obtainedCard = Card.FromString(cardString);

            Assert.Equal(expectedRank, obtainedCard.Rank);

            Assert.Equal(expectedSuit, obtainedCard.Suit);
        }

        [Theory]
        [InlineData("TWO OF CLUBS", Rank.TWO, Suit.CLUBS)]
        [InlineData("ACE OF SPADES", Rank.ACE, Suit.SPADES)]
        public void TestToStringName(string cardString, Rank rank, Suit suit)
        {
            Assert.Equal(cardString, new Card(rank, suit).ToStringName());
        }

        [Theory]
        [InlineData("7D", Rank.SEVEN, Suit.DIAMONDS)]
        [InlineData("KH", Rank.KING, Suit.HEARTS)]
        public void TestToString(string cardString, Rank rank, Suit suit)
        {
            Assert.Equal(cardString, new Card(rank, suit).ToString());
        }
    }
}

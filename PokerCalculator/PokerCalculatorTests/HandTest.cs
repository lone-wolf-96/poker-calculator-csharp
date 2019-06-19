using System;
using Xunit;
using PokerCalculator;

namespace PokerCalculatorTests
{
    public class HandTest
    {
        [Fact]
        public void TestHand()
        {
            new Hand(new Card[] { new Card(Rank.JACK, Suit.SPADES), new Card(Rank.FOUR, Suit.HEARTS),
                                new Card(Rank.FIVE, Suit.DIAMONDS), new Card(Rank.NINE, Suit.DIAMONDS),
                                new Card(Rank.JACK, Suit.CLUBS) });
        }

        [Fact]
        public void TestGetCards()
        {
            Card[] cards1 = new Card[] { new Card(Rank.JACK, Suit.SPADES),
                                new Card(Rank.FOUR, Suit.HEARTS), new Card(Rank.FIVE, Suit.DIAMONDS),
                                new Card(Rank.NINE, Suit.DIAMONDS), new Card(Rank.JACK, Suit.CLUBS) };

            Assert.Equal(cards1, new Hand(cards1).Cards);

            Card[] cards2 = new Card[] { new Card(Rank.QUEEN, Suit.CLUBS), new Card(Rank.TEN, Suit.CLUBS),
                                new Card(Rank.SIX, Suit.HEARTS), new Card(Rank.FIVE, Suit.SPADES),
                                new Card(Rank.THREE, Suit.HEARTS) };

            Assert.Equal(cards2, new Hand(cards2).Cards);
        }

        [Fact]
        public void TestGetHandRank()
        {
            Card[] cards1 = new Card[] { new Card(Rank.JACK, Suit.SPADES),
                                new Card(Rank.FOUR, Suit.HEARTS), new Card(Rank.FIVE, Suit.DIAMONDS),
                                new Card(Rank.NINE, Suit.DIAMONDS), new Card(Rank.JACK, Suit.CLUBS) };

            Assert.Equal(HandRank.ONE_PAIR, new Hand(cards1).HandRank);

            Card[] cards2 = new Card[] { new Card(Rank.QUEEN, Suit.CLUBS), new Card(Rank.TEN, Suit.CLUBS),
                                new Card(Rank.SIX, Suit.HEARTS), new Card(Rank.FIVE, Suit.SPADES),
                                new Card(Rank.THREE, Suit.HEARTS) };

            Assert.Equal(HandRank.HIGH_CARD, new Hand(cards2).HandRank);
        }

        [Theory]
        [InlineData("6C 3D TH KC 4S",
            new Rank[] { Rank.SIX, Rank.THREE, Rank.TEN, Rank.KING, Rank.FOUR },
            new Suit[] { Suit.CLUBS, Suit.DIAMONDS, Suit.HEARTS, Suit.CLUBS, Suit.SPADES })]
        public void TestFromString(string handString, Rank[] ranks, Suit[] suits)
        {
            Card[] cards = Hand.FromString(handString);

            for (int i = 0; i < cards.Length; i++)
            {
                Assert.Equal(ranks[i], cards[i].Rank);

                Assert.Equal(suits[i], cards[i].Suit);
            }
        }

        [Fact]
        public void TestToStringName()
        {
            Card[] cards1 = new Card[] { new Card(Rank.JACK, Suit.SPADES),
                                new Card(Rank.FOUR, Suit.HEARTS), new Card(Rank.FIVE, Suit.DIAMONDS),
                                new Card(Rank.NINE, Suit.DIAMONDS), new Card(Rank.JACK, Suit.CLUBS) };

            Assert.Equal("JACK OF SPADES\nFOUR OF HEARTS\nFIVE OF DIAMONDS\nNINE OF DIAMONDS\nJACK OF CLUBS",
                            new Hand(cards1).ToStringName());

            Card[] cards2 = new Card[] { new Card(Rank.QUEEN, Suit.CLUBS), new Card(Rank.TEN, Suit.CLUBS),
                                new Card(Rank.SIX, Suit.HEARTS), new Card(Rank.FIVE, Suit.SPADES),
                                new Card(Rank.THREE, Suit.HEARTS) };

            Assert.Equal("QUEEN OF CLUBS\nTEN OF CLUBS\nSIX OF HEARTS\nFIVE OF SPADES\nTHREE OF HEARTS",
                            new Hand(cards2).ToStringName());
        }

        [Fact]
        public void TestToString()
        {
            Card[] cards1 = new Card[] { new Card(Rank.JACK, Suit.SPADES),
                                new Card(Rank.FOUR, Suit.HEARTS), new Card(Rank.FIVE, Suit.DIAMONDS),
                                new Card(Rank.NINE, Suit.DIAMONDS), new Card(Rank.JACK, Suit.CLUBS) };

            Assert.Equal("JS 4H 5D 9D JC", new Hand(cards1).ToString());

            Card[] cards2 = new Card[] { new Card(Rank.QUEEN, Suit.CLUBS), new Card(Rank.TEN, Suit.CLUBS),
                                new Card(Rank.SIX, Suit.HEARTS), new Card(Rank.FIVE, Suit.SPADES),
                                new Card(Rank.THREE, Suit.HEARTS) };

            Assert.Equal("QC TC 6H 5S 3H", new Hand(cards2).ToString());
        }
    }
}

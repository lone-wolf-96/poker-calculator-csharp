namespace PokerCalculator
{
    public sealed class Card
    {
        public Rank Rank { get; }

        public Suit Suit { get; }

        public Card(Rank rank, Suit suit)
        {
            this.Rank = rank;

            this.Suit = suit;
        }

        public static Card FromString(string input)
        {
            Rank rank = Ranks.GetRankByRankValue(input[0]);

            Suit suit = Suits.GetSuitBySuitValue(input[1]);

            return new Card(rank, suit);
        }

        public string ToStringName()
        {
            string rankName = Rank.ToString("g");

            string suitName = Suit.ToString("g");

            return rankName + " OF " + suitName;
        }

        public override string ToString()
        {
            char rankChar = Rank.GetRankValue();

            char suitChar = Suit.GetSuitValue();

            return rankChar + "" + suitChar;
        }
    }
}

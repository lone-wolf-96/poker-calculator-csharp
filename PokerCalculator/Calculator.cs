namespace PokerCalculator
{
    public sealed class Calculator
    {
        public int[] Winners { get; set; }
        public int Games { get; set; }

        public Calculator(string filePath)
        {
            Winners = new int[] { 0, 0, 0 };

            Games = 0;

            Calculate(filePath);
        }

        public bool PrintResults(string filePath)
        {
            return true;
        }

        public void Calculate(string filePath)
        {

        }
    }
}

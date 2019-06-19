using System;

namespace PokerCalculator
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter the pokerdata.txt source directory (Press Enter for default):");

                String nextLine = Console.ReadLine().Trim();

                String sourcePath = GetFolder(nextLine) + "pokerdata.txt";

                Console.WriteLine("Enter the target directory (Press Enter for default):");

                nextLine = Console.ReadLine().Trim();

                String targetPath = GetFolder(nextLine);

                if (new Calculator(sourcePath).PrintResults(targetPath))
                {
                    Console.WriteLine("Successful results in your folder.");
                }
                else
                {
                    Console.WriteLine("There's been an error processing the information.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

                Console.WriteLine(e);
            }
        }

        private static string GetFolder(string line)
        {
            if (line.Length > 0)
            {
                return line + (line[line.Length - 1] == '\\' ? "" : "\\");
            }

            return System.IO.Directory.GetCurrentDirectory() + "\\";
        }
    }
}

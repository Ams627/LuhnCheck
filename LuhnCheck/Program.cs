using System;
using System.IO;
using System.Linq;

namespace LuhnCheck
{

    internal class Program
    {
        public static bool Luhn(string digits)
        {
            return digits.All(char.IsDigit) && digits.Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
        }
        private static void Main(string[] args)
        {
            try
            {
                var rand = new Random();
                string prefix = "633157";
                for (int i = 0; i < 10; i++)
                {
                    var number = rand.Next() % 1_000_000_000;
                    var numberAsString = $"{number:D9}";
                    for (int j = 0; j < 9; j++)
                    {
                        var numToCheck = prefix + numberAsString + Convert.ToChar(j + 48);
                        if (Luhn(numToCheck))
                        {
                            Console.WriteLine($"{numToCheck}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var codeBase = System.Reflection.Assembly.GetEntryAssembly().CodeBase;
                var progname = Path.GetFileNameWithoutExtension(codeBase);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}

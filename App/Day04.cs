using System.Text.RegularExpressions;

namespace AdventOfCode2023.App
{
    internal static class Day04
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Inputs/04.txt");

            var res1 = 0;
            var inputWithCounter = input.Select(x => (1, x)).ToArray();
            int i = 1;
            foreach (var (counter, line) in inputWithCounter)
            {
                // PART 1
                var splitLine = line.Split(":|".ToCharArray());
                var winningNumbers = Regex.Split(splitLine[1], @"\D+").Where(x => !string.IsNullOrEmpty(x));
                var myNumbers = Regex.Split(splitLine[2], @"\D+").Where(x => !string.IsNullOrEmpty(x));
                var correspondingNumbers = winningNumbers.Intersect(myNumbers).Count();
                res1 += (int)Math.Pow(2.0, (double)correspondingNumbers - 1);

                // PART 2
                for (int j = i; j < i + correspondingNumbers && j <= input.Count(); j++)
                {
                    inputWithCounter[j].Item1 += counter;
                }
                i++;
            }

            Console.WriteLine($"Part 1: {res1}");
            Console.WriteLine($"Part 2: {inputWithCounter.Sum(x => x.Item1)}");
            Console.ReadLine();
        }
    }
}

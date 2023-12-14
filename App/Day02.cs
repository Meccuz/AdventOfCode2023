using System.Text.RegularExpressions;

namespace AdventOfCode2023.App
{
    internal static class Day02
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Inputs/02.txt");

            int redMax = 12;
            int greenMax = 13;
            int blueMax = 14;
            int i = 1;
            var res1 = 0;
            var res2 = 0;
            var redRegex = new Regex(@"(\d+) red");
            var greenRegex = new Regex(@"(\d+) green");
            var blueRegex = new Regex(@"(\d+) blue");

            foreach (var line in input)
            {
                // PART 1
                if (GetMax(line, redRegex) <= redMax &&
                    GetMax(line, greenRegex) <= greenMax &&
                    GetMax(line, blueRegex) <= blueMax)
                {
                    res1 += i;
                }
                i++;

                // PART 2
                var minRed = GetMax(line, redRegex);
                var minGreen = GetMax(line, greenRegex);
                var minBlue = GetMax(line, blueRegex);

                res2 += minRed * minGreen * minBlue;
            }

            Console.WriteLine($"Part 1: {res1}");
            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static int GetMax(string line, Regex redRegex)
        {
            return redRegex.Matches(line)
                .Select(x => int.Parse(x.Groups[1].Value))
                .Max();
        }
    }
}

using System.Text.RegularExpressions;

namespace AdventOfCode2023.App
{
    internal static class Day01
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Inputs/01.txt");

            // PART 1
            List<int> numbers = new List<int>();

            foreach (var line in input)
            {
                var res = Regex.Matches(line, @"\d");
                if (res.Count == 1)
                {
                    numbers.Add(int.Parse($"{res[0]}{res[0]}"));
                }
                if (res.Count > 1)
                {
                    numbers.Add(int.Parse($"{res[0]}{res[res.Count - 1]}"));
                }
            }

            Console.WriteLine($"Part 1: {numbers.Sum()}");

            // PART 2
            numbers = new List<int>();
            foreach (var line in input)
            {
                var res = Regex.Matches(line, @"(?=(\d|one|two|three|four|five|six|seven|eight|nine))")
                    .Select(x => ParseDigits(x))
                    .ToList();

                int number;

                if (res.Count == 1)
                {
                    number = int.Parse($"{res[0]}{res[0]}");
                }
                else
                {
                    number = int.Parse($"{res[0]}{res[res.Count - 1]}");
                }
                numbers.Add(number);
            }

            Console.WriteLine($"Part 2: {numbers.Sum()}");
            Console.ReadLine();
        }

        private static int ParseDigits(Match x)
        {
            var val = !string.IsNullOrEmpty(x.Groups[0].Value) ? x.Groups[0].Value : x.Groups[1].Value;
            if (int.TryParse(val, out int result))
            {
                return result;
            }
            switch (val)
            {
                case "one":
                    return 1;
                case "two":
                    return 2;
                case "three":
                    return 3;
                case "four":
                    return 4;
                case "five":
                    return 5;
                case "six":
                    return 6;
                case "seven":
                    return 7;
                case "eight":
                    return 8;
                case "nine":
                    return 9;
                default:
                    throw new Exception();
            }
        }
    }
}

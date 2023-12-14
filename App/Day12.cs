


namespace AdventOfCode2023.App
{
    internal static class Day12
    {
        private static string[] input = File.ReadAllLines("Inputs/12-ez.txt");
        private static int count = 0;
        private static Dictionary<string, string> memo = new();

        public static void Run()
        {
            var res1 = 0;
            foreach (var item in input)
            {
                var splitString = item.Split(' ');
                CalcPossibleDispositionRecursive(splitString[0], splitString[1]);
                res1 += count;
                count = 0;
            }

            Console.WriteLine($"Part 1: {res1}");

            var res2 = 0;
            foreach (var item in input)
            {
                var splitString = item.Split(' ');
                var s1 = string.Empty;
                var s2 = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    s1 += $"{splitString[0]}?";
                    s2 += $"{splitString[1]},";
                }
                CalcPossibleDispositionRecursive(s1.Remove(s1.Length - 1), s2.Remove(s2.Length - 1));
                res2 += count;
                count = 0;
            }

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static void CalcPossibleDispositionRecursive(string s, string adjacentSprings)
        {
            var questionIndex = s.IndexOf('?');
            if (questionIndex < 0)
            {
                count += CheckContiguousGroups(s) == adjacentSprings ? 1 : 0;
            }
            else
            {
                var charArr = s.ToCharArray();
                charArr[questionIndex] = '.';
                CalcPossibleDispositionRecursive(new string(charArr), adjacentSprings);
                charArr[questionIndex] = '#';
                CalcPossibleDispositionRecursive(new string(charArr), adjacentSprings);
            }
        }

        private static string CheckContiguousGroups(string s)
        {
            if (memo.ContainsKey(s)) return memo[s];

            var splitString = s.Split('.');
            var res = string.Join(",", splitString.Select(x => x.Length).Where(x => x > 0));
            memo.Add(s, res);
            return res;
        }
    }
}

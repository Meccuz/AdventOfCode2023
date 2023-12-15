


namespace AdventOfCode2023.App
{
    internal static class Day13
    {
        private static string[] input = File.ReadAllLines("Inputs/13.txt");
        private static List<List<string>> patterns = new();

        public static void Run()
        {
            var tmpPattern = new List<string>();
            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    patterns.Add(new List<string>(tmpPattern));
                    tmpPattern = new();
                    continue;
                }
                tmpPattern.Add(line);
            }
            patterns.Add(new List<string>(tmpPattern));

            var res1 = 0;
            foreach (var pattern in patterns)
            {
                res1 += CheckColumnsSimmetry(pattern);
                res1 += CheckRowsSimmetry(pattern) * 100;
            }

            Console.WriteLine($"Part 1: {res1}");


            Console.WriteLine($"Part 2: {""}");
            Console.ReadLine();
        }

        private static int CheckRowsSimmetry(List<string> pattern)
        {
            for (int i = 1; i < pattern.Count; i++)
            {
                bool moveToNextLine = false;
                for (int j = 0; i - j - 1 >= 0 && j + i < pattern.Count && !moveToNextLine; j++)
                {
                    for (int k = 0; k < pattern[0].Length && !moveToNextLine; k++)
                    {
                        moveToNextLine = pattern[i - j - 1][k] != pattern[i + j][k];
                    }
                }
                if (!moveToNextLine) return i;
            }
            return 0;
        }

        private static int CheckColumnsSimmetry(List<string> pattern)
        {
            for (int i = 1; i < pattern[0].Length; i++)
            {
                bool moveToNextCol = false;
                for (int j = 0; i - j - 1 >= 0 && j + i < pattern[0].Length && !moveToNextCol; j++)
                {
                    for (int k = 0; k < pattern.Count && !moveToNextCol; k++)
                    {
                        moveToNextCol = pattern[k][i - j - 1] != pattern[k][i + j];
                    }
                }
                if (!moveToNextCol) return i;
            }
            return 0;
        }
    }
}

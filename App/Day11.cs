
namespace AdventOfCode2023.App
{
    internal static class Day11
    {
        private static string[] input = File.ReadAllLines("Inputs/11.txt");
        private static List<int> rowsToDuplicate = new();
        private static List<int> colsToDuplicate;

        public static void Run()
        {
            ExpandInput();
            //List<string> inputExpanded = ExpandInput(2);
            List<(long, long)> galaxiesCoord = LoadGalaxiesCoords(input.ToList());
            var galaxiedDistance = CalculateGalaxiesDistances(galaxiesCoord, 1);
            var res1 = galaxiedDistance.Select(x => x.Value).Sum();

            Console.WriteLine($"Part 1: {res1}");

            //var inputExpandedV2 = ExpandInput(1_000_000);
            //var galaxiesCoordV2 = LoadGalaxiesCoords(input.ToList());
            var galaxiedDistanceV2 = CalculateGalaxiesDistances(galaxiesCoord, 999_999);
            var res2 = galaxiedDistanceV2.Select(x => x.Value).Sum();

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static void ExpandInput()
        {
            //var res = new List<string>();
            //var rowsToDuplicate = new List<int>();
            var columnsToDuplicate = Enumerable.Range(0, input[0].Length).ToList();
            for (int i = 0; i < input.Length; i++)
            {
                if (!input[i].Contains("#"))
                {
                    rowsToDuplicate.Add(i);
                }

                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == '#') columnsToDuplicate.Remove(j);
                }
            }

            colsToDuplicate = columnsToDuplicate.ToList();

            //for (int i = 0; i < input.Length; i++)
            //{
            //    var s = string.Empty;
            //    for (int j = 0; j < input[i].Length; j++)
            //    {
            //        s += input[i][j];
            //        if (columnsToDuplicate.Contains(j))
            //        {
            //            for (int k = 0; k < factor - 1; k++)
            //            {
            //                s += input[i][j];
            //            }
            //        }
            //    }
            //    res.Add(s);
            //    if (rowsToDuplicate.Contains(i))
            //    {
            //        for (int k = 0; k < factor - 1; k++)
            //        {
            //            res.Add(s);
            //        }
            //    }
            //}

            //return res;
        }

        private static List<(long, long)> LoadGalaxiesCoords(List<string> inputExpanded)
        {
            var res = new List<(long, long)>();
            for (int i = 0; i < inputExpanded.Count; i++)
            {
                for (int j = 0; j < inputExpanded[i].Count(); j++)
                {
                    if (inputExpanded[i][j] == '#')
                    {
                        res.Add((i, j));
                    }
                }
            }

            return res;
        }

        private static Dictionary<((long, long), (long, long)), long> CalculateGalaxiesDistances(
            List<(long, long)> galaxiesCoord,
            int factor)
        {
            var res = new Dictionary<((long, long), (long, long)), long>();
            var cartesianGalaxies = from g1 in galaxiesCoord
                                    from g2 in galaxiesCoord
                                    where g1 != g2
                                    select new { g1, g2 };
            foreach (var g in cartesianGalaxies)
            {
                if (!res.ContainsKey((g.g1, g.g2)) && !res.ContainsKey((g.g2, g.g1)))
                {
                    var rowsToBeAdded = rowsToDuplicate
                        .Where(x => (x < g.g1.Item1 && x > g.g2.Item1) || (x > g.g1.Item1 && x < g.g2.Item1))
                        .Count();
                    var colsToBeAdded = colsToDuplicate
                        .Where(x => (x < g.g1.Item2 && x > g.g2.Item2) || (x > g.g1.Item2 && x < g.g2.Item2))
                        .Count();

                    res.Add(
                        (g.g1, g.g2),
                        Math.Abs(g.g1.Item1 - g.g2.Item1) + Math.Abs(g.g1.Item2 - g.g2.Item2) +
                        factor * rowsToBeAdded + factor * colsToBeAdded);
                }
            }

            return res;
        }
    }
}

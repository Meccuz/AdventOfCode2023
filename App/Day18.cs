





using System.Text.RegularExpressions;

namespace AdventOfCode2023.App
{
    internal static class Day18
    {
        private static string[] input = File.ReadAllLines("Inputs/18-ez.txt");
        private static long minX = 0;
        private static long minY = 0;
        private static long maxX = 0;
        private static long maxY = 0;
        private static HashSet<(long, long)> trench = new();
        private static HashSet<(long, long)> outsideTrench = new();
        private static List<(char direction, long length, string color)> instructions = new();

        public static void Run()
        {
            foreach (var line in input)
            {
                var match = Regex.Match(line, @"(\w) (\d+) \((.*)\)");
                instructions.Add((
                    char.Parse(match.Groups[1].Value),
                    long.Parse(match.Groups[2].Value),
                    match.Groups[3].Value));
            }

            CalculateTrench();
            var res1 = CalculateInsideTrench();
            //PrlongTrench();

            Console.WriteLine($"Part 1: {res1}");

            minX = 0;
            minY = 0;
            maxX = 0;
            maxY = 0;
            trench = new();
            outsideTrench = new();

            CalculateTrenchV2();
            var res2 = CalculateInsideTrench();

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static void PrintTrench()
        {
            for (long i = minY; i <= maxY; i++)
            {
                for (long j = minX; j <= maxX; j++)
                {
                    if (trench.Contains((i, j))) Console.Write("#");
                    else if (outsideTrench.Contains((i, j))) Console.Write("@");
                    else Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private static long CalculateInsideTrench()
        {
            var totalTerrain = (maxX + Math.Abs(minX) + 1) * (maxY + Math.Abs(minY) + 1);
            for (long i = minX; i <= maxX; i++)
            {
                if (!trench.Contains((minY, i)) && !outsideTrench.Contains((minY, i)))
                {
                    CalculateOutsideTrench(minY, i);
                }
                if (!trench.Contains((maxY, i)) && !outsideTrench.Contains((maxY, i)))
                {
                    CalculateOutsideTrench(maxY, i);
                }
            }

            for (long i = minY; i <= maxY; i++)
            {
                if (!trench.Contains((i, minX)) && !outsideTrench.Contains((i, minX)))
                {
                    CalculateOutsideTrench(i, minX);
                }
                if (!trench.Contains((i, maxX)) && !outsideTrench.Contains((i, maxX)))
                {
                    CalculateOutsideTrench(i, maxX);
                }
            }
            return totalTerrain - outsideTrench.Count;
        }

        private static void CalculateOutsideTrench(long startRow, long startCol)
        {
            Stack<(long, long)> cellsToVisit = new();
            cellsToVisit.Push((startRow, startCol));

            while (cellsToVisit.Count > 0)
            {
                var (row, col) = cellsToVisit.Pop();

                if (row >= minY && col >= minX && row <= maxY && col <= maxX
                    && !trench.Contains((row, col)) && !outsideTrench.Contains((row, col)))
                {
                    outsideTrench.Add((row, col));

                    cellsToVisit.Push((row, col + 1));
                    cellsToVisit.Push((row, col - 1));
                    cellsToVisit.Push((row + 1, col));
                    cellsToVisit.Push((row - 1, col));
                }
            }
        }

        private static void CalculateTrench()
        {
            var row = 0;
            var col = 0;
            foreach (var instruction in instructions)
            {
                for (var i = 0; i < instruction.length; i++)
                {
                    trench.Add((row, col));
                    switch (instruction.direction)
                    {
                        case 'U':
                            row--;
                            if (row < minY) minY = row;
                            break;
                        case 'D':
                            row++;
                            if (row > maxY) maxY = row;
                            break;
                        case 'L':
                            col--;
                            if (col < minX) minX = col;
                            break;
                        case 'R':
                            col++;
                            if (col > maxX) maxX = col;
                            break;
                    }
                }
            }
        }

        private static void CalculateTrenchV2()
        {
            var row = 0;
            var col = 0;
            foreach (var instruction in instructions)
            {
                var length = Convert.ToInt64(instruction.color.Replace("#", string.Empty).Substring(0, 5), 16);
                var direction = char.Parse(instruction.color.Replace("#", string.Empty).Substring(5));

                for (var i = 0; i < length; i++)
                {
                    trench.Add((row, col));
                    switch (direction)
                    {
                        case '3':
                            row--;
                            if (row < minY) minY = row;
                            break;
                        case '1':
                            row++;
                            if (row > maxY) maxY = row;
                            break;
                        case '2':
                            col--;
                            if (col < minX) minX = col;
                            break;
                        case '0':
                            col++;
                            if (col > maxX) maxX = col;
                            break;
                    }
                }
            }
        }
    }
}

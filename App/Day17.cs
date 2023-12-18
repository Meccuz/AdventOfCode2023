





namespace AdventOfCode2023.App
{
    internal static class Day17
    {
        private static string[] input = File.ReadAllLines("Inputs/17.txt");
        private static int minHeatLost = int.MaxValue;
        private static Dictionary<string, int> usedPaths = new();

        public static void Run()
        {
            MoveCrucible(0, 1, 1, 0, 'R');
            MoveCrucible(1, 0, 1, 0, 'D');

            Console.WriteLine($"Part 1: {minHeatLost}");

            Console.WriteLine($"Part 2: {""}");
            Console.ReadLine();
        }

        private static void MoveCrucible(
            int row,
            int col,
            int movesInDirection,
            int tot,
            char direction)
        {
            var val = int.Parse(input[row][col].ToString());
            tot += val;

            var pathKey = $"{row}-{col}-{movesInDirection}{direction}";
            if (usedPaths.ContainsKey(pathKey) && usedPaths[pathKey] <= tot) return;
            usedPaths[pathKey] = tot;
            if (row == input.Length - 1 && col == input[0].Length - 1)
            {
                minHeatLost = tot < minHeatLost ? tot : minHeatLost;
                return;
            }

            switch (direction)
            {
                case 'R':
                    if (movesInDirection < 3 && col < input[0].Length - 1)
                        MoveCrucible(row, col + 1, movesInDirection + 1, tot, direction);
                    if (row > 0)
                        MoveCrucible(row - 1, col, 1, tot, 'T');
                    if (row < input.Length - 1)
                        MoveCrucible(row + 1, col, 1, tot, 'D');
                    break;
                case 'L':
                    if (movesInDirection < 3 && col > 0)
                        MoveCrucible(row, col - 1, movesInDirection + 1, tot, direction);
                    if (row > 0)
                        MoveCrucible(row - 1, col, 1, tot, 'T');
                    if (row < input.Length - 1)
                        MoveCrucible(row + 1, col, 1, tot, 'D');
                    break;
                case 'T':
                    if (movesInDirection < 3 && row > 0)
                        MoveCrucible(row - 1, col, movesInDirection + 1, tot, direction);
                    if (col > 0)
                        MoveCrucible(row, col - 1, 1, tot, 'L');
                    if (col < input[0].Length - 1)
                        MoveCrucible(row, col + 1, 1, tot, 'R');
                    break;
                case 'D':
                    if (movesInDirection < 3 && row < input.Length - 1)
                        MoveCrucible(row + 1, col, movesInDirection + 1, tot, direction);
                    if (col > 0)
                        MoveCrucible(row, col - 1, 1, tot, 'L');
                    if (col < input[0].Length - 1)
                        MoveCrucible(row, col + 1, 1, tot, 'R');
                    break;
            }
        }
    }
}
